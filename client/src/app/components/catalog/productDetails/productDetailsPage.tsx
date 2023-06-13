import React, { useState} from "react";
import { Row, Col, Card, CardBody } from "reactstrap";
import useAsyncEffect from "use-async-effect";
import { ProductDetails } from "../../../dataModels/catalog/productDetails/productDetails";
import { Local } from "../../localization/local";
import { onAddToFavorites, onAddToCart, onDeleteProduct } from "../catalogActions";
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
import { NoData } from "../../common/presentation/noData";
import { isAuthorizedAsOneOf } from "../../../infrastructure/services/auth/authService";
import { sharedHistory } from "../../../infrastructure/sharedHistory";
import { getRoute } from "../../../utils/routeUtils";
import { routeLinks } from "../../layout/routes/routeLinks";
import { CommentsSection } from "../comments/commentsSection";
import { Link } from "react-router-dom";
import { Roles } from "../../../infrastructure/services/auth/roles";
import { notifications } from "../../toast/toast";
import { LabeledField } from "../../common/presentation/labeledField";
import { PresentLabel } from "../../common/presentation/presentLabel";
import { BinocularDetails } from "../../../dataModels/catalog/productDetails/binocularDetails";
import { BinocularCharacteristicsSection } from "./binocularCharacteristicsSection";
import { AccessoryCharacteristicsSection } from "./accessoryCharacteristicsSection";
import { AccessoryDetails } from "../../../dataModels/catalog/productDetails/accessoryDetails";

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
        } else if (productDetails?.category?.code === categoryCodes.binoculars) {
            return <BinocularCharacteristicsSection details={productDetails as BinocularDetails} />;
        } else if (productDetails?.category?.code === categoryCodes.accessories) {
            return <AccessoryCharacteristicsSection details={productDetails as AccessoryDetails} />;
        }
    };

    useAsyncEffect(async () => {
        setProductDetails(await getProductDetails(productId));
    }, []);

    const onDownloadFile = () => {
        if (!isAuthorizedAsOneOf([Roles.consumer, Roles.manager, Roles.staff])) {
            notifications.localizedWarning("NeedToAuthorize");
            sharedHistory.push(getRoute(routeLinks.account.login));
        }
    };

    return (
        <div className="product-details">
            <Row>
                <Col>
                    <h1 className="ui-page-header pt-2 pb-0">{productDetails?.name}</h1>
                </Col>
            </Row>
            <Row className="mb-3 pl-1">
                <Col>
                    <span className="sub-header-element">Бренд: {productDetails?.brand.name}</span>
                </Col>
            </Row>
            <Row className="product-details top-box row-cols-2 p-4 mb-3">
                <Col className="col-9 px-5">
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
                <Col className="col-3 ">
                    <Card className="product-card product-details-card mb-2 mx-auto">
                        <CardBody className="d-flex flex-column">
                        <div className="card-text">
                            <Row>
                                <Col>
                                    <LabeledField
                                        className="m-0 pb-0"
                                        labelKey={"Article"}
                                        value={productDetails?.code}
                                        supressMargins />
                                </Col>
                                <Col className="pl-0 d-flex align-items-center">
                                    {productDetails?.quantity === 0
                                        ? <PresentLabel icon="highlight_off" labelKey="NotInPresence" className="text-danger" />
                                        : <PresentLabel icon="check_circle_outline" labelKey="HaveInPresence" className="text-success" />}
                                </Col>
                            </Row>
                            <ProductRatingSection
                                className="mb-2"
                                productRating={productDetails?.rating} size="medium" />
                            {productDetails?.characteristics?.map((characteristic, ind) =>
                                <CardCharacteristic className="details-characteristic" key={ind} characteristic={characteristic}/>)}
                                <p className="mt-2">{productDetails?.shortDescription}</p>
                            </div>
                            <Row className="mt-auto align-items-center">
                                <Col className="col-7 align-self-center pl-0">
                                    <CardPrice
                                        value={productDetails?.price}
                                        currency={CurrencyType.BYN}
                                        showColouredBox />
                                </Col>
                                <Col className="col-5 align-self-center">
                                    <CardActionsSection
                                        onAddToCart={() => onAddToCart(productId)}
                                        onAddToFavorites={() => onAddToFavorites(productId)}
                                        onEditProduct={() => sharedHistory.push(getRoute(routeLinks.catalog.editProduct, productId))}
                                        onDeleteProduct={() => onDeleteProduct(productId)} />
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
                    {!isAuthorizedAsOneOf([Roles.consumer, Roles.manager, Roles.staff]) &&
                    <Row className="section-description">
                        <Col className="pl-0">
                            <p>
                                <Link to={getRoute(routeLinks.account.login)}>Войдите в аккаунт</Link>, чтобы скачать документы
                            </p>
                        </Col>
                    </Row>}
                    <Row>
                        <Col className="pl-0">
                            {!isEmpty(productDetails?.productFiles)
                                ? productDetails?.productFiles.map((productFile, index) => (
                                    <Row key={index}>
                                        <Col>
                                            <a
                                                href={isAuthorizedAsOneOf([Roles.consumer, Roles.manager, Roles.staff]) &&
                                                    getFileAnonymousDownloadLink(productFile.id)}
                                                onClick={() => onDownloadFile()}
                                                download={isAuthorizedAsOneOf([Roles.consumer, Roles.manager, Roles.staff])}>
                                                <span>{index + 1}. {productFile.name}</span>
                                            </a>
                                        </Col>
                                    </Row>
                                    ))
                                : <NoData localizationKey="NoInstructions"/>
                            }
                        </Col>
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