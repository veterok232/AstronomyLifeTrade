import { EntityModel } from "../common/entityModel";
import { CategoryType } from "../enums/categoryType";

export interface BrandModel extends EntityModel {
    name: string;
    categoryType: CategoryType;
}