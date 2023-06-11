import { Pageable } from "../common/pageable";
import { Sortable } from "../common/sortable";
import { BinocularFilterData } from "./binocularFilterData";

export type BinocularsSearchRequest = BinocularFilterData & Sortable & Pageable;