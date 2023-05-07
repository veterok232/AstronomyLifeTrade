import React from "react";
import { Button } from "reactstrap";
import { AppIcon } from "../appIcon";
import { Local } from "../../../localization/local";

interface Props {
    onCancel: () => void;
    labelKey: string;
}

export const BackButton = (props: Props) => {
    return (
        <Button color="link" className="card-action back-button" onClick={props.onCancel}>
            <AppIcon icon="arrow_back" className="align-text-top" />
            <span className="back-button-label"><Local id={props.labelKey} /></span>
        </Button>);
};