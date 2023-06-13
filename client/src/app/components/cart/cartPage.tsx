import React, { useState } from "react";
import { Button, Col, Form, Row } from "reactstrap";
import { Form as FinalForm, FormRenderProps } from "react-final-form";
import useAsyncEffect from "use-async-effect";
import { changeProductCount, clearCart, getCart, removeProductFromCart } from "../../api/cart/cartApi";
import { Cart } from "../../dataModels/cart/cart";
import { Local } from "../localization/local";
import { CurrencyType } from "../../dataModels/enums/currencyType";
import { CardPrice } from "../common/presentation/cardPrice";
import arrayMutators from "final-form-arrays";
import { CartItemsControl } from "./cartItemsControl";
import { sharedHistory } from "../../infrastructure/sharedHistory";
import { getRoute } from "../../utils/routeUtils";
import { routeLinks } from "../layout/routes/routeLinks";
import { NoData } from "../common/presentation/noData";

interface CartFormData {
    cart: Cart;
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

const isMakeOrderButtonActive = (cart: Cart) => {
    return cart && cart.quantity > 0;
};

export const CartPage = () => {
    const [cart, setCart] = useState<Cart>();

    useAsyncEffect(async () => {
        setCart(await getCart());
    }, []);



    const onSubmit = () => {
        sharedHistory.push(getRoute(routeLinks.orders.makeOrder));
    };

    const onClearCart = async (values: CartFormData) => {
        await clearCart();
        values.cart.cartItems = [];
        values.cart.quantity = 0;
        values.cart.totalAmount = 0;
        window.location.reload();
    };

    const onRemoveItem = async (index: number) => {
        await removeProductFromCart(cart.cartItems[index].product.productId);

        cart.cartItems.splice(index, 1);

        setCart({
            ...cart,
            cartItems: cart.cartItems,
        });
    };

    const onChangeItemQuantity = async (index: number, quantity: number) => {
        await changeProductCount({
            productId: cart.cartItems[index].product.productId,
            count: quantity
        });
    };

    return (
        <FinalForm
        onSubmit={onSubmit}
        initialValues={{cart: cart}}
        mutators={{...arrayMutators}}
        render={({ values, handleSubmit }: FormRenderProps<CartFormData>) => (
            <Form onSubmit={handleSubmit}>
                <Row className="mb-3">
                    <Col xs={4} >
                        <h1 className="ui-page-header pt-2"><Local id="Cart_Title" /></h1>
                    </Col>
                </Row>
                <Row className="order-step-card p-3 mb-2">
                    <Col>
                        <Row className="justify-content-end mb-3">
                            <Col>
                                <Button className="cart-button" type="submit" disabled={!isMakeOrderButtonActive(values.cart)}>
                                    <Local id="MakeOrder" />
                                </Button>
                            </Col>
                        </Row>
                        <hr />
                        {values.cart?.cartItems?.length > 0
                            ? <>
                                <Row className="cart-item w-100 mx-1 pl-1 pb-0">
                                    <Col className="col-2 my-auto p-3 font-weight-bold">
                                        <Local id="Product" />
                                    </Col>
                                    <Col className="col-4 my-auto font-weight-bold">

                                    </Col>
                                    <Col className="col-2 m-auto font-weight-bold">
                                        <Local id="PricePerUnit" />
                                    </Col>
                                    <Col className="col-2 m-auto font-weight-bold">
                                        <Local id="Quantity" />
                                    </Col>
                                    <Col className="col-2 m-auto font-weight-bold pb-0">
                                        <Local id="Total" />
                                    </Col>
                                </Row>
                                <CartItemsControl cartItems={values.cart?.cartItems}
                                    onRemoveItem={onRemoveItem}
                                    onChangeItemQuantity={onChangeItemQuantity} />
                                <Row className="mt-3 mb-3">
                                    <Col className="p-0 text-right">
                                        <span>
                                            <span className="total-cart-price-label"><Local id="TotalAmount" /></span>
                                            :
                                            <CardPrice
                                                className="total-cart-price"
                                                value={calculateTotalAmount(values.cart)}
                                                currency={CurrencyType.BYN} />
                                        </span>
                                    </Col>
                                </Row>
                            </>
                            : <NoData localizationKey="CartIsEmpty"/>
                        }
                        <hr />
                        <Row className="justify-content-between">
                            <Col className="p-0">
                                <Button className="cart-button ml-3" onClick={() => onClearCart(values)}>
                                    <Local id="ClearCart" />
                                </Button>
                            </Col>
                            <Col className="p-0 text-right">
                                <Button className="cart-button mr-2" type="submit" disabled={!isMakeOrderButtonActive(values.cart)}>
                                    <Local id="MakeOrder" />
                                </Button>
                            </Col>
                        </Row>
                    </Col>
                </Row>
            </Form>)}
        />
    );
};