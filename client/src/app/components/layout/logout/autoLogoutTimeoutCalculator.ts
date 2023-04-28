import { stores } from "../../../infrastructure/stores";
import { Constants } from "../../constants";

export const getSecondsToAutoLogout = (): number => {
    const now = new Date();
    const autoLogoutDateUtc = stores.contextStore.refreshTokenExpirationDateTime;
    const diffInMs = +autoLogoutDateUtc - +now;
    return Math.round(diffInMs / Constants.millisecondsInSecond);
};