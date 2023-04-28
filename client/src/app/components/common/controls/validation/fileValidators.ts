import { getFileExtension, getFileListSize } from "../../../../utils/fileUtils";
import { Constants } from "../../../constants";
import { localizer } from "../../../localization/localizer";

const validateFileSize = (
    fileSize: number,
    sizeLimit = Constants.uploadFilesSizeLimitInBytes,
    sizeLimitErrorMessage = "UploadFilesSizeLimitError"
) => {
    if (fileSize > sizeLimit) {
        return localizer.get(sizeLimitErrorMessage, { sizeLimit: sizeLimit / 1024 / 1024 });
    }

    if (fileSize === 0) {
        return localizer.get("UploadFileZeroByteSizeError", { sizeLimit: sizeLimit / 1024 / 1024 });
    }

    return undefined;
};

export const fileSizeValidator = (file?: File) => file ? validateFileSize(file.size) : undefined;

export const imageSizeValidator = (file?: File) => file
    ? validateFileSize(file.size, Constants.uploadImageSizeLimitInBytes)
    : undefined;

export const fileListSizeValidator = (files?: FileList) => files ? validateFileSize(getFileListSize(files)) : undefined;

export const fileMimeTypeValidator = (file?: File) => {
    if (file &&
        !Constants.availableDocumentFilesExtensions.some(extension => getFileExtension(file.name) === extension)) {
        return localizer.get("FileExtensionValidation");
    }
};

export const filesListMimeTypeValidator = (files?: FileList) => {
    if (!files?.length) {
        return undefined;
    }

    for (const file of Array.from(files)) {
        const error = fileMimeTypeValidator(file);

        if (error) {
            return error;
        }
    }
};