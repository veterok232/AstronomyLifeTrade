import { makeAutoObservable } from "mobx";
import { convertUtcStringToDate } from "../../utils/dateTimeUtils";
import { ContextResponse } from "../../dataModels/common/contextResponse";

class ContextStore {
    public lang = "en";
    public isAuthenticated: boolean;
    public userId?: string;
    public roleName?: string;
    public firstName?: string;
    public lastName?: string;
    public refreshTokenExpirationDateTime?: Date;

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
        this.roleName = context.roleName;
        this.firstName = context.firstName;
        this.lastName = context.lastName;
        this.setRefreshTokenExpirationDateTime(context.refreshTokenExpirationDateTime);
    }

    public setRefreshTokenExpirationDateTime(expiryDateTime: string) {
        this.refreshTokenExpirationDateTime = convertUtcStringToDate(expiryDateTime);
    }
}

export const contextStore = new ContextStore();