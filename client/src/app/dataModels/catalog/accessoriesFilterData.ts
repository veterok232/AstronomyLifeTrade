import { AccessoryType } from "../enums/accessory/accessoryType";

export interface AccessoriesFilterData {
    brandsIds?: string[];
    accessoryTypes?: AccessoryType[];
    priceMin?: number;
    priceMax?: number;
}