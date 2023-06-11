import { addProductToCart } from "../../api/cart/cartApi";
import { addProductToFavorites, deleteProduct } from "../../api/catalog/catalogApi";
import { isConsumer, isStaff } from "../../infrastructure/services/auth/authService";
import { sharedHistory } from "../../infrastructure/sharedHistory";
import { getRoute } from "../../utils/routeUtils";
import { showConfirmation } from "../common/confirmationModal";
import { routeLinks } from "../layout/routes/routeLinks";
import { localizer } from "../localization/localizer";
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

export const onDeleteProduct = (productId: string) => {
    if (!isStaff()) {
        sharedHistory.push(getRoute(routeLinks.account.login));
        return;
    }

    showConfirmation({
        body: localizer.get("DeleteProductConfirmation_Body"),
        onConfirmClick: async () => {
            const result = await deleteProduct(productId);

            if (!result.isSucceeded) {
                notifications.localizedError("DeleteProductError");
            }
        }
    });
};