import { ProductRating } from "./product/productRating";

export interface Comment {
    text: string;
    rating: ProductRating;
    createdAt: Date;
    userName: string;
    userLastName: string;
}