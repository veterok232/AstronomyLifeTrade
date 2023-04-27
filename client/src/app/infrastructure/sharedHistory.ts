/* eslint-disable @typescript-eslint/no-explicit-any, @typescript-eslint/no-unsafe-argument */
import { createBrowserHistory, To } from "history";
import { getRoute } from "../utils/routeUtils";

class SharedHistory {
    public history = createBrowserHistory();

    public getState(): any {
        return this.history.location.state;
    }

    public getSearchString(): string {
        return this.history.location.search;
    }

    public onPop(callback: () => void): () => void {
        return this.history.listen(({ action }) => {
            if (action === "POP") {
                callback();
            }
        });
    }

    public push(path: string, ...parameters: any[]) {
        this.history.push(getRoute(path, ...parameters));
    }

    public pushWithState(path: string, state: any, ...parameters: any[]) {
        this.history.push(getRoute(path, ...parameters), state);
    }

    public replace(location: To, state?: any) {
        this.history.replace(location, state);
    }

    public goBack(): void {
        this.history.back();
    }
}

export const sharedHistory = new SharedHistory();