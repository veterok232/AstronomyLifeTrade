import React from "react";
import { Button } from "reactstrap";
import { AppIcon } from "../../appIcon";

interface Props {
    onUserProfileOpen: () => void;
}

export function IconicUserProfileButton(props: Props) {
    return (<Button color="link" onClick={props.onUserProfileOpen}>
        <AppIcon className="d-block" icon="profile" />
    </Button>);
}