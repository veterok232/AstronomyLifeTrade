import React, { useEffect, useState } from "react";
import { Col, Row } from "reactstrap";
import { CartItem } from "../../dataModels/cart/cartItem";
import { IconicRemoveCartItemButton } from "../common/controls/buttons/IconicRemoveCartItemButton";
import { CardPrice } from "../common/presentation/cardPrice";
import { CurrencyType } from "../../dataModels/enums/currencyType";
import { IntegerUpDownFormControl } from "../common/controls/formControls/maskedFormControls/integerUpDownFormControl";
import { Money } from "../common/presentation/money";

interface Props {
    item: CartItem;
    ind: number;
    onRemoveItem: () => Promise<void>;
    onChangeItemQuantity: (index: number, quantity: number) => Promise<void>;
}

export const CartItemElement = (props: Props) => {
    const [totalAmount, setTotalAmount] = useState<number>(0);

    useEffect(() => {
        setTotalAmount(props.item.product.price * props.item.quantity);
    }, [props.item.product.price, props.item.quantity]);

    return (
        <Row className="cart-item w-100">
            <Col className="col-2 my-auto">
                <img className="cart-item-image" src="static/images/products/1.jpg"/>
            </Col>
            <Col className="col-4 my-auto">
                <Row className="my-auto">
                    <Col className="col-10 my-auto pr-1">{props.item.product.name}</Col>
                    <Col className="col-2 my-auto pl-0"><IconicRemoveCartItemButton className="p-0" onClick={props.onRemoveItem} /></Col>
                </Row>
            </Col>
            <Col className="col-2 m-auto">
                <CardPrice className="p-1 mx-auto" value={props.item.product.price} currency={CurrencyType.BYN} />
            </Col>
            <Col className="col-2 m-auto">
                <IntegerUpDownFormControl minValue={1} maxValue={10} name={"quantity"}
                    onChange={async (val) => await props.onChangeItemQuantity(props.ind, val)}
                    applyChangesOnlyOnBlur={false} />
            </Col>
            <Col className="col-2 m-auto">
                <Money className="mx-auto" amount={totalAmount} />
            </Col>
        </Row>
    );
};