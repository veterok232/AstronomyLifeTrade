import {
    CreateProductCharacteristicsModel,
    CreateProductModel,
    EditProductModel,
    ProductForEditModel
} from "../../components/catalog/management/createProduct/createProductPage";
import { AccessoriesSearchRequest } from "../../dataModels/catalog/accessoriesSearchRequest";
import { BinocularsSearchRequest } from "../../dataModels/catalog/binocularsSearchRequest";
import { BrandModel } from "../../dataModels/catalog/brandModel";
import { CategoryModel } from "../../dataModels/catalog/categoryModel";
import { GetProductsResult } from "../../dataModels/catalog/getProductsResult";
import { ProductRating } from "../../dataModels/catalog/product/productRating";
import { ProductDetails } from "../../dataModels/catalog/productDetails/productDetails";
import { ProductListItem } from "../../dataModels/catalog/productListItem";
import { ProductsSearchRequest } from "../../dataModels/catalog/productsSearchRequest";
import { TelescopesSearchRequest } from "../../dataModels/catalog/telescopesSearchRequest";
import { Result } from "../../dataModels/common/result";
import { objToFormData } from "../../utils/formUtils";
import { stringifyObjectToQueryString } from "../../utils/requestParameterUtils";
import { httpGet, httpPost } from "../core/requestApi";

const resourceName = "catalog";

export async function searchTelescopes(searchRequest: TelescopesSearchRequest): Promise<GetProductsResult> {
    return httpGet({
        url: `${resourceName}/telescopes?${stringifyObjectToQueryString(searchRequest)}`,
        isAnonymous: true,
    });
}

export async function searchBinoculars(searchRequest: BinocularsSearchRequest): Promise<GetProductsResult> {
    return httpGet({
        url: `${resourceName}/binoculars?${stringifyObjectToQueryString(searchRequest)}`,
        isAnonymous: true,
    });
}

export async function searchAccessories(searchRequest: AccessoriesSearchRequest): Promise<GetProductsResult> {
    return httpGet({
        url: `${resourceName}/accessories?${stringifyObjectToQueryString(searchRequest)}`,
        isAnonymous: true,
    });
}

export async function searchProducts(searchRequest: ProductsSearchRequest): Promise<GetProductsResult> {
    return httpGet({
        url: `${resourceName}/search?${stringifyObjectToQueryString(searchRequest)}`,
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

export async function getCategories(): Promise<CategoryModel[]> {
    return httpGet({
        url: `${resourceName}/get-categories`,
    });
}

export async function addProductToFavorites(productId: string): Promise<Result<boolean>> {
    return httpPost({
        url: `${resourceName}/add-to-favorites`,
        body: productId,
    });
}

export async function createProduct(data: CreateProductModel): Promise<Result<string>> {
    return httpPost({
        url: `${resourceName}/create-product`,
        body: objToFormData(data),
    });
}

export async function createProductCharacteristics(data: CreateProductCharacteristicsModel) {
    return httpPost({
        url: `${resourceName}/create-product-characteristics`,
        body: data,
        disableSuccessfulToast: true,
    });
}

export async function editProduct(data: EditProductModel): Promise<Result<string>> {
    return httpPost({
        url: `${resourceName}/edit-product`,
        body: objToFormData(data),
    });
}

export async function editProductCharacteristics(data: CreateProductCharacteristicsModel) {
    return httpPost({
        url: `${resourceName}/edit-product-characteristics`,
        body: data,
        disableSuccessfulToast: true,
    });
}

export async function deleteProduct(productId: string): Promise<Result> {
    return httpPost({
        url: `${resourceName}/delete-product/${productId}`,
    });
}

export async function getProductForEdit(productId: string): Promise<ProductForEditModel> {
    return httpGet({
        url: `${resourceName}/product-for-edit/${productId}`,
    });
}

export const getLinkToProductDetails = (productId: string): string => {
    return `/${resourceName}/product-details/${productId}`;
};