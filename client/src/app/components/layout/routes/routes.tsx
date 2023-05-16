import { CatalogMainPage } from "../../catalog/catalogMainPage";
import { CatalogTelescopesPage } from "../../catalog/catalogTelescopesPage";
import { Forbidden } from "../../errorPages/forbidden";
import { NotFound } from "../../errorPages/notFound";
import { routeLinks } from "./routeLinks";

export interface RouteItem {
    path: string;
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    component: React.ComponentType<any>;
    onlyForRoles?: string[];
}

export const commonRoutes: Array<RouteItem> = [
    { path: routeLinks.catalog.root, component: CatalogMainPage },
    { path: routeLinks.catalog.telescopes, component: CatalogTelescopesPage },
    { path: routeLinks.errors.forbidden, component: Forbidden },
    { path: routeLinks.errors.notFound, component: NotFound },
];

export const mainMenuRoutes: Array<RouteItem> = [

];

export const internalRoutes: Array<RouteItem> = [

];