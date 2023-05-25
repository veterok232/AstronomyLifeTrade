import { BrandModel } from "./brandModel";
import { CategoryModel } from "./categoryModel";
import { CharacteristicModel } from "./characteristicModel";
import { ProductRating } from "./product/productRating";

export interface ProductListItem {
    productId: string;
    brand: BrandModel;
    category: CategoryModel;
    imageFileId: string;
    price: number;
    quantity: number;
    name: string;
    shortDescription: string;
    specialNote: string;
    characteristics: CharacteristicModel[];
    rating: ProductRating;
}