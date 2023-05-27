import { Cart } from "../../dataModels/cart/cart";
import { CartChangeCountModel } from "../../dataModels/cart/cartChangeCountModel";
import { httpGet, httpPost } from "../core/requestApi";

const resourceName = "cart";

export async function getCart(): Promise<Cart> {
    return httpGet({
        url: `${resourceName}/get`,
    });
}

export async function getProductsInCart(): Promise<string[]> {
    return httpGet({
        url: `${resourceName}/get-products-in-cart`,
    });
}

export async function addProductToCart(productId: string) {
    return httpPost({
        url: `${resourceName}/add`,
        body: { productId },
        disableSuccessfulToast: true,
    });
}

export async function removeProductFromCart(productId: string) {
    return httpPost({
        url: `${resourceName}/remove`,
        body: { productId },
    });
}

export async function changeProductCount(model: CartChangeCountModel) {
    return httpPost({
        url: `${resourceName}/change-count`,
        body: model,
        disableSuccessfulToast: true,
    });
}

export async function clearCart() {
    return httpPost({
        url: `${resourceName}/clear-cart`,
        disableSuccessfulToast: true,
    });
}