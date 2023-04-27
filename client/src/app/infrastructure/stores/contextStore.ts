import { makeAutoObservable } from "mobx";
import { ContextResponse, CurrentAssignmentInfo } from "../../dataModels/contextResponse";
import { convertUtcStringToDate } from "../../utils/dateTimeUtils";

class AssignmentInfo {
    public assignmentId?: string;

    public constructor(currentAssignmentInfo: CurrentAssignmentInfo) {
        makeAutoObservable(this);
        this.assignmentId = currentAssignmentInfo.id;
    }
}

class ContextStore {
    public lang = "en";
    public isAuthenticated: boolean;
    public userId?: string;
    public hasMultipleAssignments: boolean;
    public currentAssignment?: AssignmentInfo;
    public roleName?: string;
    public firstName?: string;
    public lastName?: string;
    public refreshTokenExpirationDateTime?: Date;
    public originAssignmentId?: string;
    public permissions?: string[];

    public isContextLoaded = false;

    public get fullName(): string {
        return `${this.firstName} ${this.lastName}`;
    }

    constructor() {
        makeAutoObservable(this);
    }

    public setContext(context: ContextResponse) {
        if (!this.isContextLoaded) {
            this.isContextLoaded = true;
        }

        this.lang = context.lang;
        this.isAuthenticated = context.isAuthenticated;
        this.userId = context.userId;
        this.hasMultipleAssignments = context.hasMultipleAssignments;
        this.roleName = context.roleName;
        this.firstName = context.firstName;
        this.lastName = context.lastName;
        this.setRefreshTokenExpirationDateTime(context.refreshTokenExpirationDateTime);
        this.originAssignmentId = context.originAssignmentId;

        if (context.currentSelectedAssignment.id) {
            this.currentAssignment = new AssignmentInfo(context.currentSelectedAssignment);
        }
    }

    public setRefreshTokenExpirationDateTime(expiryDateTime: string) {
        this.refreshTokenExpirationDateTime = convertUtcStringToDate(expiryDateTime);
    }

    public setPermissions(permissions: string[]) {
        this.permissions = permissions;
    }
}

export const contextStore = new ContextStore();