import React from "react";
import { ProductListItem } from "../../../dataModels/catalog/productListItem";
import { Card, CardBody, Col, Row } from "reactstrap";
import { ProductImage } from "../../layout/catalog/productImage";
import { Link } from "react-router-dom";
import { getLinkToProductDetails } from "../../../api/catalog/catalogApi";
import { ProductRatingSection } from "./productRatingSection";
import { CardCharacteristic } from "./cardCharacteristic";
import { CardPrice } from "../../common/presentation/cardPrice";
import { CurrencyType } from "../../../dataModels/enums/currencyType";
import { CardActionsSection } from "./cardActionsSection";

interface Props {
    className?: string;
    key?: number;
    product: ProductListItem;
    onAddToFavorites: () => void;
    onAddToCart: () => void;
    isInCart?: boolean;
}

export const ProductCard = (props: Props) => {
    return (
        <Card className="product-card mr-3 mb-2">
            <ProductImage className="card-img-top" productId={props.product.productId} />
            <CardBody className="d-flex flex-column">
                <h5 className="card-title mb-1"><Link className="text-secondary" to={getLinkToProductDetails(props.product.productId)}>{props.product.name}</Link></h5>
                <div className="card-text">
                    <ProductRatingSection
                        productRating={props.product.rating} />
                    {props.product.characteristics?.map((characteristic, ind) =>
                        <CardCharacteristic key={ind} characteristic={characteristic}/>)}
                    {props.product.specialNote &&
                        <p><b>{props.product.specialNote}</b></p>
                    }
                    <p className="mt-2">{props.product.shortDescription}</p>
                </div>
                <Row className="mt-auto align-items-center">
                    <Col className="align-self-center">
                        <CardPrice
                            value={props.product.price}
                            currency={CurrencyType.BYN}
                            showColouredBox />
                    </Col>
                    <Col className="align-self-center">
                        <CardActionsSection
                            onAddToCart={props.onAddToCart}
                            onAddToFavorites={props.onAddToFavorites}/>
                    </Col>
                </Row>
            </CardBody>
        </Card>
    );
};