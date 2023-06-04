import React, { useState} from "react";
import { Row, Col, Card, CardBody, Button } from "reactstrap";
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
import { downloadFile, getFileAnonymousDownloadLink } from "../../../api/file/filesApi";
import { Constants } from "../../constants";
import { isEmpty } from "lodash";
import { NoData } from "../../common/presentation/noData";
import { isConsumer } from "../../../infrastructure/services/auth/authService";
import { sharedHistory } from "../../../infrastructure/sharedHistory";
import { getRoute } from "../../../utils/routeUtils";
import { routeLinks } from "../../layout/routes/routeLinks";
import { CommentsSection } from "../comments/commentsSection";

const sliderSettings = {
    dots: true,
    dotsClass: "slick-dots",
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1,
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

    const onDownloadFile = async (fileId: string) => {
        if (!isConsumer()) {
            sharedHistory.push(getRoute(routeLinks.account.login));
        }

        await downloadFile(fileId);
    };

    return (
        <div className="product-details">
            <Row className="mb-3">
                <Col>
                    <h1 className="ui-page-header pt-2">{productDetails?.name}</h1>
                </Col>
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
                    <Card className="product-card mb-2  mx-auto">
                        <CardBody className="d-flex flex-column">
                        <div className="card-text">
                            <ProductRatingSection
                                productRating={productDetails?.rating} />
                            {productDetails?.characteristics?.map((characteristic, ind) =>
                                <CardCharacteristic key={ind} characteristic={characteristic}/>)}
                                <p className="mt-2">{productDetails?.shortDescription}</p>
                            </div>
                            <Row className="mt-auto align-items-center">
                                <Col className="col-7 align-self-center">
                                    <CardPrice
                                        value={productDetails?.price}
                                        currency={CurrencyType.BYN}
                                        showColouredBox />
                                </Col>
                                <Col className="col-5 align-self-center">
                                    <CardActionsSection
                                        onAddToCart={() => onAddToCart(productId)}
                                        onAddToFavorites={() => onAddToFavorites(productId)}/>
                                </Col>
                            </Row>
                        </CardBody>
                    </Card>
                </Col>
            </Row>
            <Row className="order-step-card p-3 mb-2">
                <Col>
                    <Row className="mb-2">
                        <h1 className="ui-section-header pt-2"><Local id="Description"/></h1>
                    </Row>
                    <Row className="section-description">
                        <p>{productDetails?.description}</p>
                    </Row>
                </Col>
            </Row>
            <Row className="order-step-card p-3 mb-2">
                <Col>
                    <Row className="mb-2">
                        <h1 className="ui-section-header pt-2"><Local id="Equipment"/></h1>
                    </Row>
                    <Row className="section-description">
                        <Col>
                            {!isEmpty(productDetails?.equipment)
                                ? productDetails?.equipment?.split(";").map((elem, index) => (
                                    <Row className="" key={index}><span>- {elem}</span></Row>))
                                : <NoData localizationKey="NoEquipment" />
                            }
                        </Col>
                    </Row>
                </Col>
            </Row>
            {getCharacteristicsSection()}
            <Row className="order-step-card p-3 mb-2">
                <Col>
                    <Row className="mb-2">
                        <h1 className="ui-section-header pt-2"><Local id="Instructions"/></h1>
                    </Row>
                    <Row>
                        {!isEmpty(productDetails?.productFiles)
                            ? productDetails?.productFiles.map((productFile, index) => (
                                <Button key={index} onClick={() => onDownloadFile(productFile.id)} className="btn btn-link">
                                    <span>{index}. {productFile.name}</span>
                                </Button>))
                            : <NoData localizationKey="NoInstructions"/>
                        }
                    </Row>
                </Col>
            </Row>
            <Row className="order-step-card p-3 mb-2">
                <Col>
                    <Row className="mb-2">
                        <h1 className="ui-section-header pt-2"><Local id="Comments"/></h1>
                    </Row>
                    <CommentsSection productId={productId} />
                </Col>
            </Row>
        </div>
    );
};