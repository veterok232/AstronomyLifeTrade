import { Constants } from "../../../components/constants";
import { crossWindowEventBroker } from "../crossWindowEventBroker";

const onExternalLogOut = () => {
    location.reload();
};

export const initExternalLogoutSubscription = () => {
    crossWindowEventBroker.subscribe(
        Constants.crossWindowEvents.logOutCompleted, onExternalLogOut);
};