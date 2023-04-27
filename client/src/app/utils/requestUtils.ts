import { Result } from "../dataModels/common/result";

export function isEmptyOrSucceeded<TResult>(
    result: TResult
): boolean {
    // if result is empty (just ok status) OR result is assignable from Result.ts and also succeded => true
    return !result || (result as unknown as Result).isSucceeded !== false;
}