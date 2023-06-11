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
import { isEmpty } from "lodash";
import { PresentLabel } from "../../common/presentation/presentLabel";

interface Props {
    className?: string;
    key?: number;
    product: ProductListItem;
    onAddToFavorites: () => void;
    onAddToCart: () => void;
    onDeleteProduct: () => void;
    onEditProduct: () => void;
    isInCart?: boolean;
}

export const ProductCard = (props: Props) => {
    return (
        <Card className="product-card mr-3 mb-2">
            <ProductImage
                className="card-img-top card-product-image"
                productId={props.product.productId}
                productImageId={!isEmpty(props.product.imageFilesIds) && props.product.imageFilesIds[0]} />
            <CardBody className="d-flex flex-column">
                <h5 className="card-title mb-1"><Link className="text-secondary" to={getLinkToProductDetails(props.product.productId)}>{props.product.name}</Link></h5>
                <div className="card-text">
                    <ProductRatingSection
                        productRating={props.product.productRating} />
                    {props.product.characteristicsModels?.map((characteristic, ind) =>
                        <CardCharacteristic key={ind} characteristic={characteristic}/>)}
                    {props.product.specialNote &&
                        <p><b>{props.product.specialNote}</b></p>
                    }
                    <p className="section-description mt-2">{props.product.shortDescription}</p>
                </div>
                {props.product.quantity === 0
                    ? <PresentLabel icon="highlight_off" labelKey="NotInPresence" className="text-danger" />
                    : <PresentLabel icon="check_circle_outline" labelKey="HaveInPresence" className="text-success" />}
                <Row className="mt-auto align-items-center">
                    <Col className="col-7 align-self-center px-0">
                        <CardPrice
                            value={props.product.price}
                            currency={CurrencyType.BYN}
                            showColouredBox />
                    </Col>
                    <Col className="col-5 align-self-center">
                        <CardActionsSection
                            onAddToCart={props.onAddToCart}
                            onAddToFavorites={props.onAddToFavorites}
                            onDeleteProduct={props.onDeleteProduct}
                            onEditProduct={props.onEditProduct} />
                    </Col>
                </Row>
            </CardBody>
        </Card>
    );
};