import React from "react";
import { ProductListItem } from "../../../dataModels/catalog/productListItem";
import { Col, Row } from "reactstrap";
import { ProductImage } from "../../layout/catalog/productImage";
import { Link } from "react-router-dom";
import { getLinkToProductDetails } from "../../../api/catalog/catalogApi";
import { ProductRatingSection } from "./productRatingSection";
import { CardCharacteristic } from "./cardCharacteristic";
import { CardPrice } from "../../common/presentation/cardPrice";
import { CurrencyType } from "../../../dataModels/enums/currencyType";
import { CardActionsSection } from "./cardActionsSection";

interface Props {
    key: number;
    product: ProductListItem;
    onAddToFavorites: () => void;
    onAddToCart: () => void;
}

export const ProductCard = (props: Props) => {
    return (
        <div className="product-card">
            <Row>
                <ProductImage productId={props.product.id} />
                <Link to={getLinkToProductDetails(props.product.id)} />
                <ProductRatingSection productId={props.product.id} />
                {props.product.characteristics.map((characteristic, ind) =>
                    <CardCharacteristic key={ind} characteristic={characteristic}/>)}
                {props.product.specialNote &&
                    <p><b>{props.product.specialNote}</b></p>
                }
                <p>{props.product.shortDescription}</p>
            </Row>
            <Row>
                <Col>
                    <CardPrice value={props.product.price} currency={CurrencyType.BYN} />
                </Col>
                <Col>
                    <CardActionsSection
                        onAddToCart={props.onAddToCart}
                        onAddToFavorites={props.onAddToFavorites}/>
                </Col>
            </Row>
        </div>
    );
};