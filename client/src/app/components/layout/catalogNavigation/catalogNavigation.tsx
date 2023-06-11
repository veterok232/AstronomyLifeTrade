import { observer } from "mobx-react-lite";
import React from "react";
import { viewStore } from "../../../infrastructure/stores/viewStore";
import { Local } from "../../localization/local";
import MenuToggle from "../menuToggle/menuToggle";
import { NavigationItemsCollection } from "./navigationItemsCollection";
import { getCatalogMenuItems } from "./catalogNavigationConfig";
import { Col, Row } from "reactstrap";

const CatalogNavigation = observer(() => {
    return <nav className={`grid__navigation navigation ${viewStore.isNavigationOpened ? "grid__navigation--opened navigation--opened" : ""}`}>
        <div className="navigation__header">
            <img className="navigation__logo" src="static/images/logo.png" />
            <MenuToggle className="navigation__menu-toggle" />
        </div>
        <h4 className="navigation__title"><Local id="AstronomyLifeTagline" /></h4>
        <NavigationItemsCollection items={getCatalogMenuItems()} />
        <hr className="mx-2"/>
        <Row className="navigation-section">
            <Col>
                <Row className="navigation-section-header mx-1 mb-2">
                    <Col className="pl-0 d-flex justify-content-center">
                        <span className="font-weight-bold">Добро пожаловать!</span>
                    </Col>
                </Row>
                <Row className="navigation-section-body mx-1">
                    <Col>
                        <p className="m-0">Мы доставляем товары по всей Беларуси курьером!</p>
                        <p className="m-0">Адрес пункта самовывоза: г. Минск, ул. Уличная 1</p>
                        <p className="m-0">Номер телефона: +375 (29) 111-11-11</p>
                    </Col>
                </Row>
            </Col>
        </Row>
    </nav>;
});

export default CatalogNavigation;