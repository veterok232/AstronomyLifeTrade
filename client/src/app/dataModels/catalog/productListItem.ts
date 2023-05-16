import { CharacteristicModel } from "./characteristicModel";
import { ProductRating } from "./product/productRating";

export interface ProductListItem {
    id: string;
    brandId: string;
    categoryId: string;
    imageFileId: string;
    price: number;
    quantity: number;
    name: string;
    shortDescription: string;
    specialNote: string;
    characteristics: CharacteristicModel[];
    rating: ProductRating;
}