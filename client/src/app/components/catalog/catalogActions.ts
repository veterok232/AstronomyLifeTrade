import { addProductToCart } from "../../api/cart/cartApi";
import { addProductToFavorites } from "../../api/catalog/catalogApi";
import { isConsumer } from "../../infrastructure/services/auth/authService";
import { sharedHistory } from "../../infrastructure/sharedHistory";
import { getRoute } from "../../utils/routeUtils";
import { routeLinks } from "../layout/routes/routeLinks";
import { notifications } from "../toast/toast";

export const onAddToCart = async (productId: string) => {
    if (!isConsumer()) {
        sharedHistory.push(getRoute(routeLinks.account.login));
        return;
    }

    await addProductToCart(productId);
    notifications.localizedSuccess("AddToCartSuccess");
};

export const onAddToFavorites = async (productId: string) => {
    if (!isConsumer()) {
        sharedHistory.push(getRoute(routeLinks.account.login));
        return;
    }

    const result = await addProductToFavorites(productId);

    if (!result.isSucceeded) {
        notifications.localizedError("AddToFavoritesError");
    }
};