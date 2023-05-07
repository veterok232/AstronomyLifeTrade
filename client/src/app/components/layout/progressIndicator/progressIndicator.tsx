import { observer } from "mobx-react-lite";
import { progressIndicatorStore } from "../../../infrastructure/stores/progressIndicatorStore";
import React from "react";
import { Spinner } from "reactstrap";

export const ProgressIndicator = observer(() => {
    if (!progressIndicatorStore.isInProgress) {
        return null;
    }

    return (<div
        className={`progress-indicator-container ${progressIndicatorStore.isElevated ? "elevated-progress-indicator" : ""}`}>
        <Spinner className="indicator" color="primary" />
    </div>);
});