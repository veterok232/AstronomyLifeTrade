import React, { useState} from "react";
import { Row, Col, Card, CardBody } from "reactstrap";
import useAsyncEffect from "use-async-effect";
import { ProductDetails } from "../../../dataModels/catalog/productDetails/productDetails";
import { Local } from "../../localization/local";
import { onAddToFavorites, onAddToCart } from "../catalogActions";
import { useParams } from "react-router-dom";
import { CurrencyType } from "../../../dataModels/enums/currencyType";
import { CardPrice } from "../../common/presentation/cardPrice";
import { CardActionsSection } from "../product/cardActionsSection";
import { ProductRatingSection } from "../product/productRatingSection";
import Slider from "react-slick";
import { TelescopeDetails } from "../../../dataModels/catalog/productDetails/telescopeDetails";
import { TelescopeCharacteristicsSection } from "./telescopeCharacteristicsSection";
import { categoryCodes } from "../../../dataModels/enums/categoryCodes";
import { getProductDetails } from "../../../api/catalog/catalogApi";
import { CardCharacteristic } from "../product/cardCharacteristic";
import { getFileAnonymousDownloadLink } from "../../../api/file/filesApi";
import { Constants } from "../../constants";
import { isEmpty } from "lodash";

const sliderSettings = {
    dots: true,
    dotsClass: "slick-dots",
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1,
    arrows: true,
  };

const getProductImageUrl = (imageFileId?: string) => {
if (!imageFileId) {
    return Constants.defaultProductImagePath;
}

return getFileAnonymousDownloadLink(imageFileId);
};

export const ProductDetailsPage = () => {
    const { productId } = useParams<{ productId: string }>();
    const [productDetails, setProductDetails] = useState<ProductDetails>();

    const getCharacteristicsSection = () => {
        if (productDetails?.category?.code === categoryCodes.telescopes) {
            return <TelescopeCharacteristicsSection details={productDetails as TelescopeDetails} />;
        }
    };

    useAsyncEffect(async () => {
        setProductDetails(await getProductDetails(productId));
    }, []);

    return (
        <div className="product-details">
            <Row>
                <h1 className="ui-page-header pt-2">{productDetails?.name}</h1>
            </Row>
            <Row className="product-details top-box row-cols-2 p-4 mb-3">
                <Col className="col-9">
                    <Slider {...sliderSettings}>
                        {!isEmpty(productDetails?.productImagesIds)
                            ? productDetails.productImagesIds?.map((imageId, i) => (
                                <div key={i}>
                                    <img className="product-details product-image m-auto" src={getProductImageUrl(imageId)} />
                                </div>))
                            :
                                <div>
                                    <img className="product-details product-image m-auto" src={getProductImageUrl(null)} />
                                </div>
                        }
                    </Slider>
                </Col>
                <Col className="col-3">
                    <Card className="product-card mr-3 mb-2">
                        <CardBody className="d-flex flex-column">
                        <div className="card-text">
                            <ProductRatingSection
                                productRating={productDetails?.rating} />
                            {productDetails?.characteristics?.map((characteristic, ind) =>
                                <CardCharacteristic key={ind} characteristic={characteristic}/>)}
                                <p className="mt-2">{productDetails?.shortDescription}</p>
                            </div>
                            <Row className="mt-auto align-items-center">
                                <Col className="align-self-center">
                                    <CardPrice
                                        value={productDetails?.price}
                                        currency={CurrencyType.BYN}
                                        showColouredBox />
                                </Col>
                                <Col className="align-self-center">
                                    <CardActionsSection
                                        onAddToCart={() => onAddToCart(productId)}
                                        onAddToFavorites={() => onAddToFavorites(productId)}/>
                                </Col>
                            </Row>
                        </CardBody>
                    </Card>
                </Col>
            </Row>
            <Row className="mb-3">
                <h4><Local id="Description"/></h4>
                <p>{productDetails?.description}</p>
            </Row>
            <Row className="mb-3">
                <h4><Local id="Equipment"/></h4>
                <p>{productDetails?.equipment}</p>
            </Row>
            {getCharacteristicsSection()}
        </div>
    );
};