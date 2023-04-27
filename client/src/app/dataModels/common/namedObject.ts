import { EntityModel } from "./entityModel";

export interface NamedObject extends EntityModel {
    name: string;
}