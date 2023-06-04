import React from "react";
import { Local } from "../../localization/local";

interface Props {
    className?: string;
    localizationKey?: string;
}

export const NoData = (props: Props) => {
    return (
        <div className={`text-center ${props.className || ""}`}>
            <Local id={`${props.localizationKey ? props.localizationKey : "NoInformationToDisplay"}`} />
        </div>
    );
};