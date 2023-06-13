import React, { useMemo } from "react";
import { Row, Col } from "reactstrap";
import { CurrencyType } from "../../../../dataModels/enums/currencyType";
import { generateElementId } from "../../../../utils/stringUtils";
import { CardPrice } from "../../../common/presentation/cardPrice";
import { LabeledField } from "../../../common/presentation/labeledField";
import { localizer } from "../../../localization/localizer";
import { OrderStatusBadge } from "../orderStatusBadge";
import { OrderDetailsModel } from "../../../../dataModels/orders/orderDetailsModel";
import { PaymentMethod } from "../../../../dataModels/enums/paymentMethod";
import { DeliveryType } from "../../../../dataModels/enums/deliveryType";
import { isAuthorizedAsOneOf } from "../../../../infrastructure/services/auth/authService";
import { Roles } from "../../../../infrastructure/services/auth/roles";

interface Props {
    orderDetails: OrderDetailsModel;
    ind: number;
}

export const OrderDetailsCard = (props: Props) => {
    const statusBadgeId = useMemo(() => generateElementId(), []);

    if (!props.orderDetails) {
        return;
    }

    return (
        <Row className="order-details-item w-100 ml-2 mb-4">
            <Col>
                <Row className="order-item-header">
                    <Col className="d-flex align-items-center ml-3 p-0">
                        <span>#{props.orderDetails.orderNumber}</span>
                    </Col>
                    <Col className="d-flex align-items-center justify-content-end p-0">
                        <span className="mr-3">{localizer.formatDate(props.orderDetails.createdAt)}</span>
                        <OrderStatusBadge className="mr-3" domId={statusBadgeId} orderStatus={props.orderDetails.orderStatus} />
                    </Col>
                </Row>
                <Row className="ml-2">
                    <Col className="col-4 pb-0">
                        <LabeledField className="pb-0" labelKey={"CustomerName"} value={`${props.orderDetails.customerFirstName} ${props.orderDetails.customerLastName}`} />
                    </Col>
                    <Col className="col-4 pb-0">
                        <LabeledField className="pb-0" labelKey={"OrderAmount"} value={<CardPrice value={props.orderDetails.totalAmount} currency={CurrencyType.BYN} />} />
                    </Col>
                    <Col className="col-4 pb-0">
                        <LabeledField className="pb-0" labelKey={"OrderQuantity"} value={`${props.orderDetails.quantity} шт.`} />
                    </Col>
                </Row>
                <Row className="ml-2">
                    <Col className="col-4 pb-0">
                        <LabeledField className="pb-0" labelKey={"PaymentMethod"} value={`${localizer.getEnumValue(PaymentMethod, props.orderDetails.paymentMethod)}`} />
                    </Col>
                    <Col className="col-4 pb-0">
                        <LabeledField className="pb-0" labelKey={"DeliveryType"} value={`${localizer.getEnumValue(DeliveryType, props.orderDetails.deliveryType)}`} />
                    </Col>
                    {props.orderDetails.deliveryType === DeliveryType.Courier &&
                        <Col className="col-4 pb-0">
                            <LabeledField className="pb-0" labelKey={"Address"} value={`${props.orderDetails?.address?.city}, ${props.orderDetails?.address?.fullAddress}, ${props.orderDetails?.address?.postalCode}`} />
                        </Col>
                    }
                </Row>
                <Row className="ml-2">
                    <Col className="col-4 pb-0">
                        <LabeledField className="pb-0" labelKey={"CustomerEmail"} value={props.orderDetails.customerEmail} />
                    </Col>
                    <Col className="col-4 pb-0">
                        <LabeledField className="pb-0" labelKey={"CustomerPhoneNumber"} value={`+${props.orderDetails.customerPhoneNumber}`} />
                    </Col>
                </Row>
                {isAuthorizedAsOneOf([Roles.manager, Roles.staff]) &&
                    <Row className="ml-2 pb-0">
                        <Col className="col-2 pb-0">
                            <LabeledField className="pb-0" labelKey={"ManagerFullName"} value={props.orderDetails.managerFullName ? props.orderDetails.managerFullName : "Нет"} />
                        </Col>
                    </Row>
                }
            </Col>
        </Row>
    );
};