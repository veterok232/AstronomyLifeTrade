/* eslint-disable react-hooks/exhaustive-deps */
import React, { useMemo } from "react";
import { Local } from "../../localization/local";
import { localizer } from "../../localization/localizer";
import PieChart from "react-google-charts";

const textColor = "white";
const emptyTextColor = "#7A7987";

const emptyColor = "#EFEFF5";

const pieChartOptions = {
    legend: "none",
    is3D: true,
    chartArea: {
        height: "90%",
        width: "90%",
    },
    pieSliceTextStyle: {
        fontSize: 14,
        fontName: "inherit",
    },
};

declare type Direction = "row" | "column";

interface Props {
    data: Array<[string, number]>;
    colors: Array<string>;
    diagramSize: number;
    emptyDataKey: string;
    legendTitleKey: string;
    legend: JSX.Element;
    direction?: Direction;
}

export function StatisticsDiagram(props: Props) {
    const emptyData = useMemo(() => [[localizer.get(props.emptyDataKey), 1]], []);

    const total = props.data?.reduce((t, item) => t + item[1], 0) ?? 0;

    return (
        <div
            className={`statistics-diagram align-items-center align-items-md-center d-flex flex-column flex-md-${
                props.direction || "row"
            }`}
        >
            <div className="diagram overflow-hidden" style={{ minWidth: `${props.diagramSize}px` }}>
                <PieChart
                    chartType="PieChart"
                    legendToggle
                    data={[
                        ["", ""],
                        ...(total ? props.data : emptyData),
                    ]}
                    options={{
                        ...pieChartOptions,
                        height: props.diagramSize,
                        tooltip: {
                            trigger: total ? "hover" : "none",
                        },
                        pieSliceText: total ? "percentage" : "label",
                        colors: total ? props.colors : [emptyColor],
                        pieSliceTextStyle: {
                            ...pieChartOptions.pieSliceTextStyle,
                            color: total ? textColor : emptyTextColor,
                        },
                    }}
                />
            </div>
            <div className="legend">
                <div className="d-block legend-header">
                    <b>
                        <Local id={props.legendTitleKey} />
                    </b>
                    <b className="legend-total-amount">{total}</b>
                </div>
                {props.legend}
            </div>
        </div>
    );
}
