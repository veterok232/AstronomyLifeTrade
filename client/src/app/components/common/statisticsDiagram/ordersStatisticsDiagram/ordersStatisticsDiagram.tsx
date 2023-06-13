import React, { useState } from "react";
import { OrderStatus } from "../../../../dataModels/enums/orderStatus";
import { getEnumLocalizationKey } from "../../../localization/enumRegistrator";
import { localizer } from "../../../localization/localizer";
import { OrderStatusColors } from "../../../orders/orders/orderStatusColors";
import { CollapseToggle } from "../../controls/collapse/collapseToggle";
import { StatisticsDiagram } from "../statisticsDiagram";
import { OrdersStatisticsDiagramLegendItem } from "./ordersStatisticsDiagramLegendItem";

export interface OrdersAggregatedData {
    aggregatedByEntityId: string;
    items: OrdersAggregatedDataItem[];
}

export interface OrdersAggregatedDataItem {
    status: OrderStatus;
    count: number;
    amount: number;
}

const mainStatisticsStatusesInOrder = [
    OrderStatus.Pending,
    OrderStatus.Postponed,
    OrderStatus.Cancelled,
    OrderStatus.Approved,
    OrderStatus.Closed];

const colors = mainStatisticsStatusesInOrder.map((s) => OrderStatusColors[s]);

const getLegendItems = (
    orderStatuses: Array<OrderStatus>,
    data: Array<OrdersAggregatedDataItem>,
    searchLinkCommonPart: string
) =>
    orderStatuses.map((status, key) => (
        <OrdersStatisticsDiagramLegendItem
            dataItem={data?.find((i) => i.status === status)}
            color={OrderStatusColors[status]}
            orderStatus={status}
            searchLinkCommonPart={searchLinkCommonPart}
            key={key}
        />
    ));

interface Props {
    data: Array<OrdersAggregatedDataItem>;
    diagramSize: number;
    searchLinkCommonPart: string;
    collapsableLegend?: boolean;
}

export function OrdersStatisticsDiagram(props: Props) {
    const [fullLegendView, setFullLegendView] = useState(false);

    const data: Array<[string, number]> = mainStatisticsStatusesInOrder.map((status) => [
        localizer.get(getEnumLocalizationKey(OrderStatus, status)),
        props.data?.find((i) => i.status === status)?.count ?? 0,
    ]);

    const legend = (
        <>
            {getLegendItems(mainStatisticsStatusesInOrder, props.data, props.searchLinkCommonPart)}
            {props.collapsableLegend && (
                <CollapseToggle
                    className="d-none d-md-block"
                    openedLabelKey="Hide"
                    closedLabelKey="AllSatuses"
                    state={[fullLegendView, setFullLegendView]}
                />
            )}
        </>
    );

    return (
        <StatisticsDiagram
            data={data}
            colors={colors}
            diagramSize={props.diagramSize}
            legend={legend}
            emptyDataKey="NoOrders"
            legendTitleKey="Orders"
        />
    );
}
