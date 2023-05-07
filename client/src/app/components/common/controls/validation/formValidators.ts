import { notifications } from "../../../toast/toast";

export const showNotificationIfInvalid = (valid: boolean) => {
    if (!valid) {
        notifications.invalidFormError();
    }
};