import React, { useState } from "react";
import { Button, Col, Form, Row } from "reactstrap";
import { Form as FinalForm, FormRenderProps } from "react-final-form";
import useAsyncEffect from "use-async-effect";
import { changeProductCount, getCart, removeProductFromCart } from "../../api/cart/cartApi";
import { Cart } from "../../dataModels/cart/cart";
import { Local } from "../localization/local";
import { CurrencyType } from "../../dataModels/enums/currencyType";
import { CardPrice } from "../common/presentation/cardPrice";
import arrayMutators from "final-form-arrays";
import { CartItemsControl } from "./cartItemsControl";

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

export const CartPage = () => {
    const [cart, setCart] = useState<Cart>();

    useAsyncEffect(async () => {
        setCart(await getCart());
    }, []);



    const onSubmit = () => {

    };

    const onClearCart = () => {

    };

    const onRemoveItem = async (index: number) => {
        await removeProductFromCart(cart.cartItems[index].product.productId);
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
                <Row className="justify-content-end">
                    <Col>
                        <Button type="submit" >
                            <Local id="MakeOrder" />
                        </Button>
                    </Col>
                </Row>
                <Row className="cart-items-header mt-3 py-3 w-100">
                    <Col className="col-2 my-auto font-weight-bold">
                        <Local id="Product" />
                    </Col>
                    <Col className="col-4 my-auto">

                    </Col>
                    <Col className="col-2 m-auto font-weight-bold">
                        <Local id="PricePerUnit" />
                    </Col>
                    <Col className="col-2 m-auto font-weight-bold">
                        <Local id="Quantity" />
                    </Col>
                    <Col className="col-2 m-auto font-weight-bold">
                        <Local id="Total" />
                    </Col>
                </Row>
                <CartItemsControl cartItems={values.cart?.cartItems}
                    onRemoveItem={onRemoveItem}
                    onChangeItemQuantity={onChangeItemQuantity} />
                <Row className="cart-items-footer py-3 pr-3 w-100 my-3">
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
                <Row className="justify-content-between">
                    <Col className="p-0">
                        <Button type="submit">
                            <Local id="ClearCart" />
                        </Button>
                    </Col>
                    <Col className="text-right p-0">
                        <Button onClick={onClearCart} >
                            <Local id="MakeOrder" />
                        </Button>
                    </Col>
                </Row>
            </Form>)}
        />
    );
};