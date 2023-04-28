import { Constants } from "../../../components/constants";
import { crossWindowEventBroker } from "../crossWindowEventBroker";
import { clearAccessToken } from "./accessTokenService";

const onExternalLogin = () => {
    clearAccessToken();
    // showAssignmentChangedConfirmation();
};

export const initExternalLoginSubscription = () => {
    crossWindowEventBroker.subscribe(
        Constants.crossWindowEvents.logInCompleted, onExternalLogin);
};