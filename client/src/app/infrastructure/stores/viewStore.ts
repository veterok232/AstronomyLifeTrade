import { makeAutoObservable } from "mobx";

class ViewStore {
    public isNavigationOpened = false;
    public selectedNavigationPath: string = null;

    constructor() {
        makeAutoObservable(this);
    }

    public toggleNavigation() {
        this.isNavigationOpened = !this.isNavigationOpened;
    }

    public setSelectedPath(path: string) {
        if (this.selectedNavigationPath !== path) {
            this.selectedNavigationPath = path;
        }
    }
}

export const viewStore = new ViewStore();