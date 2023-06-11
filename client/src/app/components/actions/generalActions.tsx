import { isConsumer, isManager, isStaff } from "../../infrastructure/services/auth/authService";
import { sharedHistory } from "../../infrastructure/sharedHistory";
import { contextStore } from "../../infrastructure/stores/contextStore";
import { addQueryParameter, getRoute } from "../../utils/routeUtils";
import { routeLinks } from "../layout/routes/routeLinks";
import { notifications } from "../toast/toast";

export const onSearch = (searchString: string) => {
    sharedHistory.push(getRoute(addQueryParameter(
        routeLinks.catalog.search,
        { searchString: searchString })));
};

export const onCartOpen = () => {
    if (!isConsumer()) {
        sharedHistory.push(getRoute(routeLinks.account.login));
        notifications.localizedWarning("NeedToAuthorize");
        return;
    }

    sharedHistory.push(getRoute(routeLinks.cart.root));
};

export const onAstronomicalCalculatorOpen = () => {
    if (!isConsumer()) {
        sharedHistory.push(getRoute(routeLinks.account.login));
        notifications.localizedWarning("NeedToAuthorize");
        return;
    }

    sharedHistory.push(getRoute(routeLinks.astronomicalCalculator.root));
};

export const onFavoritesOpen = () => {
    if (!isConsumer()) {
        sharedHistory.push(getRoute(routeLinks.account.login));
        notifications.localizedWarning("NeedToAuthorize");
        return;
    }
};

export const onUserProfileOpen = () => {
    if (contextStore.isAuthenticated) {
        sharedHistory.push(getRoute(routeLinks.account.profile));
    } else {
        sharedHistory.push(getRoute(routeLinks.account.login));
    }
};

export const onOrdersOpen = () => {
    if (!isManager() && !isStaff()) {
        notifications.localizedWarning("NeedToAuthorize");
        sharedHistory.push(getRoute(routeLinks.account.login));
        return;
    }

    sharedHistory.push(getRoute(routeLinks.orders.root));
};

export const onManagerProfileOpen = () => {
    if (!isManager()) {
        notifications.localizedWarning("NeedToAuthorize");
        sharedHistory.push(getRoute(routeLinks.account.login));
        return;
    }

    sharedHistory.push(getRoute(routeLinks.account.managerProfile));
};

export const onStaffProfileOpen = () => {
    if (!isStaff()) {
        notifications.localizedWarning("NeedToAuthorize");
        sharedHistory.push(getRoute(routeLinks.account.login));
        return;
    }

    sharedHistory.push(getRoute(routeLinks.account.staffProfile));
};