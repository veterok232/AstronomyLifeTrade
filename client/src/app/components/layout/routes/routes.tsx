import { Roles } from "../../../infrastructure/services/auth/roles";
import { AccountProfilePage } from "../../accountProfile/accountProfile";
import { ManagerProfile } from "../../accountProfile/managerProfile";
import { StaffProfile } from "../../accountProfile/staffProfile";
import { AstronomicalCalculatorPage } from "../../astronomicalCalculator/astronomicalCalculatorPage";
import { CartPage } from "../../cart/cartPage";
import { CatalogAccessoriesPage } from "../../catalog/catalogAccessoriesPage";
import { CatalogBinocularsPage } from "../../catalog/catalogBinocularsPage";
import { CatalogMainPage } from "../../catalog/catalogMainPage";
import { CatalogTelescopesPage } from "../../catalog/catalogTelescopesPage";
import { CreateProductPage } from "../../catalog/management/createProduct/createProductPage";
import { ProductDetailsPage } from "../../catalog/productDetails/productDetailsPage";
import { ProductsSearchPage } from "../../catalog/productsSearchPage";
import { Forbidden } from "../../errorPages/forbidden";
import { NotFound } from "../../errorPages/notFound";
import { LoginPage } from "../../identity/loginPage";
import { RegisterPage } from "../../identity/registerPage";
import { MakeOrderPage } from "../../orders/makeOrder/makeOrderPage";
import { OrdersPage } from "../../orders/orders/ordersPage";
import { routeLinks } from "./routeLinks";

export interface RouteItem {
    path: string;
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    component: React.ComponentType<any>;
    onlyForRoles?: string[];
}

export const commonRoutes: Array<RouteItem> = [
    { path: routeLinks.catalog.root, component: CatalogMainPage },
    { path: routeLinks.catalog.search, component: ProductsSearchPage },
    { path: routeLinks.catalog.telescopes.category, component: CatalogTelescopesPage },
    { path: routeLinks.catalog.binoculars, component: CatalogBinocularsPage },
    { path: routeLinks.catalog.accessories, component: CatalogAccessoriesPage },
    { path: routeLinks.catalog.productDetails, component: ProductDetailsPage },
    { path: routeLinks.account.login, component: LoginPage },
    { path: routeLinks.account.register, component: RegisterPage },
    { path: routeLinks.errors.forbidden, component: Forbidden },
    { path: routeLinks.errors.notFound, component: NotFound },
];

export const customerRoutes: Array<RouteItem> = [
    { path: routeLinks.cart.root, component: CartPage, onlyForRoles: [Roles.consumer] },
    { path: routeLinks.account.profile, component: AccountProfilePage, onlyForRoles: [Roles.consumer] },
    { path: routeLinks.orders.makeOrder, component: MakeOrderPage, onlyForRoles: [Roles.consumer] },
    { path: routeLinks.astronomicalCalculator.root, component: AstronomicalCalculatorPage, onlyForRoles: [Roles.consumer] },
];

export const administratorRoutes: Array<RouteItem> = [
    { path: routeLinks.orders.root, component: OrdersPage, onlyForRoles: [Roles.manager, Roles.staff] },
    { path: routeLinks.account.staffProfile, component: StaffProfile, onlyForRoles: [Roles.staff] },
    { path: routeLinks.account.managerProfile, component: ManagerProfile, onlyForRoles: [Roles.manager] },
    { path: routeLinks.catalog.createProduct, component: CreateProductPage, onlyForRoles: [Roles.staff] },
    { path: routeLinks.catalog.editProduct, component: CreateProductPage, onlyForRoles: [Roles.staff] },
];

export const internalRoutes: Array<RouteItem> = [
];