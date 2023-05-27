import React, { useState, useEffect } from "react";
import { Row, Col } from "reactstrap";
import { CartItem } from "../../../dataModels/cart/cartItem";
import { CurrencyType } from "../../../dataModels/enums/currencyType";
import { IconicRemoveCartItemButton } from "../../common/controls/buttons/IconicRemoveCartItemButton";
import { CardPrice } from "../../common/presentation/cardPrice";
import { Money } from "../../common/presentation/money";
import { LabeledField } from "../../common/presentation/labeledField";

interface Props {
    item: CartItem;
    ind: number;
    onRemoveItem: () => Promise<void>;
}

export const OrderItemElement = (props: Props) => {
    const [totalAmount, setTotalAmount] = useState<number>(0);

    useEffect(() => {
        setTotalAmount(props.item.product.price * props.item.quantity);
    }, [props.item.product.price, props.item.quantity]);

    return (
        <Row className="cart-item w-100 mx-1">
            <Col className="col-2 my-auto p-3">
                <img className="cart-item-image" src="static/images/products/1.jpg"/>
            </Col>
            <Col className="col-3 my-auto">
                <Row className="my-auto">
                    <Col className="col-10 my-auto pr-1">{props.item.product.name}</Col>
                </Row>
            </Col>
            <Col className="col-2 m-auto">
                <LabeledField
                    labelKey={"Price"}
                    value={<CardPrice className="p-1 mx-auto" value={props.item.product.price} currency={CurrencyType.BYN} />} />
            </Col>
            <Col className="col-2 m-auto">
                <LabeledField
                    labelKey={"Quantity"}
                    value={props.item.quantity} />
            </Col>
            <Col className="col-2 m-auto">
                <LabeledField
                    labelKey={"TotalAmount"}
                    value={<Money className="mx-auto" amount={totalAmount} />} />
            </Col>
            <Col className="col-1 my-auto pl-0">
                <IconicRemoveCartItemButton className="p-0" onClick={props.onRemoveItem} />
            </Col>
        </Row>
    );
};