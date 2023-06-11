import { downloadFileByLink } from "../../infrastructure/services/fileService";

export interface FileRequestOptions {
    url: string;
    skipRedirectForConcurrency?: boolean;
}

export function httpGetFile(options: FileRequestOptions) {
    downloadFileByLink(options.url, options.skipRedirectForConcurrency);
}