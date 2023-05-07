import { observer } from "mobx-react-lite";
import { MenuItem } from "../../../dataModels/menu/menuItem";
import React from "react";
import { NavigationItemType } from "../../../dataModels/enums/navigationItemType";
import { NavigationLinkItem } from "./catalogNavigationLinkItem";
import { NavigationItemsContainer } from "./catalogNavigationItemsContainer";
import { NavigationItemsCollection } from "./navigationItemsCollection";

export interface NavigationItemProps {
    item: MenuItem;
}

export const NavigationItem = observer((props: NavigationItemProps): JSX.Element => {
    return (<>
        {props.item.isSeparated && <hr className="items-separator" />}
        {props.item.type == NavigationItemType.Link
            ? <NavigationLinkItem to={props.item.to} {...props.item} />
            : <NavigationItemsContainer {...props.item}>
                <NavigationItemsCollection items={props.item.childItems} />
            </NavigationItemsContainer>}
    </>);
});