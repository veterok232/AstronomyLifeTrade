import { getMenuItems } from "../components/layout/catalogNavigation/catalogNavigationConfig";
import { routeLinks } from "../components/layout/routes/routeLinks";
import { NavigationItemType } from "../dataModels/enums/navigationItemType";
import { MenuItem } from "../dataModels/menu/menuItem";
import { objToQueryString } from "./requestParameterUtils";

export const getDefaultPageRoute = (): string => {
    return getFirstPageRoute();
};

const getFirstPageRoute = () => {
    const firstMenuItem = getMenuItems()[0];

    if (!firstMenuItem) {
        return routeLinks.errors.forbidden;
    }

    return searchFirstAvailableRoute(firstMenuItem);
};

const searchFirstAvailableRoute = (menuItem: MenuItem) => {
    return menuItem.type === NavigationItemType.Link
        ? menuItem.to
        : searchFirstAvailableRoute(menuItem.childItems[0]);
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