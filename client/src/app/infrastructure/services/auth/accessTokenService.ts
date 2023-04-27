import { refreshContextPrivileges } from "../refreshPriviledgesService";

let accessToken: string;

export function getAccessToken(): string {
    return accessToken;
}

export function setAccessToken(token: string): void {
    accessToken = token;
    refreshContextPrivileges(token);
}

export function clearAccessToken(): void {
    accessToken = null;
}