import React from "react";
import { observer, Provider } from "mobx-react";
import { initRefreshSynchronization } from "./infrastructure/services/auth/refreshTokenService";
import { initializeFingerprintAgent } from "./infrastructure/services/auth/fingerprintService";
import { refreshToken } from "./infrastructure/services/identityService";
import { contextActions } from "./infrastructure/services/contextService";
import { initExternalLogoutSubscription } from "./infrastructure/services/auth/externalLogoutService";
import { initExternalLoginSubscription } from "./infrastructure/services/auth/externalLoginService";
import { stores } from "./infrastructure/stores";
import { RawIntlProvider } from "react-intl";
import { Localizer } from "./components/localization/localizer";
import { contextStore } from "./infrastructure/stores/contextStore";
import { sharedHistory } from "./infrastructure/sharedHistory";
import { viewStore } from "./infrastructure/stores/viewStore";
import { AutoLogoutScheduler } from "./components/layout/logout/autoLogoutScheduler";
import { Router } from "./components/layout/routes/router";
import Header from "./components/layout/header/header";

@observer
export class App extends React.Component {
    public async componentDidMount() {
        initRefreshSynchronization();
        initExternalLogoutSubscription();
        initExternalLoginSubscription();
        await initializeFingerprintAgent();
        await refreshToken();
        await contextActions.load();
    }

    public render() {
        let contextDependentComponents = null;

        if (stores.contextStore.isContextLoaded) {
            contextDependentComponents = <RawIntlProvider value={Localizer.configureIntl(contextStore.lang)}>
                <Router history={sharedHistory.history}>
                    {stores.contextStore.currentAssignment && stores.contextStore.currentAssignment.assignmentId
                        ? <div className={`grid ${viewStore.isNavigationOpened ? "grid--navigation-opened" : ""}`}>
                            <AutoLogoutScheduler expirationTime={stores.contextStore.refreshTokenExpirationDateTime} />
                            <Header />
                            <Navigation />
                            <MainContent />
                            <Footer />
                        </div>
                        : <ExternalPageLayout />}
                    <NotificationContainer />
                    <ModalsContainer />
                </Router>
            </RawIntlProvider>;
        }

        return (
            <Provider {...stores}>
                {contextDependentComponents}
                <ProgressIndicator />
            </Provider>
        );
    }
}

export default App;