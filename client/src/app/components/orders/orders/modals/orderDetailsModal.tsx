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
import { notifications } from "../../../toast/toast";
import { Money } from "../../../common/presentation/money";

interface Props {
    orderId: string;
    fromOrdersHistory?: boolean;
}

interface OrderDetailsFormData {
    orderDetails: OrderDetailsModel;
}

const closeModal = () => {
    modalsStore.closeModal(modalsTypes.orderDetailsModal);
};

const getPromotionInfo = (orderDetails: OrderDetailsModel): JSX.Element => {
    return <Money amount={orderDetails.promoAmount} discountPercent={orderDetails.promoRate * 100} />;
};

export const OrderDetailsModal = (props: Props) => {
    const [orderDetails, setOrderDetails] = useState<OrderDetailsModel>();

    const shouldHideRemoveButton = () => {
        return orderDetails?.orderItems.length === 1 ||
            orderDetails?.orderStatus === OrderStatus.Approved ||
            orderDetails?.orderStatus === OrderStatus.Cancelled ||
            orderDetails?.orderStatus === OrderStatus.Closed;
    };

    const onRemoveItem = async (index: number) => {
        await removeOrderItem({
            orderId: props.orderId,
            orderItemId: orderDetails.orderItems[index].id,
        });

        orderDetails.totalAmount -= orderDetails.orderItems[index].quantity * orderDetails.orderItems[index].product.price;
        orderDetails.orderItems.splice(index, 1);

        setOrderDetails({
            ...orderDetails,
            quantity: orderDetails.quantity - 1,
            orderItems: orderDetails.orderItems,
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
                const result = await approveOrder(orderId);

                if (!result.isSucceeded) {
                    notifications.localizedError(result.errors[0]);
                    return;
                }
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
                        <Form onSubmit={handleSubmit} className="w-100">
                            <OrderDetailsCard
                                orderDetails={orderDetails}
                                ind={0} />
                            {(orderDetails?.promoCode) &&
                                <Row className="in-order-card w-100 ml-2 mb-4">
                                    <Col>
                                        <Row className="promocode-header py-1 pl-3">
                                            <Col className="pl-0">
                                                <span className="ui-section-header"><Local id="AppliedPromocode" /></span>
                                            </Col>
                                        </Row>
                                        <Row className="promocode-body py-1 pl-3">
                                            <Col className="pl-0">
                                                <p>{orderDetails.promoCode} - {getPromotionInfo(orderDetails)}</p>
                                            </Col>
                                        </Row>
                                    </Col>
                                </Row>
                            }
                            {(orderDetails?.customerNotes && !props.fromOrdersHistory) &&
                                <Row className="in-order-card w-100 ml-2 mb-4">
                                    <Col>
                                        <Row className="comment-header py-1 pl-3">
                                            <Col className="pl-0">
                                                <span className="ui-section-header"><Local id="CustomerComment" /></span>
                                            </Col>
                                        </Row>
                                        <Row className="comment-body py-1 pl-3">
                                            <Col className="pl-0">
                                                <p>{orderDetails.customerNotes}</p>
                                            </Col>
                                        </Row>
                                    </Col>
                                </Row>
                            }
                            <OrderDetailsItemsControl orderItems={values.orderDetails?.orderItems}
                                onRemoveItem={onRemoveItem}
                                hideRemoveButton={props.fromOrdersHistory || shouldHideRemoveButton()}
                                closeModal={closeModal}/>
                            {!props.fromOrdersHistory &&
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
                            }
                        </Form>)}
                />
            </ModalBody>
        </>
    );
};