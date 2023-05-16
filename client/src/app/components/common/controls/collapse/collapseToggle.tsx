import React from "react";
import { Label } from "reactstrap";
import { Local } from "../../../localization/local";
import { AppIcon } from "../appIcon";

interface Props {
    state?: [boolean, (val: boolean) => void];
    closedLabelKey: string;
    openedLabelKey: string;
    className?: string;
}

export const CollapseToggle = (props: Props) => {
    const [fullView, toggleFullView] = props.state;

    return (<Label onClick={() => toggleFullView(!fullView)} className={`collapse-toggle text-primary ${props.className || ""}`} >
        <b><Local id={fullView ? props.openedLabelKey : props.closedLabelKey} /></b>
        <AppIcon className="material-icons align-middle" icon={fullView ? "keyboard_arrow_up" : "keyboard_arrow_down"} />
    </Label>);
};