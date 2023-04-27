export const localizationKeyName = "_localizationName";

export const registerEnumForLocalization = (registrationObject: any) => {
    let keysCount = 0;
    let firstKeyName = "";
    for (const key in registrationObject) {
        if (keysCount > 1) {
            break;
        }
        firstKeyName = key;
        keysCount++;
    }
    if (keysCount > 1) {
        throw new Error("Invalid enumeration registration for localization");
    }
    registrationObject[firstKeyName][localizationKeyName] = firstKeyName;
};

export const getEnumLocalizationKey = (enumObject: any, enumValue: any) => {
    if (!enumObject[localizationKeyName]) {
        throw new Error("Enumeration is not registered for localization");
    }
    return `${enumObject[localizationKeyName]}.${enumObject[enumValue]}`;
};