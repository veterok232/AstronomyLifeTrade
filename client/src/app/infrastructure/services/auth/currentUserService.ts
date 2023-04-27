import { Constants } from "../../../components/constants";

export function setUserId(userId: string) {
    window.localStorage.setItem(Constants.localStorageKeys.userId, userId);
}

export function getUserId(): string {
    return window.localStorage.getItem(Constants.localStorageKeys.userId);
}

export function clearUserId() {
    window.localStorage.removeItem(Constants.localStorageKeys.userId);
}