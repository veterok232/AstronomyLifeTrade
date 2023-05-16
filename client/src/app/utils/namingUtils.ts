import { camelToPascalCase } from "./stringUtils";

export const nameof = <T>(name: Extract<keyof T, string>): string => name;

export function getNameOfFunction<T>(): (name: Extract<keyof T, string>) => string {
    return (name: string) => name;
}

export function getNameOfFunctionPascalCase<T>(): (name: Extract<keyof T, string>) => string {
    return (name: string) => camelToPascalCase(name);
}