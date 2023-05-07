import { localizer } from "../components/localization/localizer";

export const localizeYesNo = (value: boolean) => {
    return localizer.get(value ? "Yes" : "No");
};

export const localizeNotAvailableShort = () => {
    return localizer.get("NotAvailableShort");
};