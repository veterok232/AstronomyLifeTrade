import { Constants } from "../components/constants";

export function calculatePageNumber(totalItemsCount: number, pageSize?: number): number {
    return Math.ceil(totalItemsCount / (pageSize ?? Constants.paging.defaultPageSize));
}

export function getPageItems<T>(pageNumber: number, pageSize: number, items: T[]): T[] {
    const from = (pageNumber - 1) * pageSize;
    const to = from + pageSize;

    return items.slice(from, to);
}