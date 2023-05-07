/* eslint-disable camelcase */
import jwt_decode from "jwt-decode";
import { Constants } from "../../components/constants";
import { originRouteParamName, routeLinks } from "../../components/layout/routes/routeLinks";
import { IdentityData } from "../../dataModels/identity/identityData";
import { getQueryParamValue } from "../../utils/requestParameterUtils";
import { getDefaultPageRoute } from "../../utils/routeUtils";
import { sharedHistory } from "../sharedHistory";
import { contextStore } from "../stores/contextStore";
import { clearAccessToken, setAccessToken } from "./auth/accessTokenService";
import { clearUserId, getUserId, setUserId } from "./auth/currentUserService";
import { contextActions } from "./contextService";
import { crossWindowEventBroker } from "./crossWindowEventBroker";
import { refreshAccessTokenSynchronized } from "./auth/refreshTokenService";
import { responseHandlingStatuses } from "../../api/core/requestApi";
import {
    logout,
    login as apiLogin,
    extendSession,
    chooseAssignment,
    switchToProxyAccess,
} from "../../api/identity/identityApi";
import { progressIndicatorStore } from "../stores/progressIndicatorStore";
import { getFingerprint } from "./auth/fingerprintService";

const saveIdentityData = (data?: IdentityData) => {
    if (data) {
        setAccessToken(data.token);
        setUserId(data.userId);
    }
};

const isAssignmentSelectionNeeded = () =>
    contextStore.hasMultipleAssignments && !contextStore.currentAssignment;

export const applyNewIdentity = async (identityData: IdentityData, searchString = "") => {
    saveIdentityData(identityData);
    await contextActions.load();

    if (isAssignmentSelectionNeeded()) {
        sharedHistory.push(`${routeLinks.account.selectAssignment}${searchString}`);
        return;
    }

    crossWindowEventBroker.publish(Constants.crossWindowEvents.logInCompleted);

    const originRoute = getQueryParamValue(originRouteParamName, searchString);
    sharedHistory.push(originRoute || getDefaultPageRoute());
};

export const isTokenExpired = (token: string) => {
    const decodedToken: { exp: number } = jwt_decode(token);

    return Date.now() > decodedToken.exp * 1000;
};

export const removeIdentityData = () => {
    clearAccessToken();
    clearUserId();
};

export async function refreshToken(silent?: boolean): Promise<number> {
    const userId = getUserId();

    if (userId) {
        const response = await refreshAccessTokenSynchronized(userId, silent);

        if (!response.isSucceeded) {
            removeIdentityData();

            return response.data?.isAssignmentInactive
                ? responseHandlingStatuses.assignmentDeactivated
                : responseHandlingStatuses.refreshTokenWasFailed;
        }

        if (!response.data.token) {
            return responseHandlingStatuses.refreshTokenNotNeeded;
        }

        setAccessToken(response.data.token);

        return responseHandlingStatuses.refreshTokenWasCompleted;
    }

    return responseHandlingStatuses.unauthenticated;
}

export const logOut = async () => {
    await logout();
    removeIdentityData();
    crossWindowEventBroker.publish(Constants.crossWindowEvents.logOutCompleted);
    window.location.href = routeLinks.login;
};

export async function login(email: string, password: string): Promise<IdentityData> {
    try {
        progressIndicatorStore.startProgress();

        const fingerprint = await getFingerprint();
        return await apiLogin({
            email,
            password,
            fingerprint,
        });
    } finally {
        progressIndicatorStore.stopProgress();
    }
}

export async function extendSessionRefreshToken() {
    try {
        progressIndicatorStore.startProgress();

        const fingerprint = await getFingerprint();
        const userId = getUserId();
        const response = await extendSession({ fingerprint, userId });
        if (!response.isSucceeded || !response.data) {
            await logOut();
            return;
        }
        contextStore.setRefreshTokenExpirationDateTime(response.data.expiryDateTime);
        crossWindowEventBroker.publish(Constants.crossWindowEvents.sessionExtended, response.data.expiryDateTime);
    } finally {
        progressIndicatorStore.stopProgress();
    }
}

export async function changeAssignment(assignmentId: string): Promise<IdentityData> {
    try {
        progressIndicatorStore.startProgress();

        const userId = getUserId();
        const fingerprint = await getFingerprint();
        return await chooseAssignment({ assignmentId, userId, fingerprint });
    } finally {
        progressIndicatorStore.stopProgress();
    }
}

export async function loginAsProxyAssignment(assignmentId: string): Promise<IdentityData> {
    try {
        progressIndicatorStore.startProgress();

        const userId = getUserId();
        const fingerprint = await getFingerprint();
        return await switchToProxyAccess({ assignmentId, userId, fingerprint });
    } finally {
        progressIndicatorStore.stopProgress();
    }
}