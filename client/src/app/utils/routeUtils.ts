import { routeLinks } from "../components/layout/routes/routeLinks";
import { objToQueryString } from "./requestParameterUtils";

export const getDefaultPageRoute = (): string => {
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