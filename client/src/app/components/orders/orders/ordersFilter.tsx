/* eslint-disable react-hooks/exhaustive-deps */
import React, { useState, useEffect } from "react";
import { FormRenderProps, Form as FinalForm } from "react-final-form";
import { useLocation } from "react-router-dom";
import { Row, Col } from "reactstrap";
import { filterDataStore } from "../../../infrastructure/stores/filterDataStore";
import { filterValues } from "../../../utils/dataTransformUtils";
import { FilterCard } from "../../common/controls/filter/filterCard";
import { FilterSection } from "../../common/controls/filter/filterSection";
import { CheckboxFormControl } from "../../common/controls/formControls/checkboxFormControl";
import { TextFormControl } from "../../common/controls/formControls/textFormControl";
import { OrdersFilterData } from "../../../dataModels/orders/ordersFilterData";
import { OrderStatus } from "../../../dataModels/enums/orderStatus";

interface Props {
    onApply: (model: OrdersFilterData) => void;
    defaultFilter: OrdersFilterData;
    filterFromQuery: OrdersFilterData;
}

interface FormSpecificFilterData {
    isOrderPendingStatus?: boolean;
    isOrderPostponedStatus?: boolean;
    isOrderCancelledStatus?: boolean;
    isOrderApprovedStatus?: boolean;
    isOrderClosedStatus?: boolean;
}

export interface FilterFormData extends OrdersFilterData {
    formSpecifics: FormSpecificFilterData;
}

const orderStatuses = {
    pending: "Pending",
    postponed: "Postponed",
    cancelled: "Cancelled",
    approved: "Approved",
    closed: "Closed",
};

const convertFilterToFormData = (filterData: OrdersFilterData): FilterFormData => {
    return {
        formSpecifics: {
            isOrderPendingStatus: filterData?.orderStatuses.some(t => t === OrderStatus.Pending),
            isOrderPostponedStatus: filterData?.orderStatuses.some(t => t === OrderStatus.Postponed),
            isOrderCancelledStatus: filterData?.orderStatuses.some(t => t === OrderStatus.Cancelled),
            isOrderApprovedStatus: filterData?.orderStatuses.some(t => t === OrderStatus.Approved),
            isOrderClosedStatus: filterData?.orderStatuses.some(t => t === OrderStatus.Closed),
        },
        ...filterData,
    };
};

const convertFormToFilterData = (formData: FilterFormData): OrdersFilterData => {
    const { formSpecifics, ...restFilterData } = formData;

    return {
        ...restFilterData,
        orderStatuses: filterValues([
            [formSpecifics.isOrderPendingStatus, OrderStatus.Pending],
            [formSpecifics.isOrderPostponedStatus, OrderStatus.Postponed],
            [formSpecifics.isOrderCancelledStatus, OrderStatus.Cancelled],
            [formSpecifics.isOrderApprovedStatus, OrderStatus.Approved],
            [formSpecifics.isOrderClosedStatus, OrderStatus.Closed],
        ]),
    };
};

export const OrdersFilter = (props: Props) => {
    const [fullView, setFullView] = useState(false);
    const [formState, setFormState] = useState<FilterFormData>(
        convertFilterToFormData(props.filterFromQuery || props.defaultFilter));

    const location = useLocation();

    useEffect(() => {
        const recurringPaymentRequestsFilter = filterDataStore
            .getFilterData<FilterFormData>(location.key);

        if (recurringPaymentRequestsFilter) {
            applyFilter(recurringPaymentRequestsFilter);
        }
    }, []);

    const applyFilter = (value: FilterFormData) => {
        setFormState(value);
        props.onApply(convertFormToFilterData(value));
    };

    const onSubmitFilter = (value: FilterFormData) => {
        filterDataStore.saveFilterData<FilterFormData>(value, location.key);
        applyFilter(value);
    };

    const resetFilter = () => {
        const defaultData = convertFilterToFormData(props.defaultFilter);
        onSubmitFilter(defaultData);
    };

    return (<FinalForm
        onSubmit={onSubmitFilter}
        initialValues={formState}
        render={({ invalid, ...renderProps }: FormRenderProps<FilterFormData>) => {
            const onOrderStatusChange = () => !fullView && renderProps.handleSubmit();
            const primarySearchContent = <>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isOrderPendingStatus"
                        onChange={onOrderStatusChange}
                        label={orderStatuses.pending}
                        className="d-md-inline-block mr-3"
                    />
                </div>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isOrderPostponedStatus"
                        onChange={onOrderStatusChange}
                        label={orderStatuses.postponed}
                        className="d-md-inline-block mr-3"
                    />
                </div>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isOrderCancelledStatus"
                        onChange={onOrderStatusChange}
                        label={orderStatuses.cancelled}
                        className="d-md-inline-block mr-3"
                    />
                </div>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isOrderApprovedStatus"
                        onChange={onOrderStatusChange}
                        label={orderStatuses.approved}
                        className="d-md-inline-block mr-3"
                    />
                </div>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isOrderClosedStatus"
                        onChange={onOrderStatusChange}
                        label={orderStatuses.closed}
                        className="d-md-inline-block mr-3"
                    />
                </div>
            </>;

            const advancedSearchContent = <>
                <FilterSection headerKey="">
                    <Row className="group-items">
                        <Col>
                            <TextFormControl label="OrderNumber" name="orderNumber" />
                        </Col>
                    </Row>
                </FilterSection>
            </>;

            return (<FilterCard
                fullViewState={[fullView, setFullView]}
                primarySearchTitle="Filter_TelescopeType"
                primarySearchContent={primarySearchContent}
                advancedSearchContent={advancedSearchContent}
                handleSubmit={renderProps.handleSubmit}
                resetFilter={resetFilter}
                hasFormErrors={invalid} />);
        }} />
    );
};