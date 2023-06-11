import React from "react";
import { Button, Col, Row } from "reactstrap";
import { LabeledField } from "../../common/presentation/labeledField";
import { CardPrice } from "../../common/presentation/cardPrice";
import { CurrencyType } from "../../../dataModels/enums/currencyType";
import { Local } from "../../localization/local";
import { showNotificationIfInvalid } from "../../common/controls/validation/formValidators";

interface Props {
    quantity: number;
    totalAmount: number;
    isValid: boolean;
}

export const MakeOrderCard = (props: Props) => {
    return (<>
        <Row className="make-order-card-section">
            <Col>
                <LabeledField labelKey={"Quantity"} value={props.quantity} isHorizontalView />
                <LabeledField labelKey={"TotalAmount_Short"} value={props.totalAmount} isHorizontalView />
            </Col>
        </Row>
        <hr/>
        <Row className="make-order-card-section">
            <Col>
                <LabeledField labelKey={"TotalAmount"}
                    value={<CardPrice className="mx-auto" value={props.totalAmount} currency={CurrencyType.BYN} />} />
            </Col>
        </Row>
        <Row className="make-order-card-section">
            <Col>
                <Button type="submit" className="w-100" onClick={() => showNotificationIfInvalid(props.isValid)}>
                    <Local id="MakeOrder" />
                </Button>
            </Col>
        </Row>
    </>);
};