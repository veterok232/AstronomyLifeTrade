import React from "react";
import { Button } from "reactstrap";
import { AppIcon } from "../appIcon";

interface Props {
    className?: string;
    onSearch: () => void;
}

export function IconicSearchButton(props: Props) {
    return (<Button className="" color="link" onClick={props.onSearch}>
        <AppIcon className="d-block" icon="search" />
    </Button>);
}