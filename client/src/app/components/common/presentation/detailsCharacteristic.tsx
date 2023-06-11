import React from "react";
import { Row, Col } from "reactstrap";

interface Props {
    name: string;
    value: string | JSX.Element;
    className?: string;
    isEditMode?: boolean;
}

export const DetailsCharacteristic = (props: Props) => {
    return (
        <Row className={`characteristic-item ${props.className}`}>
            <Col className={`name d-flex align-items-center pl-0 ${props.isEditMode ? "col-6" : "col-8"}`}>
                <span>{props.name}: </span>
            </Col>
            <Col className={`d-flex align-items-center pl-2 ${props.isEditMode ? "col-6" : "col-4"}`}>
                <span>{props.value}</span>
            </Col>
        </Row>
    );
};