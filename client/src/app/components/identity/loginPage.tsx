export const LoginPage = () => {
    const isUserDeactivated: boolean = useMemo(() =>
        new URLSearchParams(window.location.search).get("assignment-deactivated") === true.toString(), []);

    const handleSubmit = async (model: LoginFormModel) => {
        const response = await login(model.email, model.password);
        if (response) {
            await applyNewIdentity(response, sharedHistory.getSearchString());
        }
    };

    if (isUserDeactivated) {
        notifications.localizedError("LoginError_InactiveAccount");
    }

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
                        <Link className="text-primary mb-3" to={routeLinks.account.forgotPassword}><Local id="ForgotPasswordLink" /></Link>
                        <Button className="button-primary w-100 mt-4" type="submit" onClick={() => showNotificationIfInvalid(valid)}>
                            <Local id="Login" />
                        </Button>
                        <Link className="btn button-secondary w-100 mt-3" to={routeLinks.account.registerConsumer}>
                            <Local id="RegisterAsConsumer" />
                        </Link>
                    </Form>
                </SmallContainerWrapper>)}
        />
    );
};