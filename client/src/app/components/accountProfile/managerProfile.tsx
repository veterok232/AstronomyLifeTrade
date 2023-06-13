import React, { useState } from "react";
import { Row, Col, Button } from "reactstrap";
import { Local } from "../localization/local";
import { logOut } from "../../infrastructure/services/identityService";
import { sharedHistory } from "../../infrastructure/sharedHistory";
import { getRoute } from "../../utils/routeUtils";
import { routeLinks } from "../layout/routes/routeLinks";
import { StatisticsFilter } from "./statisticsFilter";
import { OrdersAggregatedDataItem, OrdersStatisticsDiagram } from "../common/statisticsDiagram/ordersStatisticsDiagram/ordersStatisticsDiagram";
import { StatisticsQuery } from "./staffProfile";
import useAsyncEffect from "use-async-effect";
import { getOrdersStatistics } from "../../api/accountProfile/accountProfileApi";

export const ManagerProfile = () => {
    const [statisticsQuery, setStatisticsQuery] = useState<StatisticsQuery>();
    const [statisticsData, setStatisticsData] = useState<Array<OrdersAggregatedDataItem>>();

    useAsyncEffect(async () => {
        if (statisticsQuery) {
            setStatisticsData(await getOrdersStatistics(statisticsQuery));
        }
    }, [statisticsQuery]);

    const onLogout = async () => {
        await logOut();
        sharedHistory.push(getRoute(routeLinks.catalog.root));
    };

    return (
        <>
            <Row className="mb-3">
                <Col xs={4} >
                    <h1 className="ui-page-header pt-2"><Local id="ManagerProfile_Title" /></h1>
                </Col>
                <Col>
                    <Button onClick={onLogout} className="float-right">
                        <Local id="Logout" />
                    </Button>
                </Col>
            </Row>
            <Row className="order-step-card p-3 mb-2">
                <Col className="px-0">
                    <Row className="mb-2">
                        <Col>
                            <h1 className="ui-section-header pt-2"><Local id="StatisticsByOrders" /></h1>
                        </Col>
                    </Row>
                    <StatisticsFilter onChange={setStatisticsQuery} />
                    <OrdersStatisticsDiagram
                        data={statisticsData}
                        searchLinkCommonPart={getRoute(routeLinks.orders.root)}
                        diagramSize={209}
                    />
                </Col>
            </Row>
        </>
    );
};