import React from "react";
import { observer, Provider } from "mobx-react";

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
                    <AffectedApplicationsConfirmationModal />
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