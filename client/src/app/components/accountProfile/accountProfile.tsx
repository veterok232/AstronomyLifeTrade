import React from "react";
import { Local } from "../localization/local";
import { Button, Form } from "reactstrap";
import { contextStore } from "../../infrastructure/stores/contextStore";
import { logOut } from "../../infrastructure/services/identityService";
import { notifications } from "../toast/toast";
import { sharedHistory } from "../../infrastructure/sharedHistory";
import { getRoute } from "../../utils/routeUtils";
import { routeLinks } from "../layout/routes/routeLinks";

export const AccountProfilePage = () => {
    const onSubmit = async () => {
        await logOut();
        sharedHistory.push(getRoute(routeLinks.catalog.root));
        notifications.localizedSuccess("LogoutSuccessfully");
    };

    return (
        <div className="account-profile">
            <h1><Local id="AccountProfile_Title"/></h1>
            <Form onSubmit={onSubmit}>
                <p>Это ваш личный кабинет, {contextStore.fullName}</p>
                <Button type="submit">
                    <Local id="Logout" />
                </Button>
            </Form>
        </div>
    );
};