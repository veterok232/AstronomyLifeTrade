import React from "react";
import { localizer } from "../../localization/localizer";

interface Props {
    amount: number;
    className?: string;
    useGrouping?: boolean;
    discountPercent?: number;
}

export function Money(props: Props) {
    const percentValue = props.discountPercent ? ` (${props.discountPercent}%)` : "";

    return <span className={props.className}>{`${localizer.formatMoney(props.amount, props.useGrouping)} руб.${percentValue}`}</span>;
}