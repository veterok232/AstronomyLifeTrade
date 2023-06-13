import React from "react";
import { NavLink } from "react-router-dom";
import { Row, Col } from "reactstrap";
import { OrderStatus } from "../../../../dataModels/enums/orderStatus";
import { getEnumLocalizationKey } from "../../../localization/enumRegistrator";
import { Local } from "../../../localization/local";
import { localizer } from "../../../localization/localizer";
import { OrdersAggregatedDataItem } from "./ordersStatisticsDiagram";

interface CountBlockProps {
    status: OrderStatus;
    count: number;
}

const CountBlock = (props: CountBlockProps) => (
    <>
        <Local id={getEnumLocalizationKey(OrderStatus, props.status)} />
        <span className={`legend-count ${props.count ? "active" : "inactive"}`}>{props.count}</span>
    </>
);

interface Props {
    dataItem: OrdersAggregatedDataItem;
    color: string;
    orderStatus: OrderStatus;
    searchLinkCommonPart: string;
}

export function OrdersStatisticsDiagramLegendItem(props: Props) {
    return (
        <Row className="legend-item flex-nowrap">
            <Col md={6} className="d-flex align-items-center my-0 pr-0">
                <span
                    className="legend-color-circle d-inline-block align-middle"
                    style={{ backgroundColor: props.color }}
                ></span>
                {props.dataItem?.count ? (
                    <NavLink
                        target="_blank"
                        to={`${props.searchLinkCommonPart}?orderStatus=${props.orderStatus}`}
                    >
                        <CountBlock status={props.orderStatus} count={props.dataItem.count} />
                    </NavLink>
                ) : (
                    <CountBlock status={props.orderStatus} count={0} />
                )}
            </Col>
            <Col className="my-0">
                <span className="d-block legend-amount text-center text-nowrap">
                    {`${localizer.formatMoney(props.dataItem?.amount ?? 0, true)} руб.`}
                </span>
            </Col>
        </Row>
    );
}
