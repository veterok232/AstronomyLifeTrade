import React from "react";
import { Button } from "reactstrap";
import { AppIcon } from "../../appIcon";

interface Props {
    className?: string;
    onOrdersOpen: () => void;
}

export function IconicOrdersButton(props: Props) {
    return (<Button color="link" onClick={props.onOrdersOpen}>
        <AppIcon className="d-block" icon="receipt_long" />
    </Button>);
}