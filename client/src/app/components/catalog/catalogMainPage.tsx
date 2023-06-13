import React from "react";
import { PopularProductsSection } from "./popularProductsSection/popularProductsSection";
import { localizer } from "../localization/localizer";
import { Row, Col} from "reactstrap";
import Slider from "react-slick";
import { Local } from "../localization/local";

const sliderSettings = {
    dots: true,
    dotsClass: "slick-dots",
    infinite: true,
    speed: 2000,
    slidesToShow: 1,
    slidesToScroll: 1,
    arrows: false,
    autoplay: true,
    waitForAnimate: true,
};

export const CatalogMainPage = () => {
    return (
        <div className="catalog-main-page">
            <Row className="mb-3">
                <Col className="px-0">
                    <Slider {...sliderSettings}>
                        <div>
                            <img className="main-page-image w-100" src="static/images/main_page/resized/cut/1.jpg" />
                        </div>
                        <div>
                            <img className="main-page-image w-100" src="static/images/main_page/resized/cut/2.jpg" />
                        </div>
                        <div>
                            <img className="main-page-image w-100" src="static/images/main_page/resized/cut/3.jpg" />
                        </div>
                        <div>
                            <img className="main-page-image w-100" src="static/images/main_page/resized/cut/4.jpg" />
                        </div>
                    </Slider>
                </Col>
            </Row>
            <Row>
                <Col>
                    <h2 className="ui-page-subheader mt-4 pt-2"><Local id="AboutShopTitle" /></h2>
                </Col>
            </Row>
            <Row>
                <Col>
                    <p className="section-description">
                        {localizer.get("MainPageDescription")}
                    </p>
                </Col>
            </Row>
            <PopularProductsSection />
            <Row>
                <Col>
                    <h5 className="main-page-end-text text-center mt-4"><Local id="MainPageEndText" /></h5>
                </Col>
            </Row>
        </div>
    );
};