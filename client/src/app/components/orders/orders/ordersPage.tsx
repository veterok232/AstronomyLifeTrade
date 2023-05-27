import React from "react";
import { Row, Col } from "reactstrap";
import { Local } from "../../localization/local";

export const OrdersPage = () => {
    return (
        <>
            <Row className="mb-3">
                <Col xs={4} >
                    <h1 className="ui-page-header pt-2"><Local id="OrdersPage_Title" /></h1>
                </Col>
            </Row>
        </>
    );
};