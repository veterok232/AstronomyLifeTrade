import React from "react";
import { PropsWithChildren } from "react";
import { Col, Container, Label, Row } from "reactstrap";
import { BackButton } from "../controls/buttons/backButton";
import { Local } from "../../localization/local";

interface Props {
    titleKey: string;
    className?: string;
    withBackButton?: boolean;
    onCancel?: () => void;
    backButtonLabelKey?: string;
}

export const SmallContainerWrapper = (props: PropsWithChildren<Props>) => {
    return (
        <Container className={`small-container p-3 p-sm-5 ${props.className || ""} ${props.withBackButton ? "with-back-button" : ""}`}>
            {props.withBackButton && <BackButton onCancel={props.onCancel} labelKey={props.backButtonLabelKey} />}
            <Row>
                <Col>
                    <img className="logo" src="/static/images/logo.png"></img>
                </Col>
                <Col className="">
                    <Label className="mt-4 tagline"><Local id="AstronomyLifeTagline" /></Label>
                </Col>
            </Row>
            <hr />
            <h4 className="title"><Local id={props.titleKey} /></h4>
            {props.children}
        </Container>
    );
};