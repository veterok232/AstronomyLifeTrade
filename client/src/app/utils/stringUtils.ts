export const stringComparer = (a: string, b: string) => {
    if (a > b) {
        return 1;
    }

    if (a < b) {
        return -1;
    }

    return 0;
};

export function generateRandomString() {
    return Math.random().toString(36).replace("0.", "");
}

export const equalsIgnoreCase = (a: string, b: string): boolean => a?.toUpperCase() === b?.toUpperCase();

export const pascalToCamelCase = (input: string) => input.charAt(0).toLowerCase() + input.substring(1);

export const camelToPascalCase = (input: string) => input.charAt(0).toUpperCase() + input.substring(1);

export function generateElementId() {
    return `id_${generateRandomString()}`;
}