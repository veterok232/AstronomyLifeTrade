/* eslint-disable react-hooks/exhaustive-deps */
import React, { useState, useEffect } from "react";
import { FormRenderProps, Form as FinalForm } from "react-final-form";
import { useLocation } from "react-router-dom";
import { Row, Col } from "reactstrap";
import { brands } from "../../../dataModels/brands";
import { filterDataStore } from "../../../infrastructure/stores/filterDataStore";
import { filterValues } from "../../../utils/dataTransformUtils";
import { FilterCard } from "../../common/controls/filter/filterCard";
import { FilterSection } from "../../common/controls/filter/filterSection";
import { CheckboxFormControl } from "../../common/controls/formControls/checkboxFormControl";
import { TextFormControl } from "../../common/controls/formControls/textFormControl";
import { AccessoriesFilterData } from "../../../dataModels/catalog/accessoriesFilterData";
import { AccessoryType } from "../../../dataModels/enums/accessory/accessoryType";

interface Props {
    onApply: (model: AccessoriesFilterData) => void;
    defaultFilter: AccessoriesFilterData;
    filterFromQuery: AccessoriesFilterData;
}

interface FormSpecificFilterData {
    isAccessoryTypeBarlowLens?: boolean;
    isAccessoryTypeOcular?: boolean;
    isAccessoryTypeLightFilter?: boolean;
    isAccessoryTypeSolarFilter?: boolean;
    isAccessoryTypeMountingsAndTripods?: boolean;
    isAccessoryTypeForMountings?: boolean;
    isAccessoryTypeAutoguidance?: boolean;
    isAccessoryTypeBagsAndCases?: boolean;

    isLevenhukBrand?: boolean;
    isBresserBrand?: boolean;
    isSkywatcherBrand?: boolean;
}

export interface FilterFormData extends AccessoriesFilterData {
    formSpecifics: FormSpecificFilterData;
}

const accessoryTypes = {
    BarlowLens: "BarlowLens",
    Ocular: "Ocular",
    LightFilter: "LightFilter",
    SolarFilter: "SolarFilter",
    MountingsAndTripods: "MountingsAndTripods",
    ForMountings: "ForMountings",
    Autoguidance: "Autoguidance",
    BagsAndCases: "BagsAndCases",
};

const convertFilterToFormData = (filterData: AccessoriesFilterData): FilterFormData => {
    return {
        formSpecifics: {
            isAccessoryTypeBarlowLens: filterData?.accessoryTypes.some(b => b === AccessoryType.BarlowLens),
            isAccessoryTypeOcular: filterData?.accessoryTypes.some(b => b === AccessoryType.Ocular),
            isAccessoryTypeLightFilter: filterData?.accessoryTypes.some(b => b === AccessoryType.LightFilter),
            isAccessoryTypeSolarFilter: filterData?.accessoryTypes.some(b => b === AccessoryType.SolarFilter),
            isAccessoryTypeMountingsAndTripods: filterData?.accessoryTypes.some(b => b === AccessoryType.MountingsAndTripods),
            isAccessoryTypeForMountings: filterData?.accessoryTypes.some(b => b === AccessoryType.ForMountings),
            isAccessoryTypeAutoguidance: filterData?.accessoryTypes.some(b => b === AccessoryType.Autoguidance),
            isAccessoryTypeBagsAndCases: filterData?.accessoryTypes.some(b => b === AccessoryType.BagsAndCases),
        },
        ...filterData,
    };
};

const convertFormToFilterData = (formData: FilterFormData): AccessoriesFilterData => {
    const { formSpecifics, ...restFilterData } = formData;

    return {
        ...restFilterData,
        accessoryTypes: filterValues([
            [formSpecifics.isAccessoryTypeBarlowLens, AccessoryType.BarlowLens],
            [formSpecifics.isAccessoryTypeOcular, AccessoryType.Ocular],
            [formSpecifics.isAccessoryTypeLightFilter, AccessoryType.LightFilter],
            [formSpecifics.isAccessoryTypeSolarFilter, AccessoryType.SolarFilter],
            [formSpecifics.isAccessoryTypeMountingsAndTripods, AccessoryType.MountingsAndTripods],
            [formSpecifics.isAccessoryTypeForMountings, AccessoryType.ForMountings],
            [formSpecifics.isAccessoryTypeAutoguidance, AccessoryType.Autoguidance],
            [formSpecifics.isAccessoryTypeBagsAndCases, AccessoryType.BagsAndCases],
        ]),
        brandsIds: filterValues([
            [formSpecifics.isLevenhukBrand, brands.Levenhuk],
            [formSpecifics.isBresserBrand, brands.Bresser],
            [formSpecifics.isSkywatcherBrand, brands.SkyWatcher],
        ]),
    };
};

export const AccessoriesFilter = (props: Props) => {
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
            const onAccessoryTypeChange = () => !fullView && renderProps.handleSubmit();
            const primarySearchContent = <>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isAccessoryTypeBarlowLens"
                        onChange={onAccessoryTypeChange}
                        label={accessoryTypes.BarlowLens}
                        className="d-md-inline-block mr-3"
                    />
                </div>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isAccessoryTypeOcular"
                        onChange={onAccessoryTypeChange}
                        label={accessoryTypes.Ocular}
                        className="d-md-inline-block mr-3"
                    />
                </div>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isAccessoryTypeLightFilter"
                        onChange={onAccessoryTypeChange}
                        label={accessoryTypes.LightFilter}
                        className="d-md-inline-block mr-3"
                    />
                </div>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isAccessoryTypeSolarFilter"
                        onChange={onAccessoryTypeChange}
                        label={accessoryTypes.SolarFilter}
                        className="d-md-inline-block mr-3"
                    />
                </div>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isAccessoryTypeMountingsAndTripods"
                        onChange={onAccessoryTypeChange}
                        label={accessoryTypes.MountingsAndTripods}
                        className="d-md-inline-block mr-3"
                    />
                </div>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isAccessoryTypeForMountings"
                        onChange={onAccessoryTypeChange}
                        label={accessoryTypes.ForMountings}
                        className="d-md-inline-block mr-3"
                    />
                </div>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isAccessoryTypeAutoguidance"
                        onChange={onAccessoryTypeChange}
                        label={accessoryTypes.Autoguidance}
                        className="d-md-inline-block mr-3"
                    />
                </div>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isAccessoryTypeBagsAndCases"
                        onChange={onAccessoryTypeChange}
                        label={accessoryTypes.BagsAndCases}
                        className="d-md-inline-block mr-3"
                    />
                </div>
            </>;

            const advancedSearchContent = <>
                <FilterSection headerKey="ByPrice">
                    <Row className="group-items">
                        <Col className="col-5 d-flex align-items-center">
                            <TextFormControl name="priceMin" className="filter-price" />
                        </Col>
                        <Col className="col-1 d-flex align-items-center">
                            <span> — </span>
                        </Col>
                        <Col className="d-flex align-items-center">
                            <TextFormControl name="priceMax" className="filter-price" />
                        </Col>
                    </Row>
                </FilterSection>
                <FilterSection headerKey="ByBrand">
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isLevenhukBrand"
                            label={brands.Levenhuk}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isBresserBrand"
                            label={brands.Bresser}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isSkywatcherBrand"
                            label={brands.SkyWatcher}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                </FilterSection>
            </>;

            return (<FilterCard
                fullViewState={[fullView, setFullView]}
                primarySearchTitle="Filter_AccessoryType"
                primarySearchContent={primarySearchContent}
                advancedSearchContent={advancedSearchContent}
                handleSubmit={renderProps.handleSubmit}
                resetFilter={resetFilter}
                hasFormErrors={invalid} />);
        }} />
    );
};