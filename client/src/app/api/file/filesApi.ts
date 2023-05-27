import { objToFormData } from "../../utils/formUtils";
import { getOneTimeLink } from "../../utils/oneTimeLinkUtils";
import { httpGetFile } from "../core/fileRequestApi";
import { apiRootUrl, httpPost } from "../core/requestApi";

const resourceName = "files";

export function getFileOneTimeDownloadLink(fileId: string): Promise<string> {
    return getOneTimeLink(`${resourceName}/download/${fileId}`);
}

export function downloadFile(fileId: string, skipRedirect?: boolean) {
    return httpGetFile({
        url: `${resourceName}/download/${fileId}`,
        skipRedirectForConcurrency: skipRedirect,
    });
}

export function uploadFile(file: File) {
    return httpPost({
        url: `${resourceName}/upload`,
        body: objToFormData({ file }),
        disableSuccessfulToast: true,
    });
}

export function getFileAnonymousDownloadLink(fileId: string): string {
    return `${apiRootUrl}/${resourceName}/download-anonymously/${fileId}`;
}