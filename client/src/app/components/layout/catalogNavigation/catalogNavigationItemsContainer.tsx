import { observer } from "mobx-react-lite";
import { NavigationItemType } from "../../../dataModels/enums/navigationItemType";
import { MenuItem } from "../../../dataModels/menu/menuItem";
import { useEffect, useState } from "react";
import { viewStore } from "../../../infrastructure/stores/viewStore";
import { sharedHistory } from "../../../infrastructure/sharedHistory";
import { getSelectedBranchText, isNodeBelongToPath, isNodeOnRootLevel } from "./catalogNavigationBranchBuilder";
import { localizer } from "../../localization/localizer";
import React from "react";
import { NavigationItemContent } from "./catalogNavigationItemContent";
import { Collapse } from "reactstrap";
import { AppIcon } from "../../common/controls/appIcon";
import { getConditionalText } from "../../../utils/formattingUtils";

interface NavigationItemsContainerProps {
    titleKey: string;
    iconName?: string;
    rootFor: string[];
    childItems?: MenuItem[];
}

const getFirstAvailableLinkChild = (items: MenuItem[]): MenuItem => {
    const item = items[0];

    return item.type === NavigationItemType.Link
        ? item
        : getFirstAvailableLinkChild(item.childItems);
};

export const NavigationItemsContainer = observer((props: React.PropsWithChildren<NavigationItemsContainerProps>) => {
    const [isOpened, setIsOpened] = useState(false);

    const toggle = () => setIsOpened(!isOpened);

    const onClick = () => {
        if (!viewStore.isNavigationOpened) {
            const link = getFirstAvailableLinkChild(props.childItems);
            sharedHistory.push(link.to);
            viewStore.setSelectedPath(link.rootFor[0]);
            setIsOpened(true);
            viewStore.toggleNavigation();
        } else {
            toggle();
        }
    };

    const isSelected = isNodeBelongToPath(props.rootFor);

    useEffect(() => {
        if (!isOpened) {
            setIsOpened(isSelected);
        }
    }, [isSelected, isOpened]);

    const isRootLevel = isNodeOnRootLevel(props.rootFor);

    const getTitleText = () => {
        if (viewStore.isNavigationOpened) {
            return undefined;
        }

        return (isSelected && isNodeOnRootLevel(props.rootFor))
            ? getSelectedBranchText()
            : localizer.get(props.titleKey);
    };

    return (<>
        <section onClick={onClick}
            title={getTitleText()}
            className={`navigation__item text-nowrap container ${getConditionalText(isSelected, "selected")} ${getConditionalText(isRootLevel, "root-level")}`}>
            <NavigationItemContent {...props} isRootLevel={isRootLevel} />
            {viewStore.isNavigationOpened && <AppIcon className="toggler" icon={isOpened ? "keyboard_arrow_up" : "keyboard_arrow_down"} />}
        </section>
        <Collapse className="ml-4" isOpen={isOpened}>
            {props.children}
        </Collapse>
    </>);
});