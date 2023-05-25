import { CartItem } from "./cartItem";

export interface Cart {
    cartItems: CartItem[];
    totalAmount: number;
    quantity: number;

}