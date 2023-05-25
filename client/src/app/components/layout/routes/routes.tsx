import { Roles } from "../../../infrastructure/services/auth/roles";
import { CartPage } from "../../cart/cartPage";
import { CatalogMainPage } from "../../catalog/catalogMainPage";
import { CatalogTelescopesPage } from "../../catalog/catalogTelescopesPage";
import { ProductDetailsPage } from "../../catalog/productDetails/productDetailsPage";
import { Forbidden } from "../../errorPages/forbidden";
import { NotFound } from "../../errorPages/notFound";
import { LoginPage } from "../../identity/loginPage";
import { RegisterPage } from "../../identity/registerPage";
import { routeLinks } from "./routeLinks";

export interface RouteItem {
    path: string;
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    component: React.ComponentType<any>;
    onlyForRoles?: string[];
}

export const commonRoutes: Array<RouteItem> = [
    { path: routeLinks.catalog.root, component: CatalogMainPage },
    { path: routeLinks.catalog.telescopes.category, component: CatalogTelescopesPage },
    { path: routeLinks.catalog.productDetails, component: ProductDetailsPage },
    { path: routeLinks.account.login, component: LoginPage },
    { path: routeLinks.account.register, component: RegisterPage },
    { path: routeLinks.errors.forbidden, component: Forbidden },
    { path: routeLinks.errors.notFound, component: NotFound },
];

export const customerRoutes: Array<RouteItem> = [
    { path: routeLinks.cart.root, component: CartPage, onlyForRoles: [Roles.consumer] },
];

export const internalRoutes: Array<RouteItem> = [

];