export const originRouteParamName = "origin_url";

export const routeLinks = {
    root: "/",
    login: "/login",
    news: "/news",
    account: {
        setupPassword: "/account/setup-password/:token",
        selectAssignment: "/account/select-assignments",
        forgotPassword: "/account/forgot-password",
        registerConsumer: "/account/register-consumer",
    },
    errors: {
        forbidden: "/errors/forbidden",
        notFound: "/errors/not-found",
    },
};