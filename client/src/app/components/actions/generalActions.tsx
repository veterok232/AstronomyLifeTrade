import { isConsumer } from "../../infrastructure/services/auth/authService";
import { sharedHistory } from "../../infrastructure/sharedHistory";
import { contextStore } from "../../infrastructure/stores/contextStore";
import { getRoute } from "../../utils/routeUtils";
import { routeLinks } from "../layout/routes/routeLinks";

export const onSearch = () => {

};

export const onCartOpen = () => {
    if (!isConsumer()) {
        sharedHistory.push(getRoute(routeLinks.account.login));
        return;
    }

    sharedHistory.push(getRoute(routeLinks.cart.root));
};

export const onAstronomicalCalculatorOpen = () => {
    if (!isConsumer()) {
        sharedHistory.push(getRoute(routeLinks.account.login));
        return;
    }
};

export const onFavoritesOpen = () => {
    if (!isConsumer()) {
        sharedHistory.push(getRoute(routeLinks.account.login));
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