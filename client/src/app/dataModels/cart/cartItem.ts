import { ProductListItem } from "../catalog/productListItem";

export interface CartItem {
    product: ProductListItem;
    quantity: number;
}