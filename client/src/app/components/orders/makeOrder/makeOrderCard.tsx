import React from "react";
import { Button, Col, Row } from "reactstrap";
import { LabeledField } from "../../common/presentation/labeledField";
import { CardPrice } from "../../common/presentation/cardPrice";
import { CurrencyType } from "../../../dataModels/enums/currencyType";
import { Local } from "../../localization/local";
import { showNotificationIfInvalid } from "../../common/controls/validation/formValidators";
import { Money } from "../../common/presentation/money";

interface Props {
    quantity: number;
    shortTotalAmount: number;
    totalAmount: number;
    isValid: boolean;
    deliveryAmount?: number;
    promoDiscountPercent?: number;
    promoDiscountAmount?: number;
}

export const MakeOrderCard = (props: Props) => {
    return (<>
        <Row className="make-order-card-section">
            <Col>
                <LabeledField labelKey={"Quantity"} value={`${props.quantity} шт.`} isHorizontalView />
                <LabeledField labelKey={"TotalAmount_Short"} value={<Money amount={props.shortTotalAmount} />} isHorizontalView />
            </Col>
        </Row>
        <hr/>
        {props.deliveryAmount &&
            <Row className="make-order-card-section">
                <Col>
                    <LabeledField labelKey={"DeliveryAmount"} value={<Money amount={props.deliveryAmount} />} isHorizontalView />
                </Col>
            </Row>
        }
        {props.promoDiscountPercent &&
            <Row className="make-order-card-section">
                <Col>
                    <LabeledField labelKey={"PromotionDiscount"} value={<Money amount={props.promoDiscountAmount} discountPercent={props.promoDiscountPercent * 100} />} isHorizontalView />
                </Col>
            </Row>
        }
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