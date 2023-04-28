import React from "react";
import { viewStore } from "../../../infrastructure/stores/viewStore";
import { AppIcon } from "../../common/controls/appIcon";

function MenuToggle(props: MenuToggleProps) {
    return <AppIcon className={props.className} icon="menu" onClick={() => viewStore.toggleNavigation()}/>;
}

interface MenuToggleProps {
    className?: string;
}

export default MenuToggle;