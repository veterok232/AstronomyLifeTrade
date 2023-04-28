import React from "react";
import { Button } from "reactstrap";
import { AppIcon } from "../../appIcon";

interface Props {
    onAstronomicalCalculatorOpen: () => void;
}

export function IconicAstronomicalCalculatorButton(props: Props) {
    return (<Button color="link" onClick={props.onAstronomicalCalculatorOpen}>
        <AppIcon className="d-block" icon="telescope" />
    </Button>);
}