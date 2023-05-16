import React, { useState } from "react";
import { ProductListItem } from "../../../dataModels/catalog/productListItem";
import { Local } from "../../localization/local";
import { NoData } from "../../common/presentation/noData";
import { ProductCard } from "../product/productCard";
import useAsyncEffect from "use-async-effect";
import { getPopularProducts } from "../../../api/catalog/catalogApi";

interface Props {
    onAddToFavorites: (productId: string) => void;
    onAddToCart: (productId: string) => void;
}

export const PopularProductsSection = (props: Props) => {
    const [popularProductsList, setPopularProductsList] = useState<ProductListItem[]>();

    useAsyncEffect(async () => {
        setPopularProductsList(await getPopularProducts());
    }, []);

    return (
        <div>
            <h1 className="page-header"><Local id="PopularProducts" /></h1>
            {popularProductsList?.length > 0
                ? popularProductsList?.map((product, ind) =>
                    <ProductCard key={ind}
                        product={product}
                        onAddToCart={() => props.onAddToCart(product.id)}
                        onAddToFavorites={() => props.onAddToFavorites(product.id)} />)
                : <NoData />
            }
        </div>
    );
};