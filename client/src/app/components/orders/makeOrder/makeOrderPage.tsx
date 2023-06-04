import React, { useState } from "react";
import { Form as FinalForm, FormRenderProps } from "react-final-form";
import { Row, Col, Button, Form } from "reactstrap";
import useAsyncEffect from "use-async-effect";
import { getCart, removeProductFromCart } from "../../../api/cart/cartApi";
import { Cart } from "../../../dataModels/cart/cart";
import { Local } from "../../localization/local";
import arrayMutators from "final-form-arrays";
import { OrderCustomerInfo } from "../../../dataModels/orders/makeOrder/orderCustomerInfo";
import { getOrderCustomerInfo, makeOrder } from "../../../api/orders/ordersApi";
import { TextFormControl } from "../../common/controls/formControls/textFormControl";
import { PhoneFormControl } from "../../common/controls/formControls/maskedFormControls/phoneFormControl";
import { EmailFormControl } from "../../common/controls/formControls/emailFormControl";
import { OrderItemsControl } from "./orderItemsControl";
import { DeliveryType } from "../../../dataModels/enums/deliveryType";
import { PaymentMethod } from "../../../dataModels/enums/paymentMethod";
import { OrderDeliverySection } from "./orderDeliverySection";
import { localizer } from "../../localization/localizer";
import { sharedHistory } from "../../../infrastructure/sharedHistory";
import { getRoute } from "../../../utils/routeUtils";
import { routeLinks } from "../../layout/routes/routeLinks";
import { OrderPaymentSection } from "./orderPaymentSection";
import { MakeOrderCard } from "./makeOrderCard";
import { TextAreaFormControl } from "../../common/controls/formControls/textareaFormControl";
import { required, requiredNonWhitespace } from "../../common/controls/validation/validators";
import { notifications } from "../../toast/toast";
import { modalsStore } from "../../../infrastructure/stores/modalsStore";
import { modalsTypes } from "../../layout/modals/modalsTypes";

export interface OrderFormData {
    cart: Cart;
    customerInfo: OrderCustomerInfo;
    deliveryType?: DeliveryType;
    paymentMethod?: PaymentMethod;
    customerNotes?: string;
    promocode?: string;
}

const calculateTotalAmount = (cart: Cart): number => {
    if (!cart || !cart.cartItems) {
        return 0;
    }

    let result = 0;

    for (let i = 0; i < cart.cartItems.length; i++) {
        result += cart.cartItems[i].product.price * cart.cartItems[i].quantity;
    }

    return result;
};

export const MakeOrderPage = () => {
    const [customerInfo, setCustomerInfo] = useState<OrderCustomerInfo>(null);
    const [cart, setCart] = useState<Cart>();

    useAsyncEffect(async () => {
        setCart(await getCart());
        setCustomerInfo(await getOrderCustomerInfo());
    }, []);

    const onSubmit = async (formData: OrderFormData) => {
        if (formData.cart.cartItems.length < 1) {
            notifications.localizedError("NeedToAddProductToOrder");
            return;
        }

        const result = await makeOrder({
            ...formData,
            cartItemsIds: formData.cart.cartItems.map(ci => ci.id),
            totalAmount: calculateTotalAmount(formData.cart),
            customerInfo: formData.customerInfo,
        });

        if (!result.isSucceeded) {
            notifications.localizedError("MakeOrderError");
        } else {
            modalsStore.openModal({
                modalType: modalsTypes.makeOrderConfirmationModal,
                modalProps: {
                    orderNumber: result.data,
                },
            });
        }
    };

    const onRemoveItem = async (index: number) => {
        setCart({
            ...cart,
            quantity: cart.quantity - cart.cartItems[index].quantity,
        });

        await removeProductFromCart(cart.cartItems[index].product.productId);
    };

    return (
        <FinalForm
        onSubmit={onSubmit}
        initialValues={{
            cart: cart,
            customerInfo: customerInfo,
        }}
        mutators={{...arrayMutators}}
        render={({ values, handleSubmit, valid }: FormRenderProps<OrderFormData>) => (
            <Form onSubmit={handleSubmit}>
                <Row className="mb-3">
                    <Col>
                        <h1 className="ui-page-header pt-2"><Local id="MakeOrder_Title" /></h1>
                    </Col>
                </Row>
                <Row>
                    <Col className="col-9">
                        <Row className="order-step-card p-3 mb-2">
                            <Col>
                                <Row className="mb-2">
                                    <h1 className="ui-section-header pt-2"><Local id="CustomerInfo" /></h1>
                                </Row>
                                <Row>
                                    <Col className="pl-0">
                                        <TextFormControl label={"FirstName"} name={"customerInfo.firstName"} placeholder="Имя"
                                            validator={requiredNonWhitespace} markRequired />
                                    </Col>
                                    <Col className="pl-0">
                                        <TextFormControl label={"LastName"} name={"customerInfo.lastName"} placeholder="Фамилия"
                                            validator={requiredNonWhitespace} markRequired />
                                    </Col>
                                </Row>
                                <Row>
                                    <Col className="pl-0">
                                        <PhoneFormControl label={"Phone"} name={"customerInfo.phone"}
                                            validator={required} markRequired />
                                    </Col>
                                    <Col className="pl-0">
                                        <EmailFormControl label={"Email"} name={"customerInfo.email"} placeholder="Адрес электронной почты"
                                            validator={required} markRequired />
                                    </Col>
                                </Row>
                            </Col>
                        </Row>
                        <Row className="order-step-card p-3 mb-2">
                            <Col>
                                <Row className="mb-2">
                                    <Col className="pl-0">
                                        <h1 className="ui-section-header pt-2"><Local id="OrderComposition" /></h1>
                                    </Col>
                                    <Col className="justify-content-between">
                                        <Button className="float-right"
                                            onClick={() => sharedHistory.push(getRoute(routeLinks.cart.root))}>
                                                {localizer.get("BackToCart")}
                                        </Button>
                                    </Col>
                                </Row>
                                <OrderItemsControl cartItems={values.cart?.cartItems}
                                    onRemoveItem={onRemoveItem} />
                            </Col>
                        </Row>
                        <Row className="order-step-card p-3 mb-2">
                            <Col>
                                <Row className="mb-2">
                                    <h1 className="ui-section-header pt-2"><Local id="OrderDelivery" /></h1>
                                </Row>
                                <OrderDeliverySection />
                                {values.deliveryType === DeliveryType.Courier &&
                                    <Row>
                                        <Col>
                                            <TextFormControl label={"City"} name={"customerInfo.address.city"} placeholder="Город"
                                                validator={requiredNonWhitespace} markRequired />
                                            <TextFormControl label={"PostalCode"} name={"customerInfo.address.postalCode"} placeholder="Почтовый индекс"
                                                validator={requiredNonWhitespace} markRequired />
                                        </Col>
                                        <Col>
                                            <TextFormControl label={"FullAddressLine"} name={"customerInfo.address.fullAddress"} placeholder="Улица, дом, квартира"
                                                validator={requiredNonWhitespace} markRequired />
                                        </Col>
                                    </Row>
                                }
                            </Col>
                        </Row>
                        <Row className="order-step-card p-3 mb-2">
                            <Col>
                                <Row className="mb-2">
                                    <h1 className="ui-section-header pt-2"><Local id="OrderPayment" /></h1>
                                </Row>
                                <OrderPaymentSection />
                            </Col>
                        </Row>
                        <Row className="order-step-card p-3 mb-2">
                            <Col>
                                <Row className="mb-2">
                                    <h1 className="ui-section-header pt-2"><Local id="CustomerNotes" /></h1>
                                </Row>
                                <Row>
                                    <TextAreaFormControl className="w-100" name={"customerNotes"} placeholder="Вы можете оставить свой комментарий к заказу здесь" />
                                </Row>
                            </Col>
                        </Row>
                    </Col>
                    <Col className="col-3 make-order-card-wrapper">
                        <Row className="sticky-card">
                            <Row className="make-order-card p-3 mb-2">
                                <Col>
                                    <Row className="mb-2">
                                        <h1 className="ui-section-header pt-2"><Local id="TotalsInOrder" /></h1>
                                    </Row>
                                    <MakeOrderCard quantity={values.cart?.quantity ?? 0} totalAmount={calculateTotalAmount(values.cart)} isValid={valid} />
                                </Col>
                            </Row>
                            <Row className="promocode-card p-3">
                                <Col>
                                    <Row className="mb-2">
                                        <h1 className="ui-section-header pt-2"><Local id="Promocode" /></h1>
                                    </Row>
                                    <TextFormControl name={"promocode"} placeholder="Введите промокод" />
                                </Col>
                            </Row>
                        </Row>
                    </Col>
                </Row>
            </Form>)}
        />
    );
};