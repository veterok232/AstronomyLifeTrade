import React from "react";
import { FieldArray } from "react-final-form-arrays";
import { Row } from "reactstrap";
import { OrderItem } from "../../../../dataModels/orders/orderItem";
import { withParent } from "../../../common/controls/formControls/formControlsDecorators";
import { NoData } from "../../../common/presentation/noData";
import { OrderDetailsItemElement } from "./orderDetailsItemElement";

interface Props {
    orderItems: OrderItem[];
    onRemoveItem: (index: number) => Promise<void>;
    hideRemoveButton?: boolean;
    closeModal: () => void;
}

export const OrderDetailsItemsControl = (props: Props) => {
    if (!props.orderItems) {
        return <NoData />;
    }

    return (
        <FieldArray name="orderDetails.orderItems" className="m-0 w-100">
            {({ fields }) => (
                <Row className="p-2">
                    {fields.map((name, i) => (
                        withParent(OrderDetailsItemElement, name, {
                            item: props.orderItems[i],
                            ind: i,
                            hideRemoveButton: props.hideRemoveButton,
                            onRemoveItem: async () => {
                                await props.onRemoveItem(i);
                                fields.remove(i);
                            },
                            closeModal: props.closeModal,
                        }
                    )))}
                </Row>
            )}
        </FieldArray>
    );
};