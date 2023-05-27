import React from "react";
import { FieldArray } from "react-final-form-arrays";
import { CartItem } from "../../../dataModels/cart/cartItem";
import { withParent } from "../../common/controls/formControls/formControlsDecorators";
import { NoData } from "../../common/presentation/noData";
import { OrderItemElement } from "./orderItemElement";
import { Row } from "reactstrap";

interface Props {
    cartItems: CartItem[];
    onRemoveItem: (index: number) => Promise<void>;
}

export const OrderItemsControl = (props: Props) => {
    if (!props.cartItems) {
        return <NoData />;
    }

    return (
        <FieldArray name="cart.cartItems" className="m-0 w-100">
            {({ fields }) => (
                <Row className="p-2">
                    {fields.map((name, i) => (
                        withParent(OrderItemElement, name, {
                            item: props.cartItems[i],
                            ind: i,
                            onRemoveItem: async () => {
                                await props.onRemoveItem(i);
                                fields.remove(i);
                            },
                        }
                    )))}
                </Row>
            )}
        </FieldArray>
    );
};