/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable react-hooks/rules-of-hooks */
import { isEmpty } from "lodash";
import { observer } from "mobx-react-lite";
import React, { useState, useEffect } from "react";
import { FormRenderProps, Form as FinalForm } from "react-final-form";
import { useLocation } from "react-router-dom";
import { Col, Row, Label, Card } from "reactstrap";
import { filterDataStore } from "../../infrastructure/stores/filterDataStore";
import { DateFormControl } from "../common/controls/formControls/dateFormControl";
import { Local } from "../localization/local";
import { StatisticsQuery } from "./staffProfile";
import { localizer } from "../localization/localizer";
import { RichSelectFormControl } from "../common/controls/formControls/richSelectFormControl";
import { LabeledValue } from "../common/controls/labeledValue";
import { getManagersLookupItems } from "../../api/accountProfile/accountProfileApi";
import { isManager, isStaff } from "../../infrastructure/services/auth/authService";
import { contextStore } from "../../infrastructure/stores/contextStore";

export interface StatisticsFilterFormData {
    managerAssignmentId?: string
    createdOnFrom?: Date;
    createdOnTo?: Date;
}

export interface ManagersLookupItemsRequest {
    searchValue?: string;
    selectedId?: string;
}

interface Props {
    onChange: (query: StatisticsQuery) => void;
}

const getPreviousMonthDate = (date: Date) => {
    date.setMonth(date.getMonth() - 1);

    return date;
};

export const getManagersSelectItems = async (request: ManagersLookupItemsRequest): Promise<LabeledValue[]> => {
    const result = await getManagersLookupItems(request);
    return result.map(r => ({ value: r.id, label: r.name }));
};

export const StatisticsFilter = observer((props: Props) => {
    const [formState, setFormState] = useState<StatisticsFilterFormData>();

    const location = useLocation();

    const defaultFilter: StatisticsFilterFormData = {
        managerAssignmentId: isManager() ? contextStore.assignmentId : null,
        createdOnFrom: getPreviousMonthDate(new Date()),
        createdOnTo: new Date(),
    };

    useEffect(() => {
        const storedFilter = filterDataStore.getFilterData<StatisticsFilterFormData>(location.key);
        setFormState(isEmpty(storedFilter) ? defaultFilter : storedFilter);
    }, []);

    const applyFilter = (filter: StatisticsFilterFormData) => {
        setFormState(filter);
        props.onChange(filter);
    };

    const onFilterDataChanged = (filter: StatisticsFilterFormData) => {
        filterDataStore.saveFilterData<StatisticsFilterFormData>(filter, location.key);
        applyFilter(filter);
    };

    const render = ({ values }: FormRenderProps<StatisticsFilterFormData>) => {
        useEffect(() => {
            onFilterDataChanged(values);
        }, [values]);

        const dateRangeFilters = () => {
            return (
                <Col>
                    <Row>
                        <Col>
                            <Row>
                                <Col className="float-left created-date-input">
                                    <Label>
                                        <Local id="from" />
                                    </Label>
                                    <DateFormControl
                                        name="createdOnFrom"
                                    />
                                </Col>
                                <Col className="float-left created-date-input">
                                    <Label>
                                        <Local id="to" />
                                    </Label>
                                    <DateFormControl
                                        name="createdOnTo"
                                    />
                                </Col>
                                {isStaff() &&
                                    <Col md={4} sm={12}>
                                        <RichSelectFormControl
                                            name="managerAssignmentId"
                                            label="Manager"
                                            optionsLoader={async (inputValue) =>
                                                await getManagersSelectItems({
                                                    searchValue: inputValue,
                                                    selectedId: values.managerAssignmentId,
                                                })
                                            }
                                            clearable
                                            placeholder={localizer.get("Name")}
                                            onChange={() => null}
                                            searchable
                                        />
                                    </Col>
                                }
                            </Row>
                        </Col>
                    </Row>
                </Col>
            );
        };

        return (
            <Card className="dashboard-filter ui-card p-3 mt-3 mb-3 filter-card">
                <Row>
                    <Col>
                        <Row>
                            {dateRangeFilters()}
                        </Row>
                    </Col>
                </Row>
            </Card>
        );
    };

    if (!formState) {
        return;
    }

    return (
        <FinalForm
            onSubmit={() => null}
            initialValues={formState}
            render={render}
        />
    );
});
