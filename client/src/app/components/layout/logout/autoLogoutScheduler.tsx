import React from "react";
import { crossWindowEventBroker } from "../../../infrastructure/services/crossWindowEventBroker";
import { Constants } from "../../constants";
import { contextStore } from "../../../infrastructure/stores/contextStore";
import { extendSessionRefreshToken } from "../../../infrastructure/services/identityService";
import { getSecondsToAutoLogout } from "./autoLogoutTimeoutCalculator";
import { AutoLogoutConfirmationModal } from "./autoLogoutConfirmationModal";

interface State {
    isModalOpened: boolean;
}

interface Props {
    expirationTime: Date;
}

export class AutoLogoutScheduler extends React.Component<Props, State> {
    private preventiveCheckInterval: NodeJS.Timeout | null = null;
    private extendSessionListenerReference: string = null;

    constructor(props: Props) {
        super(props);

        this.state = { isModalOpened: false };
    }

    public componentDidMount() {
        this.extendSessionListenerReference = crossWindowEventBroker.subscribe(
            Constants.crossWindowEvents.sessionExtended, this.onSessionExtendedExternally);
        this.setPreventiveCheckInterval();
    }

    public componentWillUnmount() {
        crossWindowEventBroker.unsubscribe(this.extendSessionListenerReference);
        this.clearPreventiveCheckInterval();
    }

    public componentDidUpdate(prevProps: Props) {
        if (prevProps.expirationTime != this.props.expirationTime) {
            this.setInitialState();
        }
    }

    private onSessionExtendedExternally = (newExpiryDateTime: string) => {
        contextStore.setRefreshTokenExpirationDateTime(newExpiryDateTime);
        this.setInitialState();
    };

    private displayModal = () => {
        this.setState({ isModalOpened: true });
    };

    private handleStayOption = async () => {
        await extendSessionRefreshToken();
        this.setInitialState();
    };

    private setInitialState = () => {
        this.setState({ isModalOpened: false });
        this.setPreventiveCheckInterval();
    };

    private clearPreventiveCheckInterval = () => {
        clearInterval(this.preventiveCheckInterval);
    };

    private performPreventiveCheck = () => {
        const secondsToAutoLogout = getSecondsToAutoLogout();

        if (secondsToAutoLogout < 0) {
            location.reload();

            return;
        }

        if (secondsToAutoLogout < Constants.autoLogoutTimerInSec) {
            this.displayModal();
            this.clearPreventiveCheckInterval();
        }
    };

    private setPreventiveCheckInterval = () => {
        this.clearPreventiveCheckInterval();

        this.preventiveCheckInterval = global.setInterval(
            this.performPreventiveCheck,
            Constants.millisecondsInSecond);
    };

    public render() {
        return <AutoLogoutConfirmationModal isOpen={this.state.isModalOpened} handleStay={this.handleStayOption} />;
    }
}
