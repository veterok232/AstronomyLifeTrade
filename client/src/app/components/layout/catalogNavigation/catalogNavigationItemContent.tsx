import React from "react";
import { AppIcon } from "../../common/controls/appIcon";
import { Local } from "../../localization/local";

interface NavigationItemContentProps {
    iconName?: string;
    titleKey: string;
    isRootLevel: boolean;
}

export const NavigationItemContent = (props: NavigationItemContentProps) => {
    return (<>
        {!props.isRootLevel && <li className="navigation__indent-icon" />}
        {props.iconName && <AppIcon icon={props.iconName} className="navigation__item-icon" />}
        <span className="navigation__item-text"><Local id={props.titleKey} /></span>
    </>);
};