import React from "react";
import { Button } from "reactstrap";
import { AppIcon } from "../../appIcon";

interface Props {
    onFavoritesOpen: () => void;
}

export function IconicFavoritesButton(props: Props) {
    return (<Button color="link" onClick={props.onFavoritesOpen}>
        <AppIcon className="d-block" icon="heart" />
    </Button>);
}