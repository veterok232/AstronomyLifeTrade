import React from "react";
import { IconicCartButton } from "../../common/controls/buttons/headerActionButtons/iconicCartButton";
import { IconicFavoritesButton } from "../../common/controls/buttons/headerActionButtons/iconicFavoritesButton";
import { IconicUserProfileButton } from "../../common/controls/buttons/headerActionButtons/iconicUserProfileButton";
import { Col, Row } from "reactstrap";
import { IconicAstronomicalCalculatorButton } from "../../common/controls/buttons/headerActionButtons/iconicAstronomicalCalculatorButton";
import { isManager, isStaff } from "../../../infrastructure/services/auth/authService";
import { IconicOrdersButton } from "../../common/controls/buttons/headerActionButtons/iconicOrdersButton";

interface Props {
    className?: string;
    onCartOpen: () => void;
    onUserProfileOpen: () => void;
    onFavoritesOpen: () => void;
    onAstronomicalCalculatorOpen: () => void;
    onOrdersOpen: () => void;
    onManagerProfileOpen: () => void;
}

export const HeaderActionButtons = (props: Props) => {
    return (
        <Row className={props.className}>
            {!isManager() && !isStaff() && <>
                <Col className="align-content-center m-0 p-0">
                    <IconicAstronomicalCalculatorButton className="header__button m-0 p-0" onAstronomicalCalculatorOpen={props.onAstronomicalCalculatorOpen} />
                </Col>
                <Col className="align-content-center m-0 p-0">
                    <IconicFavoritesButton className="header__button m-0 p-0" onFavoritesOpen={props.onFavoritesOpen} />
                </Col>
                <Col className="align-content-center m-0 p-0">
                    <IconicCartButton className="header__button m-0 p-0" onCartOpen={props.onCartOpen} />
                </Col>
                <Col className="align-content-center m-0 p-0">
                    <IconicUserProfileButton className="header__button m-0 p-0" onUserProfileOpen={props.onUserProfileOpen} />
                </Col>
            </>}
            {isManager() && <>
                <Col className="align-content-center m-0 p-0">
                    <IconicOrdersButton className="header__button m-0 p-0" onOrdersOpen={props.onOrdersOpen} />
                </Col>
                <Col className="align-content-center m-0 p-0">
                    <IconicUserProfileButton className="header__button m-0 p-0" onUserProfileOpen={props.onManagerProfileOpen} />
                </Col>
            </>}
        </Row>
    );
};