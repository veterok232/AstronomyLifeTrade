import { NavigationItemType } from "../enums/navigationItemType";

export interface MenuItem {
    type: NavigationItemType;
    to?: string;
    rootFor: string[];
    childItems?: MenuItem[];
    iconName?: string;
    titleKey: string;
    isSeparated?: boolean;
}