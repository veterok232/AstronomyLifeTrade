import React from "react";
import { Row, Col, Button } from "reactstrap";
import { Local } from "../localization/local";
import { logOut } from "../../infrastructure/services/identityService";
import { sharedHistory } from "../../infrastructure/sharedHistory";
import { getRoute } from "../../utils/routeUtils";
import { routeLinks } from "../layout/routes/routeLinks";

export const ManagerProfile = () => {
    const onLogout = async () => {
        await logOut();
        sharedHistory.push(getRoute(routeLinks.catalog.root));
    };

    return (
        <>
            <Row className="mb-3">
                <Col xs={4} >
                    <h1 className="ui-page-header pt-2"><Local id="ManagerProfile_Title" /></h1>
                </Col>
                <Col>
                    <Button onClick={onLogout} className="float-right">
                        <Local id="Logout" />
                    </Button>
                </Col>
            </Row>
        </>
    );
};