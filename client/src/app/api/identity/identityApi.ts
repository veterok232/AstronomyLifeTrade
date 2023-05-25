import { UserRegistrationModel } from "../../components/identity/registerPage";
import { Result } from "../../dataModels/common/result";
import { OneTimeTokenTermType } from "../../dataModels/enums/oneTimeTokenTermType";
import { ChangeAssignmentData } from "../../dataModels/identity/changeAssignmentData";
import { ClientIdentificationData } from "../../dataModels/identity/clientIdentificationData";
import { ExtendSessionResponse } from "../../dataModels/identity/extendSessionResponse";
import { IdentityData } from "../../dataModels/identity/identityData";
import { LoginData } from "../../dataModels/identity/loginData";
import { RefreshAccessTokenData } from "../../dataModels/identity/refreshAccessTokenData";
import { handleErrorResult } from "../../utils/handleResponseUtils";
import { httpPost } from "../core/requestApi";

const identityResource = "identity";

export async function login(data: LoginData): Promise<IdentityData> {
    const response = await httpPost<LoginData, Result<IdentityData>>({
        url: `${identityResource}/login`,
        body: data,
        disableSuccessfulToast: true,
    });
    handleErrorResult(response);

    return response.data;
}

export async function register(data: UserRegistrationModel): Promise<Result> {
    const response = await httpPost<UserRegistrationModel, Result>({
        url: `${identityResource}/register-consumer`,
        body: data,
        disableSuccessfulToast: true,
    });

    handleErrorResult(response);

    return response;
}

export async function chooseAssignment(changeAssignmentData: ChangeAssignmentData): Promise<IdentityData> {
    const response = await httpPost<ChangeAssignmentData, Result<IdentityData>>({
        url: `${identityResource}/choose-assignment`,
        body: changeAssignmentData,
        disableSuccessfulToast: true,
    });
    handleErrorResult(response);

    return response.data;
}

export async function switchToProxyAccess(changeAssignmentData: ChangeAssignmentData): Promise<IdentityData> {
    const response: Result<IdentityData> = await httpPost({
        url: `${identityResource}/switch-to-proxy-access`,
        body: changeAssignmentData,
        disableSuccessfulToast: true,
    });
    handleErrorResult(response);

    return response.data;
}

export async function logout() {
    return await httpPost({
        url: `${identityResource}/logout`,
        useElevatedProgressIndicator: true,
        disableSuccessfulToast: true,
    });
}

export function refreshAccessToken(data: ClientIdentificationData): Promise<Result<RefreshAccessTokenData>> {
    return httpPost({
        url: `${identityResource}/refresh-access-token`,
        body: data,
        isAnonymous: true,
        silent: data.silent,
        disableSuccessfulToast: true,
    });
}

export async function extendSession(data: ClientIdentificationData): Promise<Result<ExtendSessionResponse>> {
    return await httpPost<ClientIdentificationData, Result<ExtendSessionResponse>>({
        url: `${identityResource}/extend-session`,
        body: data,
        useElevatedProgressIndicator: true,
        disableSuccessfulToast: true,
    });
}

export async function createOneTimeToken(tokenTermType: OneTimeTokenTermType): Promise<string> {
    return await httpPost({
        url: `${identityResource}/create-one-time-token`,
        body: { tokenTermType },
        useElevatedProgressIndicator: true,
        disableSuccessfulToast: true,
    });
}