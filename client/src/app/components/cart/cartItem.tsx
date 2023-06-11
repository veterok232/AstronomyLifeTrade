import React, { useEffect, useState } from "react";
import { Col, Row } from "reactstrap";
import { CartItem } from "../../dataModels/cart/cartItem";
import { IconicRemoveCartItemButton } from "../common/controls/buttons/IconicRemoveCartItemButton";
import { CardPrice } from "../common/presentation/cardPrice";
import { CurrencyType } from "../../dataModels/enums/currencyType";
import { IntegerUpDownFormControl } from "../common/controls/formControls/maskedFormControls/integerUpDownFormControl";
import { Money } from "../common/presentation/money";
import { isEmpty } from "lodash";
import { ProductImage } from "../layout/catalog/productImage";
import { Link } from "react-router-dom";
import { getLinkToProductDetails } from "../../api/catalog/catalogApi";

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
        <Row className="cart-item w-100 mx-1">
            <Col className="col-2 my-auto p-3">
                <ProductImage
                    className="cart-item-image"
                    productId={props.item.product.productId}
                    productImageId={!isEmpty(props.item.product.imageFilesIds) && props.item.product.imageFilesIds[0]} />
            </Col>
            <Col className="col-4 my-auto">
                <Row className="my-auto">
                    <Col className="col-10 my-auto pr-1">
                        <Link className="text-secondary" to={getLinkToProductDetails(props.item.product.productId)}>
                            {props.item.product.name}
                        </Link>
                    </Col>
                    <Col className="col-2 my-auto pl-0"><IconicRemoveCartItemButton className="p-0" onClick={props.onRemoveItem} /></Col>
                </Row>
            </Col>
            <Col className="col-2 m-auto">
                <CardPrice className="mx-auto" value={props.item.product.price} currency={CurrencyType.BYN} />
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