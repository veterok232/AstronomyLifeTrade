import React from "react";
import { Button } from "reactstrap";
import { AppIcon } from "../appIcon";

interface Props {
    className?: string;
    onClick: () => void;
}

export function IconicRemoveCartItemButton(props: Props) {
    return (<Button className="" color="link" onClick={props.onClick}>
        <AppIcon className="d-block" icon="cancel" />
    </Button>);
}