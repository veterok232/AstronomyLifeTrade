import { GetProductsResult } from "../../dataModels/catalog/getProductsResult";
import { ProductRating } from "../../dataModels/catalog/product/productRating";
import { ProductListItem } from "../../dataModels/catalog/productListItem";
import { TelescopesSearchRequest } from "../../dataModels/catalog/productsSearchRequest";
import { Result } from "../../dataModels/common/result";
import { stringifyObjectToQueryString } from "../../utils/requestParameterUtils";
import { apiRootUrl, httpGet, httpPost } from "../core/requestApi";

const resourceName = "catalog";

export async function searchTelescopes(searchRequest: TelescopesSearchRequest): Promise<GetProductsResult> {
    return httpGet({
        url: `${resourceName}/telescopes?${stringifyObjectToQueryString(searchRequest)}`,
    });
}

export async function getPopularProducts(): Promise<ProductListItem[]> {
    return httpGet({
        url: `${resourceName}/get-popular-products`,
    });
}

export async function getProductRating(productId: string): Promise<ProductRating> {
    return httpGet({
        url: `${resourceName}/get-product-rating/${productId}`,
    });
}

export async function addProductToCart(productId: string): Promise<Result<boolean>> {
    return httpPost({
        url: `${resourceName}/add-to-cart`,
        body: productId,
    });
}

export async function addProductToFavorites(productId: string): Promise<Result<boolean>> {
    return httpPost({
        url: `${resourceName}/add-to-favorites`,
        body: productId,
    });
}

export const getLinkToProductDetails = (productId: string): string => {
    return `${apiRootUrl}/${resourceName}/product-details/${productId}`;
};