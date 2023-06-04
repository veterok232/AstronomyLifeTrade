import React from "react";
import { CartItem } from "../../dataModels/cart/cartItem";
import { withParent } from "../common/controls/formControls/formControlsDecorators";
import { NoData } from "../common/presentation/noData";
import { CartItemElement } from "./cartItem";
import { FieldArray } from "react-final-form-arrays";
import { Row } from "reactstrap";

interface Props {
    cartItems: CartItem[];
    onRemoveItem: (index: number) => Promise<void>;
    onChangeItemQuantity: (index: number, quantity: number) => Promise<void>;
}

export const CartItemsControl = (props: Props) => {
    if (!props.cartItems) {
        return <NoData />;
    }

    return (
        <FieldArray name="cart.cartItems" className="m-0 w-100">
            {({ fields }) => (
                <Row className="px-2 pb-2">
                    {fields.map((name, i) => (
                        withParent(CartItemElement, name, {
                            item: props.cartItems[i],
                            ind: i,
                            onRemoveItem: async () => {
                                await props.onRemoveItem(i);
                                fields.remove(i);
                            },
                            onChangeItemQuantity: props.onChangeItemQuantity,
                        }
                    )))}
                </Row>
            )}
        </FieldArray>
    );
};

{/* <Row className="m-0 w-100">
    {fields.cart?.cartItems?.length > 0
        ? values.cart.cartItems.map((cartItem, ind) =>
            <CartItemElement
                key={ind}
                ind={ind}
                item={cartItem}
                onRemoveItem={async () => await onRemoveItem(ind)} />)
        : <NoData />}
</Row> */}