import { ProductListItem } from "../catalog/productListItem";

export interface CartItem {
    id: string;
    product: ProductListItem;
    quantity: number;
}