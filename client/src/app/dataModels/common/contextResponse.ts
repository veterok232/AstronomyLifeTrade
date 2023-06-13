export interface ContextResponse {
    isAuthenticated: boolean;
    lang: string;
    userId?: string;
    firstName?: string;
    lastName?: string;
    roleName?: string;
    refreshTokenExpirationDateTime?: string;
    assignmentId?: string;
}

export interface CurrentAssignmentInfo {
    id?: string;
}