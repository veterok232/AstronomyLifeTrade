import React, { useMemo } from "react";
import { Row, Col, Button } from "reactstrap";
import { CurrencyType } from "../../../dataModels/enums/currencyType";
import { OrderListItem } from "../../../dataModels/orders/orderListItem";
import { CardPrice } from "../../common/presentation/cardPrice";
import { OrderStatusBadge } from "./orderStatusBadge";
import { Local } from "../../localization/local";
import { generateElementId } from "../../../utils/stringUtils";
import { LabeledField } from "../../common/presentation/labeledField";
import { localizer } from "../../localization/localizer";
import { isAuthorizedAsOneOf } from "../../../infrastructure/services/auth/authService";
import { Roles } from "../../../infrastructure/services/auth/roles";

interface Props {
    order: OrderListItem;
    ind: number;
    onShowOrderDetails: () => Promise<void> | void;
}

export const OrderCard = (props: Props) => {
    const statusBadgeId = useMemo(() => generateElementId(), []);

    return (
        <Row className="order-item w-100 mb-3">
            <Col>
                <Row className="order-item-header">
                    <Col className="d-flex align-items-center ml-3 p-0">
                        <span>#{props.order.orderNumber}</span>
                    </Col>
                    <Col className="d-flex align-items-center justify-content-end p-0">
                        <span className="mr-3">{localizer.formatDate(props.order.createdAt)}</span>
                        <OrderStatusBadge className="mr-3" domId={statusBadgeId} orderStatus={props.order.status} />
                    </Col>
                </Row>
                <Row className="ml-2 pb-0">
                    <Col className="col-2 pb-0">
                        <LabeledField className="pb-0" labelKey={"CustomerName"} value={`${props.order.customerFirstName} ${props.order.customerLastName}`} />
                    </Col>
                    <Col className="col-2 pb-0">
                        <LabeledField className="pb-0" labelKey={"OrderAmount"} value={<CardPrice value={props.order.totalAmount} currency={CurrencyType.BYN} />} />
                    </Col>
                    <Col className="col-2 pb-0">
                        <LabeledField className="pb-0" labelKey={"OrderQuantity"} value={`${props.order.quantity} шт.`} />
                    </Col>
                    {isAuthorizedAsOneOf([Roles.manager, Roles.staff]) &&
                        <Col className="col-6 pb-0">
                            <LabeledField className="pb-0" labelKey={"ManagerFullName"} value={props.order.managerFullName ? props.order.managerFullName : "Нет"} />
                        </Col>
                    }
                </Row>
                <hr className="mt-0"/>
                <Row>
                    <Col className="my-auto pl-0 ml-3">
                        <Button className="btn btn-primary" onClick={props.onShowOrderDetails}>
                            <Local id="ShowOrderDetails" />
                        </Button>
                    </Col>
                </Row>
            </Col>
        </Row>
    );
};