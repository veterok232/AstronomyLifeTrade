import React from "react";
import { IconicCartButton } from "../../common/controls/buttons/headerActionButtons/iconicCartButton";
import { IconicFavoritesButton } from "../../common/controls/buttons/headerActionButtons/iconicFavoritesButton";
import { IconicUserProfileButton } from "../../common/controls/buttons/headerActionButtons/iconicUserProfileButton";
import { Col, Row } from "reactstrap";

interface Props {
    className?: string;
    onCartOpen: () => void;
    onUserProfileOpen: () => void;
    onFavoritesOpen: () => void;
    onAstronomicalCalculatorOpen: () => void;
}

export const HeaderActionButtons = (props: Props) => {
    return (
        <Row className={props.className}>
            <Col className="align-content-center m-0 p-0">
                <IconicFavoritesButton className="header__button m-0 p-0" onFavoritesOpen={props.onFavoritesOpen} />
            </Col>
            <Col className="align-content-center m-0 p-0">
                <IconicCartButton className="header__button m-0 p-0" onCartOpen={props.onCartOpen} />
            </Col>
            <Col className="align-content-center m-0 p-0">
                <IconicUserProfileButton className="header__button m-0 p-0" onUserProfileOpen={props.onUserProfileOpen} />
            </Col>
        </Row>
    );
};