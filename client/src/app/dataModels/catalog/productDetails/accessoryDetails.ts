import { AccessoryType } from "../../enums/accessory/accessoryType";
import { ProductCharacteristics } from "./productCharacteristics";
import { ProductDetails } from "./productDetails";

export interface AccessoryDetails extends ProductDetails, ProductCharacteristics {
    accessoryType?: AccessoryType;
}