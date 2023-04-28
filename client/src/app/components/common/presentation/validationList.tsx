import React from "react";

interface Props {
    showValidations: boolean;
    areWarnings?: boolean;
    validations: string | Array<string>;
    className?: string;
}

export function ValidationList(props: Props) {
    if (!props.showValidations) {
        return null;
    }

    return (<>
        {(props.validations instanceof Array ? props.validations : [props.validations]).map((error: string, key: number) =>
            <span key={key} className={`${props.className || ""}
                validation-${props.areWarnings ? "warning" : "error"}`}>{error}</span>)}
    </>);
}