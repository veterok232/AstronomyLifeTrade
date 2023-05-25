import React, { useState } from "react";
import { ProductListItem } from "../../../dataModels/catalog/productListItem";
import { Local } from "../../localization/local";
import { NoData } from "../../common/presentation/noData";
import { ProductCard } from "../product/productCard";
import useAsyncEffect from "use-async-effect";
import { getPopularProducts } from "../../../api/catalog/catalogApi";
import { getProductsInCart } from "../../../api/cart/cartApi";
import { isConsumer } from "../../../infrastructure/services/auth/authService";

interface Props {
    onAddToFavorites: (productId: string) => void;
    onAddToCart: (productId: string) => void;
}

export const PopularProductsSection = (props: Props) => {
    const [popularProductsList, setPopularProductsList] = useState<ProductListItem[]>();
    const [productsInCart, setProductsInCart] = useState<string[]>();

    useAsyncEffect(async () => {
        setPopularProductsList(await getPopularProducts());

        if (isConsumer()) {
            setProductsInCart(await getProductsInCart());
        }
    }, []);

    const isProductInCart = (productId: string): boolean => {
        return isConsumer()
            ? productsInCart.includes(productId)
            : false;
    };

    return (
        <div>
            <h1 className="page-header"><Local id="PopularProducts" /></h1>
            {popularProductsList?.length > 0
                ? popularProductsList?.map((product, ind) =>
                    <ProductCard key={ind}
                        product={product}
                        onAddToCart={() => props.onAddToCart(product.productId)}
                        onAddToFavorites={() => props.onAddToFavorites(product.productId)}
                        isInCart={isProductInCart(product.productId)} />)
                : <NoData />
            }
        </div>
    );
};