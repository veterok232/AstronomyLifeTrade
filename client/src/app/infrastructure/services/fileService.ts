import { routeLinks } from "../../components/layout/routes/routeLinks";
import { notifications } from "../../components/toast/toast";
import { sharedHistory } from "../sharedHistory";

const tempFrameElementRemovingTimeoutInMilliseconds = 3 * 60 * 1000; // 3 min

export function downloadFileByLink(link: string, skipRedirect?: boolean) {
    downloadViaFrame(link, skipRedirect);
}

function downloadViaLink(link: string) {
    const tempAnchorElement = window.document.createElement("a");
    tempAnchorElement.href = link;
    tempAnchorElement.download = "";
    tempAnchorElement.target = "_self";
    tempAnchorElement.click();
    tempAnchorElement.remove();
}

function downloadViaFrame(link: string, skipRedirect?: boolean) {
    const tempFrameElement = window.document.createElement("iframe");
    tempFrameElement.style.display = "none";
    tempFrameElement.src = link;

    // Called when file downloading is failed.
    tempFrameElement.onload = () => {
        notifications.localizedError("RetrieveDataConcurrencyError");

        if (!skipRedirect) {
            sharedHistory.push(routeLinks.errors.notFound);
        }
    };

    window.document.body.appendChild(tempFrameElement);
    setTimeout(() => {
        tempFrameElement.remove();
    }, tempFrameElementRemovingTimeoutInMilliseconds);
}