import { Pageable } from "../common/pageable";
import { Sortable } from "../common/sortable";
import { OrdersFilterData } from "./ordersFilterData";

export type OrdersSearchRequest = OrdersFilterData & Sortable & Pageable;