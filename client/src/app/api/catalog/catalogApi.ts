import { BrandModel } from "../../dataModels/catalog/brandModel";
import { GetProductsResult } from "../../dataModels/catalog/getProductsResult";
import { ProductRating } from "../../dataModels/catalog/product/productRating";
import { ProductDetails } from "../../dataModels/catalog/productDetails/productDetails";
import { ProductListItem } from "../../dataModels/catalog/productListItem";
import { TelescopesSearchRequest } from "../../dataModels/catalog/productsSearchRequest";
import { Result } from "../../dataModels/common/result";
import { stringifyObjectToQueryString } from "../../utils/requestParameterUtils";
import { httpGet, httpPost } from "../core/requestApi";

const resourceName = "catalog";

export async function searchTelescopes(searchRequest: TelescopesSearchRequest): Promise<GetProductsResult> {
    return httpGet({
        url: `${resourceName}/telescopes?${stringifyObjectToQueryString(searchRequest)}`,
        isAnonymous: true,
    });
}

export async function getPopularProducts(): Promise<ProductListItem[]> {
    return httpGet({
        url: `${resourceName}/get-popular-products`,
        isAnonymous: true,
    });
}

export async function getProductRating(productId: string): Promise<ProductRating> {
    return httpGet({
        url: `${resourceName}/get-product-rating/${productId}`,
    });
}

export async function getProductDetails(productId: string): Promise<ProductDetails> {
    return httpGet({
        url: `${resourceName}/product-details/${productId}`,
    });
}

export async function getBrands(): Promise<BrandModel[]> {
    return httpGet({
        url: `${resourceName}/get-brands`,
    });
}

export async function addProductToFavorites(productId: string): Promise<Result<boolean>> {
    return httpPost({
        url: `${resourceName}/add-to-favorites`,
        body: productId,
    });
}

export const getLinkToProductDetails = (productId: string): string => {
    return `/${resourceName}/product-details/${productId}`;
};