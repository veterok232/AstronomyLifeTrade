import React, { useState } from "react";
import { Button, Col, Form, ModalBody, Row } from "reactstrap";
import { OrderDetailsModel } from "../../../../dataModels/orders/orderDetailsModel";
import { modalsStore } from "../../../../infrastructure/stores/modalsStore";
import { ModalHeaderControl } from "../../../common/controls/modals/modalHeaderControl";
import { modalsTypes } from "../../../layout/modals/modalsTypes";
import { Local } from "../../../localization/local";
import { OrderDetailsItemsControl } from "./orderDetailsItemsControl";
import { Form as FinalForm, FormRenderProps } from "react-final-form";
import arrayMutators from "final-form-arrays";
import useAsyncEffect from "use-async-effect";
import { approveOrder, cancelOrder, closeOrder, getOrderDetails, postponeOrder, removeOrderItem } from "../../../../api/orders/ordersApi";
import { OrderDetailsCard } from "./orderDetailsCard";
import { localizer } from "../../../localization/localizer";
import { OrderStatus } from "../../../../dataModels/enums/orderStatus";
import { showConfirmation } from "../../../common/confirmationModal";

interface Props {
    orderId: string;
}

interface OrderDetailsFormData {
    orderDetails: OrderDetailsModel;
}

const closeModal = () => {
    modalsStore.closeModal(modalsTypes.orderDetailsModal);
};

export const OrderDetailsModal = (props: Props) => {
    const [orderDetails, setOrderDetails] = useState<OrderDetailsModel>();

    const onRemoveItem = async (index: number) => {
        await removeOrderItem({
            orderId: props.orderId,
            orderItemId: orderDetails.orderItems[index].id,
        });
    };

    useAsyncEffect(async () => {
        setOrderDetails(await getOrderDetails(props.orderId));
    }, [props.orderId]);

    const onPostponeOrder = (orderId: string) => {
        showConfirmation({
            body: localizer.get("PostponeOrderConfirmation_Body"),
            onConfirmClick: async () => {
                await postponeOrder(orderId);
                closeModal();
            }
        });
    };

    const onCancelOrder = (orderId: string) => {
        showConfirmation({
            body: localizer.get("CancelOrderConfirmation_Body"),
            onConfirmClick: async () => {
                await cancelOrder(orderId);
                closeModal();
            }
        });
    };

    const onApproveOrder = (orderId: string) => {
        showConfirmation({
            body: localizer.get("ApproveOrderConfirmation_Body"),
            onConfirmClick: async () => {
                await approveOrder(orderId);
                closeModal();
            }
        });
    };

    const onCloseOrder = (orderId: string) => {
        showConfirmation({
            body: localizer.get("CloseOrderConfirmation_Body"),
            onConfirmClick: async () => {
                await closeOrder(orderId);
                closeModal();
            }
        });
    };

    return (
        <>
            <ModalHeaderControl toggle={closeModal}>
                <p className="title text-center">
                    <Local id="OrderDetailsModal.Title" />
                </p>
            </ModalHeaderControl>
            <hr className="w-50" />
            <ModalBody className="d-flex flex-column align-items-center px-sm-5 px-3 pt-2">
                <FinalForm
                    onSubmit={() => null}
                    initialValues={{
                        orderDetails: orderDetails,
                    }}
                    mutators={{...arrayMutators}}
                    render={({ values, handleSubmit }: FormRenderProps<OrderDetailsFormData>) => (
                        <Form onSubmit={handleSubmit}>
                            <OrderDetailsCard
                                orderDetails={orderDetails}
                                ind={0} />
                            <OrderDetailsItemsControl orderItems={values.orderDetails?.orderItems}
                                onRemoveItem={onRemoveItem} />
                            <Row className="order-step-card p-3 mb-2">
                                <Col>
                                    <Row className="mb-2">
                                        {orderDetails?.orderStatus === OrderStatus.Pending &&
                                            <Button className="order-action-button button-postpone mr-2"
                                                onClick={() => onPostponeOrder(props.orderId)}>
                                                    {localizer.get("PostponeOrder")}
                                            </Button>
                                        }
                                        {(orderDetails?.orderStatus === OrderStatus.Pending || orderDetails?.orderStatus === OrderStatus.Postponed ||
                                          orderDetails?.orderStatus === OrderStatus.Approved) &&
                                            <Button className="order-action-button button-cancel mr-2"
                                                onClick={() => onCancelOrder(props.orderId)}>
                                                    {localizer.get("CancelOrder")}
                                            </Button>
                                        }
                                        {(orderDetails?.orderStatus === OrderStatus.Pending || orderDetails?.orderStatus === OrderStatus.Postponed) &&
                                            <Button className="order-action-button button-approve mr-2"
                                                onClick={() => onApproveOrder(props.orderId)}>
                                                    {localizer.get("ApproveOrder")}
                                            </Button>
                                        }
                                        {(orderDetails?.orderStatus === OrderStatus.Approved) &&
                                            <Button className="order-action-button button-close mr-2"
                                                onClick={() => onCloseOrder(props.orderId)}>
                                                    {localizer.get("CloseOrder")}
                                            </Button>
                                        }
                                    </Row>
                                </Col>
                            </Row>
                        </Form>)}
                />
            </ModalBody>
        </>
    );
};