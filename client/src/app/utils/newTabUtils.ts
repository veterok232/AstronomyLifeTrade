import { notifications } from "../components/toast/toast";

export const openLinkInNewTab = (link: string) => {
    const newWindow = window.open(link, "_blank");

    if (!newWindow) {
        notifications.localizedError("PopUpsBlockedNotification");

        return;
    }

    newWindow.focus();
};