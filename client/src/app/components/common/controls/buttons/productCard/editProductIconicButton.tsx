import React from "react";
import { Button } from "reactstrap";
import { AppIcon } from "../../appIcon";

interface Props {
    onClick: () => void;
}

export function EditProductIconicButton(props: Props) {
    return (<Button color="link" onClick={props.onClick}>
        <AppIcon className="d-block" icon="edit" />
    </Button>);
}