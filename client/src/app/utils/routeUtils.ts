import { routeLinks } from "../components/layout/routes/routeLinks";
import { isManager, isStaff } from "../infrastructure/services/auth/authService";
import { objToQueryString } from "./requestParameterUtils";

export const getDefaultPageRoute = (): string => {
    if (isManager()) {
        return getRoute(routeLinks.orders.root);
    } else if (isStaff()) {
        return getRoute(routeLinks.account.staffProfile);
    }

    return getRoute(routeLinks.catalog.root);
};

// eslint-disable-next-line @typescript-eslint/no-explicit-any
export const getRoute = (path: string, ...parameters: any[]): string => {
    return parameters?.length
        ? parameters.reduce((p, parameter) => p.replace(/:[^/]*/, parameter), path)
        : path;
};

// eslint-disable-next-line @typescript-eslint/no-explicit-any
export const addQueryParameter = (path: string, queryParameter: any): string => {
    return `${path}?${objToQueryString(queryParameter)}`;
};