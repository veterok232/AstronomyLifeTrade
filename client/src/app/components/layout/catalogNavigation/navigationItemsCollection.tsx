import { observer } from "mobx-react-lite";
import { MenuItem } from "../../../dataModels/menu/menuItem";
import { NavigationItem } from "./catalogNavigationItem";
import React from "react";

interface NavigationItemsCollectionProps {
    items: Array<MenuItem>;
}

export const NavigationItemsCollection = observer((props: NavigationItemsCollectionProps): JSX.Element => {
    return (<>
        {props.items.map((i, ind) => <NavigationItem key={ind} item={i} />)}
    </>);
});