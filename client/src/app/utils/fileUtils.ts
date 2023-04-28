export const getFileNameWithoutExtension = (fileName: string) => {
    const lastDotPositions = fileName.lastIndexOf(".");

    return lastDotPositions === -1 ? fileName : fileName.substr(0, lastDotPositions);
};

export const getFileListSize = (files: FileList) => {
    return Array.from(files).reduce((total, file) => total + file.size, 0);
};

export const convertExtensionsListToAcceptString = (extensions: string[]) => {
    return extensions?.map(x => `.${x}`).join(",");
};

export const getFileExtension = (fileName: string) => {
    const indexOfExtension = fileName.lastIndexOf(".") + 1;

    return indexOfExtension ? fileName.substr(indexOfExtension) : undefined;
};

export const convertFileToBase64 = (file: File): Promise<string> => new Promise<string>((resolve, reject) => {
    const reader = new FileReader();
    reader.onerror = () => {
        reader.abort();
        reject(new Error("Error parsing file"));
    };

    reader.readAsArrayBuffer(file);
    reader.onload = () => {
        const bytes = Array.from(new Uint8Array(reader.result as ArrayBuffer));
        const base64StringFile = btoa(bytes.map((item) => String.fromCharCode(item)).join(""));
        resolve(base64StringFile);
    };
});