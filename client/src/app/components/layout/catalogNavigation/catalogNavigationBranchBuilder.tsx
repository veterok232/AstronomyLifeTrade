import { MenuItem } from "../../../dataModels/menu/menuItem";
import { viewStore } from "../../../infrastructure/stores/viewStore";
import { localizer } from "../../localization/localizer";
import { getCatalogMenuItems } from "./catalogNavigationConfig";

const branchTextDelimiter = " -> ";

export function isNodeBelongToPath(rootFor: string[]): boolean {
    return rootFor.some(root => viewStore.selectedNavigationPath?.startsWith(root));
}

export function isNodeOnRootLevel(rootFor: string[]): boolean {
    return getCatalogMenuItems().some(i => i.rootFor.every(root => rootFor.includes(root)));
}

export function getSelectedBranchText(): string {
    return getNodeTitleKeysOnTheBranch(getCatalogMenuItems())
        .map(k => localizer.get(k))
        .join(branchTextDelimiter);
}

function getNodeTitleKeysOnTheBranch(menuItems: MenuItem[]): string[] {
    const nodeTitleKeys: Array<string> = [];
    for (const menuItem of menuItems) {
        if (menuItem.rootFor.some(root => viewStore.selectedNavigationPath.startsWith(root))) {
            nodeTitleKeys.push(menuItem.titleKey);
            if (menuItem.childItems?.length) {
                nodeTitleKeys.push(...getNodeTitleKeysOnTheBranch(menuItem.childItems));
            }

            break;
        }
    }

    return nodeTitleKeys;
}