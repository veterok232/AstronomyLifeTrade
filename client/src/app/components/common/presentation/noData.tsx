import React from "react";
import { Local } from "../../localization/local";

interface Props {
    className?: string;
}

export const NoData = (props: Props) => {
    return (
        <div className={`text-center ${props.className || ""}`}>
            <Local id="NoInformationToDisplay" />
        </div>
    );
};