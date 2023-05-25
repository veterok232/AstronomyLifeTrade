import React from "react";
import { localizer } from "../../localization/localizer";

interface Props {
    amount: number;
    className?: string;
    useGrouping?: boolean;
}

export function Money(props: Props) {
    return <span className={props.className}>{`${localizer.formatMoney(props.amount, props.useGrouping)} руб.`}</span>;
}