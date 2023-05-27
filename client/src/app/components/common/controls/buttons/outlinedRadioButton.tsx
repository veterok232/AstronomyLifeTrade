import React, { useMemo } from "react";
import { generateElementId } from "../../../../utils/stringUtils";

interface Props {
    className?: string;
    text: string;
}

export const OrderDeliverySection = (props: Props) => {
    const elementId = useMemo(() => generateElementId(), []);

    return (<>
        <input type="radio" className="btn-check" name="options-outlined" id={elementId} />
        <label className="btn btn-outline-primary" htmlFor={elementId}>{props.text}</label>
    </>);
};