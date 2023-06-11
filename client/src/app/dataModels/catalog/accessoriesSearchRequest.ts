import { Pageable } from "../common/pageable";
import { Sortable } from "../common/sortable";
import { AccessoriesFilterData } from "./accessoriesFilterData";

export type AccessoriesSearchRequest = AccessoriesFilterData & Sortable & Pageable;