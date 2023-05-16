import { addProductToCart, addProductToFavorites } from "../../api/catalog/catalogApi";
import { notifications } from "../toast/toast";

export const onAddToCart = async (productId: string) => {
    const result = await addProductToCart(productId);

    if (!result.isSucceeded) {
        notifications.localizedError("AddToCartError");
    }
};

export const onAddToFavorites = async (productId: string) => {
    const result = await addProductToFavorites(productId);

    if (!result.isSucceeded) {
        notifications.localizedError("AddToCartError");
    }
};