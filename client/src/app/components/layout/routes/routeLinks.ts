export const originRouteParamName = "origin_url";

export const routeLinks = {
    root: "/",
    news: "/news",
    account: {
        login: "/account/login",
        register: "/account/register",
        profile: "/account/profile",
        managerProfile: "/account/manager-profile",
        setupPassword: "/account/setup-password/:token",
        selectAssignment: "/account/select-assignments",
        forgotPassword: "/account/forgot-password",
    },
    errors: {
        forbidden: "/errors/forbidden",
        notFound: "/errors/not-found",
    },
    catalog: {
        root: "/catalog",
        telescopes: {
            category: "/catalog/telescopes",
        },
        binoculars: "/catalog/binoculars",
        accessories: "catalog/accessories",
        productDetails: "catalog/product-details/:productId",
    },
    cart: {
        root: "/cart",
    },
    orders: {
        root: "/orders",
        makeOrder: "orders/make-order",
        orderDetails: "/orders/order-details/:orderId",
    },
    astronomicalCalculator: {
        root: "/astronomical-calculator",
    },
};