import React, { useLayoutEffect, useState } from "react";
import { Router as ReactRouter } from "react-router-dom";

export const Router = ({ history, ...props }: any) => {
    const [state, setState] = useState({
        action: history.action,
        location: history.location,
    });

    useLayoutEffect(() => history.listen(setState), [history]);

    return (
        <ReactRouter
            { ...props }
            location={state.location}
            navigationType={state.action}
            navigator={history} />
    );
};