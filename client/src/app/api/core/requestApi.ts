import { routeLinks } from "../../components/layout/routes/routeLinks";
import { notifications } from "../../components/toast/toast";
import { OneTimeTokenTermType } from "../../dataModels/enums/oneTimeTokenTermType";
import { getAccessToken } from "../../infrastructure/services/auth/accessTokenService";
import { isTokenExpired, refreshToken } from "../../infrastructure/services/identityService";
import { sharedHistory } from "../../infrastructure/sharedHistory";
import { modalsStore } from "../../infrastructure/stores/modalsStore";
import { ProgressTrackOptions, progressIndicatorStore } from "../../infrastructure/stores/progressIndicatorStore";
import { isEmptyOrSucceeded } from "../../utils/requestUtils";
import { createOneTimeToken } from "../identity/identityApi";

export type HttpRequestMethod = "GET" | "POST" | "PUT" | "DELETE";

export interface RequestOptions<T> {
    url: string;
    silent?: boolean;
    useElevatedProgressIndicator?: boolean;
    body?: T;
    isAnonymous?: boolean;
    skipRedirectForConcurrency?: boolean;
    requiredOneTimeTokenWithTermType?: OneTimeTokenTermType;
    disableSuccessfulToast?: boolean;
    blockedGroup?: string;
    requestId?: string;
}

export const responseHandlingStatuses = {
    unhandled: 0,
    unauthenticated: 1,
    refreshTokenWasFailed: 2,
    refreshTokenWasCompleted: 3,
    assignmentIsMissing: 4,
    refreshTokenNotNeeded: 5,
    unauthorized: 6,
    concurrencyUpdate: 7,
    concurrencyGet: 8,
    forbidden: 9,
    assignmentDeactivated: 10,
};

export const apiRootUrl = "/api";

const headerNames = {
    assignmentIsMissing: "AssignmentIsMissing",
    authenticate: "www-authenticate",
    exceptionTraceId: "ExceptionTraceId",
    fileName: "FileName",
    contentType: "content-type",
};

const contentTypes = {
    plainText: "text/plain",
    json: "application/json"
};

function getBody<T>(options: RequestOptions<T>) {
    if (!options || !(options.body)) {
        return undefined;
    }

    if (options.body instanceof FormData) {
        return options.body;
    }

    return JSON.stringify(options.body);
}

function getContentTypeHeader<T>(options?: RequestOptions<T>): { "Content-Type": string } | {} {
    if (options === null || options === undefined) {
        return { "Content-Type": contentTypes.plainText };
    }
    if (options.body instanceof FormData) {
        return {};
    }
    return {
        "Content-Type": (typeof options.body === "object"
            ? contentTypes.json
            : contentTypes.plainText),
    };
}

function handleRedirect<T>(status: number, options?: RequestOptions<T>) {
    let redirectToRoute: string = undefined;
    let redirectToPage: string = undefined;

    switch (status) {
        case responseHandlingStatuses.unauthorized:
        case responseHandlingStatuses.unauthenticated:
        case responseHandlingStatuses.refreshTokenWasFailed:
            redirectToPage = routeLinks.login;
            break;
        case responseHandlingStatuses.assignmentIsMissing:
            redirectToRoute = routeLinks.account.selectAssignment;
            break;
        case responseHandlingStatuses.concurrencyGet:
            redirectToRoute = options?.skipRedirectForConcurrency ? undefined : routeLinks.errors.notFound;
            break;
        case responseHandlingStatuses.forbidden:
            redirectToRoute = routeLinks.errors.forbidden;
            break;
        case responseHandlingStatuses.assignmentDeactivated:
            redirectToPage = `${routeLinks.login}?assignment-deactivated=${true.toString()}`;
            break;
    }

    if (redirectToRoute) {
        sharedHistory.push(redirectToRoute);
    }
    if (redirectToPage) {
        window.location.href = redirectToPage;
    }
}

async function getOneTimeTokenAuthorizationHeader(
    tokenTermType: OneTimeTokenTermType
): Promise<{ "Authorization": string } | {}> {
    const token = await createOneTimeToken(tokenTermType);

    return { "Authorization": `OneTimeToken ${token}` };
}

async function getAccessTokenAuthorizationHeader(silent?: boolean): Promise<{ "Authorization": string } | {}> {
    let token = getAccessToken();
    if (token && isTokenExpired(token)) {
        const refreshTokenResponseStatus = await refreshToken(silent);

        handleRedirect(refreshTokenResponseStatus);

        if (refreshTokenResponseStatus === responseHandlingStatuses.assignmentDeactivated) {
            return null;
        }

        token = getAccessToken();
    }

    return token ? { "Authorization": `Bearer ${token}` } : {};
}

async function getAuthorizationHeader<T>(options: RequestOptions<T>): Promise<{ "Authorization": string } | {} | null> {
    if (options.isAnonymous) {
        return null;
    }

    if (options.requiredOneTimeTokenWithTermType) {
        return getOneTimeTokenAuthorizationHeader(options.requiredOneTimeTokenWithTermType);
    }

    return getAccessTokenAuthorizationHeader(options.silent);
}

function isInvalidTokenResponse(response: Response): boolean {
    const authHeader = response.headers.get(headerNames.authenticate);
    return authHeader && authHeader.includes("invalid_token");
}

function isAssignmentMissing(response: Response): boolean {
    return response.headers.get(headerNames.assignmentIsMissing) as unknown as boolean;
}

async function handleUnauthorized(response: Response): Promise<number> {
    return isInvalidTokenResponse(response)
        ? await refreshToken()
        : responseHandlingStatuses.unauthorized;
}

function handleForbidden(response: Response): number {
    return isAssignmentMissing(response)
        ? responseHandlingStatuses.assignmentIsMissing
        : responseHandlingStatuses.forbidden;
}

async function handleHeaders<T>(response: Response, method: string, options: RequestOptions<T>): Promise<number> {
    let handleHeaderStatus = responseHandlingStatuses.unhandled;

    switch (response.status) {
        case 401:
            handleHeaderStatus = await handleUnauthorized(response);
            break;
        case 403:
            handleHeaderStatus = handleForbidden(response);
            break;
        case 409:
            if (method === "GET") {
                handleHeaderStatus = responseHandlingStatuses.concurrencyGet;
                notifications.localizedError("RetrieveDataConcurrencyError");
            } else {
                handleHeaderStatus = responseHandlingStatuses.concurrencyUpdate;
                notifications.localizedError("UpdateDataConcurrencyError");
            }
            break;
    }

    if (!options.skipRedirectForConcurrency) {
        modalsStore.closeAllModals();
    }

    handleRedirect(handleHeaderStatus, options);

    return handleHeaderStatus;
}

async function handleSucceededResponse<TResult>(response: Response): Promise<TResult> {
    const responseText = await response.text();

    if (!responseText) {
        return;
    }

    return JSON.parse(responseText);
}

function handleSucceededToastMessage<TResult>(
    result: TResult,
    method: HttpRequestMethod,
    disableSuccessfulToast: boolean,
) {
    if (!disableSuccessfulToast && method !== "GET" && isEmptyOrSucceeded<TResult>(result)) {
        notifications.defaultSuccess();
    }
}

async function handleFailedResponse<T>(response: Response, method: string, options: RequestOptions<T>) {
    const handleHeaderStatus = await handleHeaders(response, method, options);

    if (handleHeaderStatus === responseHandlingStatuses.unhandled) {
        notifications.defaultRequestError(response.headers.get(headerNames.exceptionTraceId));
    }

    throw new Error(`Request was failed with status: ${Object.entries(responseHandlingStatuses).map(
        // To show handled status as string
        function ([key, value]) {
            if (value === handleHeaderStatus) {
                return key;
            }
        }).filter((key) => key)}.`);
}

async function performRequest<TRequest, TResult>(
    method: HttpRequestMethod,
    options: RequestOptions<TRequest>,
): Promise<TResult> {
    try {
        progressIndicatorStore.startProgress(createProgressTrackOptions(options));

        const authorizationHeader = await getAuthorizationHeader(options);

        if (!authorizationHeader && !options.isAnonymous) {
            return Promise.resolve(null);
        }

        const response = await fetch(`${apiRootUrl}/${options.url}`, {
            method,
            body: getBody(options),
            credentials: "include",
            headers: {
                "Accept": contentTypes.json,
                ...authorizationHeader,
                ...getContentTypeHeader(options),
            },
        });

        if (response.ok) {
            const result = await handleSucceededResponse<TResult>(response);
            handleSucceededToastMessage(result, method, options.disableSuccessfulToast || options.silent);

            return result;
        }

        await handleFailedResponse(response, method, options);
    } finally {
        progressIndicatorStore.stopProgress(createProgressTrackOptions(options));
    }
}

function createProgressTrackOptions<TRequest>(options: RequestOptions<TRequest>): ProgressTrackOptions {
    return {
        silent: options.silent,
        blockedGroup: options.blockedGroup,
        isElevated: options.useElevatedProgressIndicator,
        requestId: options.requestId,
    };
}

export async function httpGet<TResult>(options: RequestOptions<void>): Promise<TResult> {
    return performRequest("GET", options);
}

export async function httpPost<TRequest, TResult = void>(options: RequestOptions<TRequest>): Promise<TResult> {
    return performRequest<TRequest, TResult>("POST", options);
}

export async function httpPut<TRequest, TResult = void>(options: RequestOptions<TRequest>): Promise<TResult> {
    return performRequest<TRequest, TResult>("PUT", options);
}

export async function httpDelete<TResult = void>(options: RequestOptions<void>): Promise<TResult> {
    return performRequest("DELETE", options);
}
