import React from "react";
import { CurrencyType } from "../../../dataModels/enums/currencyType";
import { Money } from "./money";

interface Props {
    value: number;
    currency: CurrencyType;
    className?: string;
    showColouredBox?: boolean;
    inlineStyle?: boolean;
}

/* const getCurrencyLabel = (currency: CurrencyType): string => {
    switch (currency) {
        case CurrencyType.BYN:
            return "руб.";
        case CurrencyType.USD:
            return "$";
    }
}; */

export const CardPrice = (props: Props) => {
    return (
        <div className={`${props.showColouredBox ? "price py-1 px-2 ml-2" : "d-inline"} ${props.className}`}>
            <Money className="price-font" amount={props.value} />
        </div>
    );
};