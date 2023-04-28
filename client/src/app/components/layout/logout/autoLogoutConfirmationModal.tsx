import React from "react";
import { getSecondsToAutoLogout } from "./autoLogoutTimeoutCalculator";
import { logOut, removeIdentityData } from "../../../infrastructure/services/identityService";
import { routeLinks } from "../routes/routeLinks";
import { Constants } from "../../constants";
import { Button, Modal, ModalBody, ModalFooter } from "reactstrap";
import { ModalHeaderControl } from "../../common/controls/modals/modalHeaderControl";
import { Local } from "../../localization/local";

interface Props {
    handleStay: () => void;
    isOpen: boolean;
}

interface State {
    remainingTimeInSeconds: number;
}

const modalZIndex = 9999998;

const getTruncatedSecondsToAutoLogout = () => {
    return Math.trunc(getSecondsToAutoLogout());
};

export class AutoLogoutConfirmationModal extends React.Component<Props, State> {
    private countdownInterval: NodeJS.Timeout | null = null;

    constructor(props: Props) {
        super(props);

        this.state = this.getInitialState();
    }

    public componentDidUpdate(prevProps: Props) {
        if (!prevProps.isOpen && this.props.isOpen) {
            this.initCountdownInterval();
            this.setState(this.getInitialState());
        }

        if (prevProps.isOpen && !this.props.isOpen) {
            this.clearCountdownInterval();
        }
    }

    public componentWillUnmount() {
        this.clearCountdownInterval();
    }

    private getInitialState = (): State => {
        return {
            remainingTimeInSeconds: getTruncatedSecondsToAutoLogout(),
        };
    };

    private onCountdownTick = () => {
        const secondsLeft = getTruncatedSecondsToAutoLogout();

        if (secondsLeft > 0) {
            this.setState({ remainingTimeInSeconds: secondsLeft });
        } else {
            this.clearCountdownInterval();
            // at this point user session already expired so we need to remove expired identity info
            removeIdentityData();
            window.location.href = routeLinks.login;
        }
    };

    private initCountdownInterval = () => {
        this.countdownInterval = global.setInterval(this.onCountdownTick, Constants.millisecondsInSecond);
    };

    private clearCountdownInterval = () => clearInterval(this.countdownInterval);

    public stayHandler = () => {
        this.setState(this.getInitialState());
        this.clearCountdownInterval();
        this.props.handleStay();
    };

    public render() {
        return (<Modal isOpen={this.props.isOpen} className="auto-logout-confirmation-modal" zIndex={modalZIndex}>
            <ModalHeaderControl>
                <p className="text-center mb-0">
                    <b><Local id={"AutoLogoutConfirmationModal_Header"} /></b>
                </p>
                <hr />
            </ModalHeaderControl>
            <ModalBody className="text-center">
                <Local id={"AutoLogoutConfirmationModal_BodyTemplate"}
                    values={{ remainingSeconds: this.state.remainingTimeInSeconds.toString() }} />
            </ModalBody>
            <ModalFooter
                className="border-top-0 d-flex justify-content-center mb-4 flex-column-reverse flex-sm-row align-items-stretch">
                <Button onClick={logOut} className="button-secondary">
                    <Local id={"Logout"} />
                </Button>
                <Button onClick={this.stayHandler} className="button-primary">
                    <Local id={"AutoLogoutConfirmationModal_StayButton"} />
                </Button>
            </ModalFooter>
        </Modal>);
    }
}