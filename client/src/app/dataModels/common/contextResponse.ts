export interface ContextResponse {
    isAuthenticated: boolean;
    lang: string;
    userId?: string;
    firstName?: string;
    lastName?: string;
    currentSelectedAssignment?: CurrentAssignmentInfo;
    roleName?: string;
    refreshTokenExpirationDateTime?: string;
    originAssignmentId?: string;
}

export interface CurrentAssignmentInfo {
    id?: string;
}