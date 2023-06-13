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
import { PromotionModel } from "../../../dataModels/promotions/promotionModel";
import { getPromotion } from "../../../api/promotions/promotionsApi";
import { AppIcon } from "../../common/controls/appIcon";

export interface OrderFormData {
    cart: Cart;
    customerInfo: OrderCustomerInfo;
    deliveryType?: DeliveryType;
    paymentMethod?: PaymentMethod;
    customerNotes?: string;
    promoCode?: string;
}

const courierDeliveryAmount = 20;

const calculateTotalAmount = (cart: Cart, deliveryType: DeliveryType, promotion: PromotionModel): number => {
    if (!cart || !cart.cartItems) {
        return 0;
    }

    let result = 0;

    for (let i = 0; i < cart.cartItems.length; i++) {
        result += cart.cartItems[i].product.price * cart.cartItems[i].quantity;
    }

    if (promotion) {
        result *= (1 - promotion.promoRate);
    }

    if (deliveryType === DeliveryType.Courier) {
        result += courierDeliveryAmount;
    }

    return result;
};

const calculateShortTotalAmount = (cart: Cart): number => {
    if (!cart || !cart.cartItems) {
        return 0;
    }

    let result = 0;

    for (let i = 0; i < cart.cartItems.length; i++) {
        result += cart.cartItems[i].product.price * cart.cartItems[i].quantity;
    }

    return result;
};

const calculatePromotionDiscountAmount = (cart: Cart, promotion: PromotionModel): number => {
    if (!cart || !cart.cartItems) {
        return 0;
    }

    let result = 0;

    for (let i = 0; i < cart.cartItems.length; i++) {
        result += cart.cartItems[i].product.price * cart.cartItems[i].quantity;
    }

    if (promotion) {
        result *= promotion.promoRate;
    }

    return result;
};

export const MakeOrderPage = () => {
    const [customerInfo, setCustomerInfo] = useState<OrderCustomerInfo>(null);
    const [cart, setCart] = useState<Cart>();
    const [promotion, setPromotion] = useState<PromotionModel>();
    const [promotionNotFound, setPromotionNotFound] = useState<boolean>(null);

    useAsyncEffect(async () => {
        setCart(await getCart());
        setCustomerInfo(await getOrderCustomerInfo());
    }, []);

    const onSubmit = async (formData: OrderFormData) => {
        if (formData.cart.cartItems.length < 1) {
            notifications.localizedError("NeedToAddProductToOrder");
            return;
        }

        if (promotionNotFound !== false && promotionNotFound !== null) {
            notifications.localizedError("EnterValidPromotionMessage");
            return;
        }

        const result = await makeOrder({
            ...formData,
            cartItemsIds: formData.cart.cartItems.map(ci => ci.id),
            totalAmount: calculateTotalAmount(formData.cart, formData.deliveryType, promotion),
            customerInfo: formData.customerInfo,
            promoCode: formData.promoCode,
            promoRate: promotion?.promoRate,
            promoAmount: calculatePromotionDiscountAmount(formData.cart, promotion),
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

        cart.cartItems.splice(index, 1);

        setCart({
            ...cart,
            cartItems: cart.cartItems,
        });
    };

    const onApplyPromocode = async (promocode: string) => {
        const result = await getPromotion(promocode);

        if (!result.isSucceeded) {
            notifications.localizedWarning("PromotionNotFound");
            setPromotionNotFound(true);

            return;
        }

        setPromotion(result.data);
        setPromotionNotFound(false);
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
                                    <MakeOrderCard
                                        quantity={values.cart?.quantity ?? 0}
                                        shortTotalAmount={calculateShortTotalAmount(values.cart)}
                                        totalAmount={calculateTotalAmount(values.cart, values.deliveryType, promotion)}
                                        isValid={valid}
                                        promoDiscountAmount={promotionNotFound === false && calculatePromotionDiscountAmount(values.cart, promotion)}
                                        promoDiscountPercent={promotion?.promoRate}
                                        deliveryAmount={values.deliveryType === DeliveryType.Courier && courierDeliveryAmount} />
                                </Col>
                            </Row>
                            <Row className="promocode-card p-3">
                                <Col className="pl-0">
                                    <Row className="mb-2">
                                        <Col>
                                            <h1 className="ui-section-header pt-2"><Local id="Promocode" /></h1>
                                        </Col>
                                    </Row>
                                    <TextFormControl
                                        name={"promoCode"}
                                        placeholder="Введите промокод"
                                        onChange={() => setPromotionNotFound(null)} />
                                    <Button onClick={() => onApplyPromocode(values.promoCode)}>
                                        <Local id="ApplyPromocode" />
                                    </Button>
                                    {promotionNotFound === true &&
                                        <div className="mt-3 d-flex align-items-center">
                                            <AppIcon className="promotion-fail-message" icon="block" />
                                            <span className="ml-1 promotion-fail-message">Введён некорректный промокод</span>
                                        </div>
                                    }
                                    {promotionNotFound === false &&
                                        <div className="mt-3 d-flex align-items-center">
                                            <AppIcon className="text-success" icon="check_circle" />
                                            <span className="ml-1 text-success">Промокод успешно применён!</span>
                                        </div>
                                    }
                                </Col>
                            </Row>
                        </Row>
                    </Col>
                </Row>
            </Form>)}
        />
    );
};