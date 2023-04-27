import { Dictionary } from "lodash";
import { makeAutoObservable } from "mobx";

export const blockedRequestGroups = {
    common: "Common",
    dashboard: "Dashboard"
};

export interface ProgressTrackOptions {
    silent?: boolean;
    isElevated?: boolean;
    blockedGroup?: string;
    requestId?: string;
}

class ProgressIndicatorStore {
    private blockedGroups: Dictionary<number> = {
        [blockedRequestGroups.common]: 0
    };

    private individuallyTrackedRequests: string[] = [];

    public isElevated = false;

    public get isInProgress() {
        return this.blockedGroups[blockedRequestGroups.common] > 0;
    }

    constructor() {
        makeAutoObservable(this);
    }

    public isGroupInProgress(blockedGroup: string) {
        if (this.blockedGroups[blockedGroup]) {
            return this.blockedGroups[blockedGroup] > 0;
        }
    }

    public isRequestInProgress(requestId: string) {
        return this.individuallyTrackedRequests.includes(requestId);
    }

    public startProgress(options?: ProgressTrackOptions) {
        if (options?.requestId) {
            this.individuallyTrackedRequests.push(options.requestId);
        }
        if (options?.silent) {
            return;
        }
        const blockedGroup = options?.blockedGroup ?? blockedRequestGroups.common;
        if (!Object.keys(this.blockedGroups).includes(blockedGroup)) {
            this.blockedGroups[blockedGroup] = 0;
        }
        this.blockedGroups[blockedGroup]++;
        if (options?.isElevated) {
            this.isElevated = true;
        }
    }

    public stopProgress(options?: ProgressTrackOptions) {
        if (options?.requestId) {
            this.individuallyTrackedRequests = this.individuallyTrackedRequests.filter(r => r !== options.requestId);
        }
        if (options?.silent) {
            return;
        }
        const blockedGroup = options?.blockedGroup ?? blockedRequestGroups.common;
        if (this.blockedGroups[blockedGroup] > 0) {
            this.blockedGroups[blockedGroup]--;
        }
        if (options?.isElevated) {
            this.isElevated = false;
        }
    }
}

export const progressIndicatorStore = new ProgressIndicatorStore();