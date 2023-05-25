import React from "react";
import { applyNewIdentity, login } from "../../infrastructure/services/identityService";
import { sharedHistory } from "../../infrastructure/sharedHistory";
import { Form as FinalForm, FormRenderProps } from "react-final-form";
import { SmallContainerWrapper } from "../common/containers/smallContainerWrapper";
import { Button, Form } from "reactstrap";
import { EmailFormControl } from "../common/controls/formControls/emailFormControl";
import { PasswordFormControl } from "../common/controls/formControls/passwordFormControl";
import { composeValidators, defaultTextFieldMaxLength, required } from "../common/controls/validation/validators";
import { localizer } from "../localization/localizer";
import { Link } from "react-router-dom";
import { routeLinks } from "../layout/routes/routeLinks";
import { Local } from "../localization/local";
import { showNotificationIfInvalid } from "../common/controls/validation/formValidators";

interface LoginFormModel {
    email: string;
    password: string;
}

export const LoginPage = () => {
    const handleSubmit = async (model: LoginFormModel) => {
        const response = await login(model.email, model.password);
        if (response) {
            await applyNewIdentity(response, sharedHistory.getSearchString());
        }
    };

    return (
        <FinalForm
            onSubmit={handleSubmit}
            render={({ valid, handleSubmit }: FormRenderProps<LoginFormModel>) => (
                <SmallContainerWrapper titleKey="Login_Title">
                    <Form onSubmit={handleSubmit}>
                        <EmailFormControl name="email" label="Email" placeholder={localizer.get("Login_EmailPlaceholder")}
                            validator={required} skipEmailFormatValidation />
                        <PasswordFormControl name="password" label="Password" placeholder={localizer.get("Login_PasswordPlaceholder")}
                            validator={composeValidators(required, defaultTextFieldMaxLength)} skipPasswordFormatValidation />
                        <Link className=" text-secondary mb-3" to={routeLinks.account.forgotPassword}><Local id="ForgotPasswordLink" /></Link>
                        <Button className="w-100 mt-4" type="submit" onClick={() => showNotificationIfInvalid(valid)}>
                            <Local id="Login" />
                        </Button>
                        <Link className="btn btn-outline-secondary w-100 mt-3" to={routeLinks.account.register}>
                            <Local id="Register" />
                        </Link>
                    </Form>
                </SmallContainerWrapper>)}
        />
    );
};