import React, { useState, useEffect } from "react";
import { Row, Col } from "reactstrap";
import { CurrencyType } from "../../../../dataModels/enums/currencyType";
import { OrderItem } from "../../../../dataModels/orders/orderItem";
import { IconicRemoveCartItemButton } from "../../../common/controls/buttons/IconicRemoveCartItemButton";
import { CardPrice } from "../../../common/presentation/cardPrice";
import { LabeledField } from "../../../common/presentation/labeledField";
import { Money } from "../../../common/presentation/money";
import { ProductImage } from "../../../layout/catalog/productImage";
import { isEmpty } from "lodash";

interface Props {
    item: OrderItem;
    ind: number;
    onRemoveItem: () => Promise<void>;
}

export const OrderDetailsItemElement = (props: Props) => {
    const [totalAmount, setTotalAmount] = useState<number>(0);

    useEffect(() => {
        setTotalAmount(props.item.product.price * props.item.quantity);
    }, [props.item.product.price, props.item.quantity]);

    return (
        <Row className="cart-item w-100 mx-1">
            <Col className="col-2 my-auto p-3">
                <ProductImage
                    className="cart-item-image"
                    productId={props.item.product.productId}
                    productImageId={!isEmpty(props.item.product.imageFilesIds) && props.item.product.imageFilesIds[0]} />
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