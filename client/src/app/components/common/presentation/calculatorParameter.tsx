import React from "react";
import { Row, Col } from "reactstrap";
import { localizer } from "../../localization/localizer";

interface Props {
    name: string;
    value: string;
}

export const CalculatorParameter = (props: Props) => {
    return (
        <Row className="characteristic-item">
            <Col className="name d-flex align-items-center pl-0 col-3">
                <span>{localizer.get(props.name)}: </span>
            </Col>
            <Col className="d-flex align-items-center pl-2 col-9">
                <span>{props.value}</span>
            </Col>
        </Row>
    );
};