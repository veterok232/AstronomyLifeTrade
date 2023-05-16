import React from "react";
import { CurrencyType } from "../../../dataModels/enums/currencyType";

interface Props {
    value: number;
    currency: CurrencyType;
    className?: string;
}

const getCurrencyLabel = (currency: CurrencyType): string => {
    switch (currency) {
        case CurrencyType.BYN:
            return "руб.";
        case CurrencyType.USD:
            return "$";
    }
};

export const CardPrice = (props: Props) => {
    return (
        <div className="price">
            <span>{props.value} {getCurrencyLabel(props.currency)}</span>
        </div>
    );
};