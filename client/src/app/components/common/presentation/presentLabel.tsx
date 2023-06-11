import React from "react";
import { localizer } from "../../localization/localizer";

interface Props {
    className?: string;
    labelKey?: string;
    icon: string;
    color?: string;
}

export const PresentLabel = (props: Props) => {
    return (
        <span className={`not-in-present-label ${props.className}`}>
            <i className="material-icons not-in-present-icon">{props.icon}</i>
            {localizer.get(props.labelKey)}
        </span>
    );
};