import { refreshAccessToken } from "../../../api/identity/identityApi";
import { Result } from "../../../dataModels/common/result";
import { RefreshAccessTokenData } from "../../../dataModels/identity/refreshAccessTokenData";
import { crossWindowEventBroker } from "../crossWindowEventBroker";
import { getFingerprint } from "./fingerprintService";

const refreshTokenStatusKey = "refreshTokenStatus";
const refreshInProgressValue = "inProgress";
const refreshCompletedEventName = "refreshTokenCompleted";

let refreshTokenPromise: Promise<Result<RefreshAccessTokenData>> = null;

const refreshTokenHandler = async (userId: string, silent?: boolean): Promise<Result<RefreshAccessTokenData>> => {
    return refreshAccessToken({
        userId,
        fingerprint: await getFingerprint(),
        silent,
    });
};

const handleRefreshTokenStart = (userId: string, silent?: boolean) => {
    window.localStorage.setItem(refreshTokenStatusKey, refreshInProgressValue);
    refreshTokenPromise = refreshTokenHandler(userId, silent);
};

const clearRefreshTokenInfrastructureValues = () => {
    window.localStorage.removeItem(refreshTokenStatusKey);
    refreshTokenPromise = null;
};

const handleRefreshTokenFinish = () => {
    clearRefreshTokenInfrastructureValues();
};

export const initRefreshSynchronization = () => {
    clearRefreshTokenInfrastructureValues();
};

export async function refreshAccessTokenSynchronized(userId: string, silent?: boolean): Promise<Result<RefreshAccessTokenData>> {
    if (refreshTokenPromise !== null) {
        return refreshTokenPromise;
    }

    if (window.localStorage.getItem(refreshTokenStatusKey) === refreshInProgressValue) {
        return new Promise((resolve) => {
            const handlerRef = crossWindowEventBroker.subscribe(refreshCompletedEventName, (result: string) => {
                crossWindowEventBroker.unsubscribe(handlerRef);
                resolve(JSON.parse(result) as Result<RefreshAccessTokenData>);
            });
        });
    }

    handleRefreshTokenStart(userId, silent);
    let result: Result<RefreshAccessTokenData>;
    try {
        result = await refreshTokenPromise;
    } finally {
        handleRefreshTokenFinish();
    }
    crossWindowEventBroker.publish(refreshCompletedEventName, JSON.stringify(result));

    return result;
}