import { SortOrder } from "../enums/sortOrder";

export interface Sortable {
    sortBy: string;
    direction: SortOrder;
}