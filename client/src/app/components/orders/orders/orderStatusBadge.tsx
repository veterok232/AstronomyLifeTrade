import React from "react";
import { Badge } from "reactstrap";
import { OrderStatus } from "../../../dataModels/enums/orderStatus";
import { localizer } from "../../localization/localizer";
import { OrderStatusColors } from "./orderStatusColors";

interface Props {
    domId?: string;
    orderStatus: OrderStatus;
    labelId?: string;
    className?: string;
}

export const OrderStatusBadge = (props: Props) => {
    const badgeStyle = {
        backgroundColor: OrderStatusColors[props.orderStatus],
    };

    return (
        <Badge
            id={props.domId}
            style={badgeStyle}
            color="primary"
            className={`rounded-pill align-text-bottom px-3 py-1 text-uppercase ui-text-extended ${
                props.className || ""
            }`}
        >
            {props.labelId
                ? localizer.get(props.labelId)
                : localizer.getEnumValue(OrderStatus, props.orderStatus)}
        </Badge>
    );
};