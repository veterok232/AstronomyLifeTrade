import { FileModel } from "../../fileModel";
import { BrandModel } from "../brandModel";
import { CategoryModel } from "../categoryModel";
import { CharacteristicModel } from "../characteristicModel";
import { Comment } from "../comment";
import { ProductRating } from "../product/productRating";

export interface ProductDetails {
    name: string;
    code: string;
    description: string;
    brand: BrandModel;
    category: CategoryModel;
    price: number;
    manufacturer: string;
    quantity: number;
    equipment: string;
    rating: ProductRating;
    shortDescription: string;
    characteristics: CharacteristicModel[];
    productImagesIds: string[];
    productFiles: FileModel[];
}