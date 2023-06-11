import { Pageable } from "../common/pageable";
import { Sortable } from "../common/sortable";
import { ProductsFilterData } from "./productsFilterData";

export type ProductsSearchRequest = ProductsFilterData & Sortable & Pageable;