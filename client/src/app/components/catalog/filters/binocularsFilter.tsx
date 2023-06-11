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
import { BinocularFilterData } from "../../../dataModels/catalog/binocularFilterData";
import { BinocularPurpose } from "../../../dataModels/enums/binocuclar/binocularPurpose";
import { FocusingMethod } from "../../../dataModels/enums/binocuclar/focusingMethod";
import { OpticsMaterial } from "../../../dataModels/enums/binocuclar/opticsMaterial";

interface Props {
    onApply: (model: BinocularFilterData) => void;
    defaultFilter: BinocularFilterData;
    filterFromQuery: BinocularFilterData;
}

interface FormSpecificFilterData {
    isBinocularPurposeArmy?: boolean;
    isBinocularPurposeAstronomical?: boolean;
    isBinocularPurposeChildren?: boolean;
    isBinocularPurposeHuntingAndFishing?: boolean;
    isBinocularPurposeSport?: boolean;
    isBinocularPurposeNavy?: boolean;

    isBinocularLevenhukBrand?: boolean;
    isBinocularBresserBrand?: boolean;

    isBinocularFocusingMethodCentral?: boolean;
    isBinocularFocusingMethodDifferential?: boolean;

    isBinocularOpticsMaterialBak4?: boolean;
    isBinocularOpticsMaterialBak7?: boolean;
    isBinocularOpticsMaterialBak10?: boolean;
    isBinocularOpticsMaterialEcoGlass?: boolean;
    isBinocularOpticsMaterialED?: boolean;
    isBinocularOpticsMaterialK9?: boolean;
    isBinocularOpticsMaterialOpticalPlastic?: boolean;
    isBinocularOpticsMaterialOpticalGlass?: boolean;
}

export interface FilterFormData extends BinocularFilterData {
    formSpecifics: FormSpecificFilterData;
}

const opticsMaterials = {
    Bak4: "Bak4",
    Bak7: "Bak7",
    Bak10: "Bak10",
    EcoGlass: "EcoGlass",
    ED: "ED",
    K9: "K9",
    OpticalPlastic: "OpticalPlastic",
    OpticalGlass: "OpticalGlass",
};

const binocularBrands = {
    levenhuk: "Levenhuk",
    bresser: "Bresser",
};

const focusingMethods = {
    central: "Central",
    differential: "Differential",
};

const binocularPurposes = {
    army: "Army",
    astronomical: "Astronomical",
    children: "Children",
    huntingAndFishing: "HuntingAndFishing",
    sport: "Sport",
    navy: "Navy",
};

const convertFilterToFormData = (filterData: BinocularFilterData): FilterFormData => {
    return {
        formSpecifics: {
            isBinocularPurposeArmy: filterData?.binocularPurposes.some(b => b === BinocularPurpose.Army),
            isBinocularPurposeAstronomical: filterData?.binocularPurposes.some(b => b === BinocularPurpose.Astronomical),
            isBinocularPurposeChildren: filterData?.binocularPurposes.some(b => b === BinocularPurpose.Children),
            isBinocularPurposeHuntingAndFishing: filterData?.binocularPurposes.some(b => b === BinocularPurpose.HuntingAndFishing),
            isBinocularPurposeSport: filterData?.binocularPurposes.some(b => b === BinocularPurpose.Sport),
            isBinocularPurposeNavy: filterData?.binocularPurposes.some(b => b === BinocularPurpose.Navy),

            isBinocularLevenhukBrand: filterData?.brandsIds?.some(id => id === brands.Levenhuk),
            isBinocularBresserBrand: filterData?.brandsIds?.some(id => id === brands.Bresser),

            isBinocularFocusingMethodCentral: filterData?.focusingMethods?.some(b => b === FocusingMethod.Central),
            isBinocularFocusingMethodDifferential: filterData?.focusingMethods?.some(b => b === FocusingMethod.Differential),

            isBinocularOpticsMaterialBak4: filterData?.opticsMaterials?.some(t => t === OpticsMaterial.Bak4),
            isBinocularOpticsMaterialBak7: filterData?.opticsMaterials?.some(t => t === OpticsMaterial.Bak7),
            isBinocularOpticsMaterialBak10: filterData?.opticsMaterials?.some(t => t === OpticsMaterial.Bak10),
            isBinocularOpticsMaterialEcoGlass: filterData?.opticsMaterials?.some(t => t === OpticsMaterial.EcoGlass),
            isBinocularOpticsMaterialED: filterData?.opticsMaterials?.some(t => t === OpticsMaterial.ED),
            isBinocularOpticsMaterialK9: filterData?.opticsMaterials?.some(t => t === OpticsMaterial.K9),
            isBinocularOpticsMaterialOpticalPlastic: filterData?.opticsMaterials?.some(t => t === OpticsMaterial.OpticalPlastic),
            isBinocularOpticsMaterialOpticalGlass: filterData?.opticsMaterials?.some(t => t === OpticsMaterial.OpticalGlass),
        },
        ...filterData,
    };
};

const convertFormToFilterData = (formData: FilterFormData): BinocularFilterData => {
    const { formSpecifics, ...restFilterData } = formData;

    return {
        ...restFilterData,
        binocularPurposes: filterValues([
            [formSpecifics.isBinocularPurposeArmy, BinocularPurpose.Army],
            [formSpecifics.isBinocularPurposeAstronomical, BinocularPurpose.Astronomical],
            [formSpecifics.isBinocularPurposeChildren, BinocularPurpose.Children],
            [formSpecifics.isBinocularPurposeHuntingAndFishing, BinocularPurpose.HuntingAndFishing],
            [formSpecifics.isBinocularPurposeSport, BinocularPurpose.Sport],
            [formSpecifics.isBinocularPurposeNavy, BinocularPurpose.Navy],
        ]),
        brandsIds: filterValues([
            [formSpecifics.isBinocularLevenhukBrand, brands.Levenhuk],
            [formSpecifics.isBinocularBresserBrand, brands.Bresser],
        ]),
        focusingMethods: filterValues([
            [formSpecifics.isBinocularFocusingMethodCentral, FocusingMethod.Central],
            [formSpecifics.isBinocularFocusingMethodDifferential, FocusingMethod.Differential],
        ]),
        opticsMaterials: filterValues([
            [formSpecifics.isBinocularOpticsMaterialBak4, OpticsMaterial.Bak4],
            [formSpecifics.isBinocularOpticsMaterialBak7, OpticsMaterial.Bak7],
            [formSpecifics.isBinocularOpticsMaterialBak10, OpticsMaterial.Bak10],
            [formSpecifics.isBinocularOpticsMaterialEcoGlass, OpticsMaterial.EcoGlass],
            [formSpecifics.isBinocularOpticsMaterialED, OpticsMaterial.ED],
            [formSpecifics.isBinocularOpticsMaterialK9, OpticsMaterial.K9],
            [formSpecifics.isBinocularOpticsMaterialOpticalPlastic, OpticsMaterial.OpticalPlastic],
            [formSpecifics.isBinocularOpticsMaterialOpticalGlass, OpticsMaterial.OpticalGlass],
        ]),
    };
};

export const BinocularsFilter = (props: Props) => {
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
            const onBinocularPurposeChange = () => !fullView && renderProps.handleSubmit();
            const primarySearchContent = <>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isBinocularPurposeArmy"
                        onChange={onBinocularPurposeChange}
                        label={binocularPurposes.army}
                        className="d-md-inline-block mr-3"
                    />
                </div>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isBinocularPurposeAstronomical"
                        onChange={onBinocularPurposeChange}
                        label={binocularPurposes.astronomical}
                        className="d-md-inline-block mr-3"
                    />
                </div>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isBinocularPurposeChildren"
                        onChange={onBinocularPurposeChange}
                        label={binocularPurposes.children}
                        className="d-md-inline-block mr-3"
                    />
                </div>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isBinocularPurposeHuntingAndFishing"
                        onChange={onBinocularPurposeChange}
                        label={binocularPurposes.huntingAndFishing}
                        className="d-md-inline-block mr-3"
                    />
                </div>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isBinocularPurposeSport"
                        onChange={onBinocularPurposeChange}
                        label={binocularPurposes.sport}
                        className="d-md-inline-block mr-3"
                    />
                </div>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isBinocularPurposeNavy"
                        onChange={onBinocularPurposeChange}
                        label={binocularPurposes.navy}
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
                            name="formSpecifics.isBinocularLevenhukBrand"
                            label={binocularBrands.levenhuk}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isBinocularBresserBrand"
                            label={binocularBrands.bresser}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                </FilterSection>
                <FilterSection headerKey="ByFocusingMethod">
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isBinocularFocusingMethodCentral"
                            label={focusingMethods.central}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isBinocularFocusingMethodDifferential"
                            label={focusingMethods.differential}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                </FilterSection>
                <FilterSection headerKey="ByOpticsMaterial">
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isBinocularOpticsMaterialBak4"
                            label={opticsMaterials.Bak4}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isBinocularOpticsMaterialBak7"
                            label={opticsMaterials.Bak7}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isBinocularOpticsMaterialBak10"
                            label={opticsMaterials.Bak10}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isBinocularOpticsMaterialEcoGlass"
                            label={opticsMaterials.EcoGlass}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isBinocularOpticsMaterialED"
                            label={opticsMaterials.ED}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isBinocularOpticsMaterialK9"
                            label={opticsMaterials.K9}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isBinocularOpticsMaterialOpticalPlastic"
                            label={opticsMaterials.OpticalPlastic}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isBinocularOpticsMaterialOpticalGlass"
                            label={opticsMaterials.OpticalGlass}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                </FilterSection>
            </>;

            return (<FilterCard
                fullViewState={[fullView, setFullView]}
                primarySearchTitle="Filter_BinocularPurpose"
                primarySearchContent={primarySearchContent}
                advancedSearchContent={advancedSearchContent}
                handleSubmit={renderProps.handleSubmit}
                resetFilter={resetFilter}
                hasFormErrors={invalid} />);
        }} />
    );
};