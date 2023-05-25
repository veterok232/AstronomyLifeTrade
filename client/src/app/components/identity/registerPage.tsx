import React from "react";
import { FormRenderProps, Form as FinalForm } from "react-final-form";
import { Link } from "react-router-dom";
import { Form, Button } from "reactstrap";
import { SmallContainerWrapper } from "../common/containers/smallContainerWrapper";
import { EmailFormControl } from "../common/controls/formControls/emailFormControl";
import { PasswordFormControl } from "../common/controls/formControls/passwordFormControl";
import { showNotificationIfInvalid } from "../common/controls/validation/formValidators";
import { required, composeValidators, defaultTextFieldMaxLength, nonWhitespace, requiredNonWhitespace } from "../common/controls/validation/validators";
import { routeLinks } from "../layout/routes/routeLinks";
import { Local } from "../localization/local";
import { localizer } from "../localization/localizer";
import { TextFormControl } from "../common/controls/formControls/textFormControl";
import { PhoneFormControl } from "../common/controls/formControls/maskedFormControls/phoneFormControl";
import { register } from "../../api/identity/identityApi";
import { sharedHistory } from "../../infrastructure/sharedHistory";
import { getDefaultPageRoute } from "../../utils/routeUtils";

export interface UserRegistrationModel {
    email: string;
    password: string;
    retypedPassword: string;
    firstName: string;
    lastName: string;
    phone: string;
}

export const RegisterPage = () => {
    const handleSubmit = async (model: UserRegistrationModel) => {
        const response = await register(model);

        if (response.isSucceeded) {
            sharedHistory.push(getDefaultPageRoute());
        }
    };

    return (
        <FinalForm
            onSubmit={handleSubmit}
            render={({ valid, handleSubmit }: FormRenderProps<UserRegistrationModel>) => (
                <SmallContainerWrapper titleKey="Register_Title">
                    <Form onSubmit={handleSubmit}>
                        <TextFormControl label="FirstName" name="firstName" validator={requiredNonWhitespace} markRequired />
                        <TextFormControl label="LastName" name="lastName" validator={nonWhitespace} markRequired />
                        <EmailFormControl name="email" label="Email" placeholder={localizer.get("Login_EmailPlaceholder")}
                            validator={required} skipEmailFormatValidation markRequired />
                        <PhoneFormControl label="PhoneNumber" name="phone" markRequired />
                        <PasswordFormControl name="password" label="Password" placeholder={localizer.get("Login_PasswordPlaceholder")}
                            validator={composeValidators(required, defaultTextFieldMaxLength)} skipPasswordFormatValidation />
                        <PasswordFormControl name="retypedPassword" label="RetypePassword" placeholder={localizer.get("Login_PasswordPlaceholder")}
                            validator={composeValidators(required, defaultTextFieldMaxLength)} skipPasswordFormatValidation />
                        <Button className="button-primary w-100 mt-4" type="submit" onClick={() => showNotificationIfInvalid(valid)}>
                            <Local id="Register" />
                        </Button>
                        <Link className="btn btn-outline-secondary w-100 mt-3" to={routeLinks.account.login}>
                            <Local id="Login" />
                        </Link>
                    </Form>
                </SmallContainerWrapper>)}
        />
    );
};