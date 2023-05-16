import React, { useState, useEffect } from "react";
import { FormRenderProps, Form as FinalForm } from "react-final-form";
import { useLocation } from "react-router-dom";
import { filterDataStore } from "../../../infrastructure/stores/filterDataStore";
import { getNameOfFunction } from "../../../utils/namingUtils";
import { FilterCard } from "../../common/controls/filter/filterCard";
import { FilterSection } from "../../common/controls/filter/filterSection";
import { TelescopesFilterData } from "../../../dataModels/catalog/telescopesFilterData";

interface Props {
    onApply: (model: TelescopesFilterData) => void;
    defaultFilter: TelescopesFilterData;
    filterFromQuery: TelescopesFilterData;
}

interface FormSpecificFilterData {
}

export interface TelescopesFilterDataFilterFormData extends TelescopesFilterData {
    formSpecifics: FormSpecificFilterData;
}

const getFilterDataPropertyName = getNameOfFunction<TelescopesFilterDataFilterFormData>();

const convertFilterToFormData = (filterData: TelescopesFilterData): TelescopesFilterDataFilterFormData => {
    return {
        formSpecifics: {
        },
        ...filterData,
    };
};

const convertFormToFilterData = (formData: TelescopesFilterDataFilterFormData): TelescopesFilterData => {
    const { formSpecifics, ...restFilterData } = formData;

    return {
        ...restFilterData,
    };
};

const orderedMethods = [
];

export const TelescopesFilter = (props: Props) => {
    const [fullView, setFullView] = useState(false);
    const [formState, setFormState] = useState<TelescopesFilterDataFilterFormData>(
        convertFilterToFormData(props.filterFromQuery || props.defaultFilter));

    const location = useLocation();

    useEffect(() => {
        const recurringPaymentRequestsFilter = filterDataStore
            .getFilterData<TelescopesFilterDataFilterFormData>(location.key);

        if (recurringPaymentRequestsFilter) {
            applyFilter(recurringPaymentRequestsFilter);
        }
    }, []);

    const applyFilter = (value: TelescopesFilterDataFilterFormData) => {
        setFormState(value);
        props.onApply(convertFormToFilterData(value));
    };

    const onSubmitFilter = (value: TelescopesFilterDataFilterFormData) => {
        filterDataStore.saveFilterData<TelescopesFilterDataFilterFormData>(value, location.key);
        applyFilter(value);
    };

    const resetFilter = () => {
        const defaultData = convertFilterToFormData(props.defaultFilter);
        onSubmitFilter(defaultData);
    };

    return (<FinalForm
        onSubmit={onSubmitFilter}
        initialValues={formState}
        render={({ handleSubmit, invalid, values, errors }: FormRenderProps<TelescopesFilterDataFilterFormData>) => {
            const primarySearchContent = <>
            </>;

            const advancedSearchContent = <>
                <FilterSection headerKey="PaymentStates">
                </FilterSection>
            </>;

            return (<FilterCard
                fullViewState={[fullView, setFullView]}
                primarySearchTitle="Status"
                primarySearchContent={primarySearchContent}
                advancedSearchContent={advancedSearchContent}
                handleSubmit={handleSubmit}
                resetFilter={resetFilter}
                hasFormErrors={invalid} />);
        }} />
    );
};