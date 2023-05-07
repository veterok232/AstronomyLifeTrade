import { Navigate, Route, Routes, useLocation } from "react-router-dom";
import { isAuthorizedAsOneOf } from "../../infrastructure/services/auth/authService";
import { routeLinks } from "./routes/routeLinks";
import { RouteItem, commonRoutes, internalRoutes, mainMenuRoutes } from "./routes/routes";
import React, { useEffect } from "react";
import { viewStore } from "../../infrastructure/stores/viewStore";
import { getDefaultPageRoute } from "../../utils/routeUtils";

const routesWithRedirectToDefaultPage = [
    routeLinks.login,
    routeLinks.root,
];

const isForbidden = (routeItem: RouteItem) => {
    return routeItem.onlyForRoles && !isAuthorizedAsOneOf(routeItem.onlyForRoles);
};

const ProtectedComponent = (props: RouteItem) => {
    return isForbidden(props)
        ? <Navigate replace to={routeLinks.errors.forbidden} />
        : <props.component />;
};

export const MainContent = () => {
    const routes = internalRoutes.concat(mainMenuRoutes).concat(commonRoutes);
    const location = useLocation();

    useEffect(() => {
        if (location.pathname !== routeLinks.errors.forbidden) {
            viewStore.setSelectedPath(location.pathname);
        }
    }, [location]);

    return (
        <main className="grid__main main">
            <Routes>
                {routes.map((route, key) => <Route {...route} key={key} element={<ProtectedComponent {...route} />} />)}
                {routesWithRedirectToDefaultPage.map((route, key) =>
                    <Route path={route} key={key} element={<Navigate replace to={getDefaultPageRoute()} />} />)}
                <Route path="*" element={<Navigate replace to={routeLinks.errors.notFound} />} />
            </Routes>
        </main>
    );
};