import React, { useState } from "react";
import { ProductListItem } from "../../../dataModels/catalog/productListItem";
import { Local } from "../../localization/local";
import { NoData } from "../../common/presentation/noData";
import { ProductCard } from "../product/productCard";
import useAsyncEffect from "use-async-effect";
import { getPopularProducts } from "../../../api/catalog/catalogApi";
import { sharedHistory } from "../../../infrastructure/sharedHistory";
import { getRoute } from "../../../utils/routeUtils";
import { routeLinks } from "../../layout/routes/routeLinks";
import { onAddToCart, onAddToFavorites, onDeleteProduct } from "../catalogActions";
import { Col, Row } from "reactstrap";

export const PopularProductsSection = () => {
    const [popularProductsList, setPopularProductsList] = useState<ProductListItem[]>();

    useAsyncEffect(async () => {
        setPopularProductsList(await getPopularProducts());
    }, []);

    return (
        <div>
            <Row className="mb-3">
                <Col>
                    <h1 className="ui-page-header pt-2"><Local id="PopularProducts" /></h1>
                </Col>
            </Row>
            <Row className="p-3">
                {popularProductsList?.length > 0
                    ? popularProductsList.map((product, ind) =>
                        <ProductCard
                            className="col-2"
                            key={ind}
                            product={product}
                            onAddToFavorites={async () => await onAddToFavorites(product.productId)}
                            onAddToCart={async () => await onAddToCart(product.productId)}
                            onEditProduct={() => sharedHistory.push(getRoute(routeLinks.catalog.editProduct, product.productId))}
                            onDeleteProduct={async () => await onDeleteProduct(product.productId)} />)
                    : <NoData />}
            </Row>
        </div>
    );
};