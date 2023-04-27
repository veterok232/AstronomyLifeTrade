import { isEmpty } from "lodash";
import { Result } from "../dataModels/common/result";
import { notifications } from "../components/toast/toast";
import { localizer } from "../components/localization/localizer";
import { parseISO } from "date-fns";

function handleErrorResultInternal<TResult>(
    result: Result<TResult>,
    notifyErrorAction: (error: string) => void,
    notifyWarningAction: (error: string) => void,
    shouldThrow = false,
) {
    if (result.isSucceeded) {
        if (!isEmpty(result.errors)) {
            result.errors.forEach((error) => error && notifyWarningAction(error));
        }

        return;
    }

    isEmpty(result.errors)
        ? notifications.defaultRequestError()
        : result.errors.forEach((error) => error && notifyErrorAction(error));

    if (shouldThrow) {
        throw new Error(isEmpty(result.errors)
            ? localizer.get("DefaultRequestError")
            : result.errors.map(e => localizer.get(e)).join(".\n"));
    }
}

export function throwIfErrorResult<TResult>(result: Result<TResult>) {
    handleErrorResultInternal(result, e => notifications.localizedError(e), e => notifications.localizedWarning(e), true);
}

export function handleErrorResult<TResult>(result: Result<TResult>) {
    handleErrorResultInternal(result, e => notifications.localizedError(e),  e => notifications.localizedWarning(e));
}

export function handleLocalizedErrorResult<TResult>(result: Result<TResult>) {
    handleErrorResultInternal(result, e => notifications.error(e),  e => notifications.warning(e));
}

export function handleDateResponse(date: Date): Date {
    return parseISO(date.toString());
}