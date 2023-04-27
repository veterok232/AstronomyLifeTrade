import { downloadFileByLink } from "../../infrastructure/services/fileService";
import { getOneTimeLink } from "../../utils/oneTimeLinkUtils";

export interface FileRequestOptions {
    url: string;
    skipRedirectForConcurrency?: boolean;
}

export async function httpGetFile(options: FileRequestOptions) {
    const oneTimeLink = await getOneTimeLink(options.url);
    downloadFileByLink(oneTimeLink, options.skipRedirectForConcurrency);
}