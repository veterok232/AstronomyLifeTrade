import { observer } from "mobx-react-lite";
import React from "react";
import { viewStore } from "../../../infrastructure/stores/viewStore";
import { Local } from "../../localization/local";
import MenuToggle from "../menuToggle/menuToggle";
import { NavigationItemsCollection } from "./navigationItemsCollection";
import { getMenuItems } from "./catalogNavigationConfig";

const CatalogNavigation = observer(() => {
    return <nav className={`grid__navigation navigation ${viewStore.isNavigationOpened ? "grid__navigation--opened navigation--opened" : ""}`}>
        <div className="navigation__header">
            <img className="navigation__logo" src="static/images/logo.svg" />
            <MenuToggle className="navigation__menu-toggle" />
        </div>
        <h4 className="navigation__title"><Local id="BelmontTagline" /></h4>
        <NavigationItemsCollection items={getMenuItems()} />
    </nav>;
});

export default CatalogNavigation;