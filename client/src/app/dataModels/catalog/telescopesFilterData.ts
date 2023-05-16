import { MountingType } from "../enums/telescope/mountingType";
import { TelescopeControlType } from "../enums/telescope/telescopeControlType";
import { TelescopeType } from "../enums/telescope/telescopeType";
import { TelescopeUserLevel } from "../enums/telescope/telescopeUserLevel";

export interface TelescopesFilterData {
    brandsIds?: string[];
    priceMin?: number;
    priceMax?: number;
    userLevels?: TelescopeUserLevel[];
    mountingTypes?: MountingType[];
    telescopeControlTypes?: TelescopeControlType[];
    telescopeTypes?: TelescopeType[];
}