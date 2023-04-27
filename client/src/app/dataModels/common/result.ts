export class Result<T = {}> {
    data: T;
    isSucceeded: boolean;
    errors: string[];
}