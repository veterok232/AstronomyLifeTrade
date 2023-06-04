import { ProductListItem } from "../catalog/productListItem";

export interface OrderItem {
    id: string;
    quantity: number;
    createdAt: Date;
    product: ProductListItem;
}