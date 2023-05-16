let accessToken: string;

export function getAccessToken(): string {
    return accessToken;
}

export function setAccessToken(token: string): void {
    accessToken = token;
}

export function clearAccessToken(): void {
    accessToken = null;
}