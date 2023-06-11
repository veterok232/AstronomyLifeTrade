import { Pageable } from "../common/pageable";
import { Sortable } from "../common/sortable";
import { TelescopesFilterData } from "./telescopesFilterData";

export type TelescopesSearchRequest = TelescopesFilterData & Sortable & Pageable;