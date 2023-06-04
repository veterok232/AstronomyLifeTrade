import React, { useMemo, useState } from "react";
import { Row, Col } from "reactstrap";
import { Local } from "../../localization/local";
import { OrdersFilterData } from "../../../dataModels/orders/ordersFilterData";
import { OrderStatus } from "../../../dataModels/enums/orderStatus";
import { Sortable } from "../../../dataModels/common/sortable";
import useAsyncEffect from "use-async-effect";
import { ListResult } from "../../../dataModels/common/listResult";
import { Pageable } from "../../../dataModels/common/pageable";
import { SortOrder } from "../../../dataModels/enums/sortOrder";
import { calculatePageNumber } from "../../../utils/paginationUtils";
import { PaginationControl } from "../../common/controls/pagination/paginationControl";
import Sorting from "../../common/controls/sorting/sorting";
import { FilterData, ListRequestHandler } from "../../common/lists/listRequestHandler";
import { NoData } from "../../common/presentation/noData";
import { Constants } from "../../constants";
import { OrderListItem } from "../../../dataModels/orders/orderListItem";
import { searchOrders } from "../../../api/orders/ordersApi";
import { OrdersFilter } from "./ordersFilter";
import { OrderCard } from "./orderCard";
import { modalsStore } from "../../../infrastructure/stores/modalsStore";
import { modalsTypes } from "../../layout/modals/modalsTypes";

const defaultFilter: OrdersFilterData = {
    orderStatuses: [
        OrderStatus.Pending,
        OrderStatus.Postponed,
        OrderStatus.Approved,
    ],
};

const defaultSorting: Sortable = {
    sortBy: "CreatedAt",
    direction: SortOrder.Descending,
};

const defaultPaging: Pageable = {
    pageNumber: 1,
    pageSize: Constants.paging.defaultPageSize,
};

const getInitialFilter = () => defaultFilter;

const onShowOrderDetails = (orderId: string) => {
    modalsStore.openModal({
        modalType: modalsTypes.orderDetailsModal,
        modalProps: {
            orderId: orderId,
        },
        size: "xl",
    });
};

export const OrdersPage = () => {
    const [orders, setOrders] = useState<ListResult<OrderListItem>>();

    async function loadOrders(request: FilterData<{}>) {
        setOrders(await searchOrders(request));
    }

    const listHandler = useMemo(() =>
        new ListRequestHandler<OrdersFilterData>(
            getInitialFilter(), defaultPaging, defaultSorting, loadOrders), []);

    useAsyncEffect(async () => {
        await loadOrders(listHandler.getRequest());
    }, []);

    const pagesCount = calculatePageNumber(orders?.totalCount);

    return (
        <div className="catalog-page">
            <Row>
                <Col xs={4} >
                    <h1 className="ui-page-header pt-2"><Local id="OrdersPage_Title" /></h1>
                </Col>
            </Row>
            <OrdersFilter onApply={listHandler.applyFilter}
                defaultFilter={defaultFilter} filterFromQuery={listHandler.getRequest()} />
            <div>
                <div className="d-flex align-items-center">
                    <div className="mr-auto"><Local id="Found" />: <b>{orders?.totalCount}</b></div>
                    <Sorting
                        default={defaultSorting}
                        sortKeys={["CreatedAt", "OrdersFilter_TotalAmount"]}
                        onChange={listHandler.applyListOptions} />
                </div>
                <Row className="p-3">
                    {orders?.totalCount > 0
                        ? orders.items.map((order, ind) =>
                            <OrderCard
                                key={ind}
                                ind={ind}
                                order={order}
                                onShowOrderDetails={() => onShowOrderDetails(order.id)} />)
                        : <NoData />}
                </Row>
            </div>
            {pagesCount > 1 &&
                <div className="mt-3 justify-content-center">
                    <PaginationControl
                        totalPages={pagesCount}
                        onChange={x => listHandler.applyListOptions({ pageNumber: x })}
                        extPageHandler={listHandler.pageHandler} />
                </div>}
        </div>
    );
};