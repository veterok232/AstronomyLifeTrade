export interface RouteItem {
    path: string;
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    component: React.ComponentType<any>;
    onlyForRoles?: string[];
    onlyForPermissions?: string[];
}

export const commonRoutes: Array<RouteItem> = [

];

export const mainMenuRoutes: Array<RouteItem> = [

];

export const internalRoutes: Array<RouteItem> = [

];