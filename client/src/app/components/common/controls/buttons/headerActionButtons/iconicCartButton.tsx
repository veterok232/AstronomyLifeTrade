import React from "react";
import { Button } from "reactstrap";
import { AppIcon } from "../../appIcon";

interface Props {
    className?: string;
    onCartOpen: () => void;
}

export function IconicCartButton(props: Props) {
    return (<Button color="link" onClick={props.onCartOpen}>
        <AppIcon className="d-block" icon="shopping_cart" />
    </Button>);
}