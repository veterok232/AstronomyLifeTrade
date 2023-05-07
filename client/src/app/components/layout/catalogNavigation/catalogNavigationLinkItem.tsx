import { observer } from "mobx-react-lite";
import { isNodeBelongToPath, isNodeOnRootLevel } from "./catalogNavigationBranchBuilder";
import { NavLink } from "react-router-dom";
import React from "react";
import { viewStore } from "../../../infrastructure/stores/viewStore";
import { localizer } from "../../localization/localizer";
import { getConditionalText } from "../../../utils/formattingUtils";
import { NavigationItemContent } from "./catalogNavigationItemContent";

export interface NavigationLinkItemProps {
    to: string;
    rootFor: string[];
    iconName?: string;
    titleKey: string;
    availableFor?: string[];
}

export const NavigationLinkItem: React.FC<NavigationLinkItemProps> = observer((props: NavigationLinkItemProps) => {
    const isSelected = isNodeBelongToPath(props.rootFor);
    const isRootLevel = isNodeOnRootLevel(props.rootFor);

    return <NavLink
        to={props.to}
        title={viewStore.isNavigationOpened ? undefined : localizer.get(props.titleKey)}
        className={`navigation__item text-nowrap container ${getConditionalText(isSelected, "selected")} ${getConditionalText(isRootLevel, "root-level")}`}
        onClick={() => viewStore.setSelectedPath(props.rootFor[0])}>
        <NavigationItemContent {...props} isRootLevel={isRootLevel} />
    </NavLink>;
});