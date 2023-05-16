import { ProductListItem } from "./productListItem";

export interface GetProductsResult {
    totalCount: number;
    items: Array<ProductListItem>;
}