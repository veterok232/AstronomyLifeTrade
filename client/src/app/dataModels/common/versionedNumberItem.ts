import { NumberItem } from "./numberItem";

export interface VersionedNumberItem extends NumberItem {
    version?: string;
}