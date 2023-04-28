import { NavigationItemType } from "../enums/navigationItemType";

export interface MenuItemConfig {
    type: NavigationItemType;
    to?: string;
    rootFor: string[];
    childItems?: MenuItemConfig[];
    iconName?: string;
    titleKey: string;
    isSeparated?: boolean;
    availableForRoles?: string[];
}