import { NavigationItemType } from "../../../dataModels/enums/navigationItemType";
import { MenuItem } from "../../../dataModels/menu/menuItem";
import { MenuItemConfig } from "../../../dataModels/menu/menuItemConfig";
import { isAuthorizedAsOneOf } from "../../../infrastructure/services/auth/authService";
import { routeLinks } from "../routes/routeLinks";

export const getCatalogMenuItems = (): Array<MenuItem> => {
    return getUserAccessibleMenuItems(menuItemsConfig);
};

const getUserAccessibleMenuItems = (items: Array<MenuItemConfig>): Array<MenuItem> => {
    const result = new Array<MenuItem>();

    for (const item of items.filter(item => isUserHasAccessToItem(item))) {
        result.push(item.type === NavigationItemType.Link ? { ...item } : createContainerItem(item));
    }

    return result;
};

const createContainerItem = (item: MenuItemConfig) => {
    const childItems = getUserAccessibleMenuItems(item.childItems);

    const newItem = {
        ...item,
        childItems,
    };

    if (childItems.length === 0) {
        newItem.type = NavigationItemType.Link;
        newItem.to = routeLinks.errors.forbidden;
    }

    return newItem;
};

const isUserHasAccessToItem = (item: MenuItemConfig) => {
    return (item.availableForRoles === undefined || isAuthorizedAsOneOf(item.availableForRoles));
};

const menuItemsConfig: Array<MenuItemConfig> = [{
    type: NavigationItemType.Link,
    iconName: "dashboard",
    titleKey: "Telescopes",
    to: routeLinks.catalog.telescopes.category,
    rootFor: [routeLinks.catalog.telescopes.category],
}, {
    type: NavigationItemType.Link,
    iconName: "dashboard",
    titleKey: "Binoculars",
    to: routeLinks.catalog.binoculars,
    rootFor: [routeLinks.catalog.binoculars],
}, {
    type: NavigationItemType.Link,
    iconName: "dashboard",
    titleKey: "Accessories",
    to: routeLinks.catalog.accessories,
    rootFor: [routeLinks.catalog.accessories],
}];