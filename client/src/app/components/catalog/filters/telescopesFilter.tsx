import React, { useState, useEffect } from "react";
import { FormRenderProps, Form as FinalForm } from "react-final-form";
import { useLocation } from "react-router-dom";
import { filterDataStore } from "../../../infrastructure/stores/filterDataStore";
import { getNameOfFunction } from "../../../utils/namingUtils";
import { FilterCard } from "../../common/controls/filter/filterCard";
import { FilterSection } from "../../common/controls/filter/filterSection";
import { TelescopesFilterData } from "../../../dataModels/catalog/telescopesFilterData";
import { CheckboxFormControl } from "../../common/controls/formControls/checkboxFormControl";
import { TelescopeType } from "../../../dataModels/enums/telescope/telescopeType";
import { filterValues } from "../../../utils/dataTransformUtils";
import { brands } from "../../../dataModels/brands";
import { TelescopeUserLevel } from "../../../dataModels/enums/telescope/telescopeUserLevel";
import { TelescopeControlType } from "../../../dataModels/enums/telescope/telescopeControlType";
import { MountingType } from "../../../dataModels/enums/telescope/mountingType";
import { TextFormControl } from "../../common/controls/formControls/textFormControl";

interface Props {
    onApply: (model: TelescopesFilterData) => void;
    defaultFilter: TelescopesFilterData;
    filterFromQuery: TelescopesFilterData;
}

interface FormSpecificFilterData {
    isTelescopeRefractorType?: boolean;
    isTelescopeReflectorType?: boolean;
    isTelescopeMirrorLensType?: boolean;

    isLevenhukBrand?: boolean;
    isBresserBrand?: boolean;
    isSkyWatcherBrand?: boolean;

    isForChildren?: boolean;
    isForBeginners?: boolean;
    isForConfidentUsers?: boolean;
    isForProfessionals?: boolean;

    isManualControlType?: boolean;
    isAutoguidanceControlType?: boolean;

    isEquatorialMounting?: boolean;
    isAzimutalMounting?: boolean;
    isDobsonMounting?: boolean;
    isEquatorialEQ1Mounting?: boolean;
}

export interface TelescopesFilterDataFilterFormData extends TelescopesFilterData {
    formSpecifics: FormSpecificFilterData;
}

const telescopeTypes = {
    refractor: "Refractor",
    reflector: "Reflector",
    mirrorLens: "MirrorLens",
};

const telescopeBrands = {
    levenhuk: "Levenhuk",
    bresser: "Bresser",
    skyWatcher: "SkyWatcher",
};

const telescopeUserLevels = {
    forChldren: "ForChildren",
    forBeginners: "ForBeginners",
    forConfidentUsers: "ForConfidentUsers",
    forProfessionals: "ForProfessionals",
};

const mountingTypes = {
    equatorial: "Equatorial",
    azimutal: "Azimutal",
    dobson: "Dobson",
    equatorialEQ1: "EquatorialEQ1",
};

const telescopesControlTypes = {
    manual: "Manual",
    autoguidance: "Autoguidance",
};

const getFilterDataPropertyName = getNameOfFunction<TelescopesFilterDataFilterFormData>();

const convertFilterToFormData = (filterData: TelescopesFilterData): TelescopesFilterDataFilterFormData => {
    return {
        formSpecifics: {
            isTelescopeRefractorType: filterData?.telescopeTypes.some(t => t === TelescopeType.Refractor),
            isTelescopeReflectorType: filterData?.telescopeTypes.some(t => t === TelescopeType.Reflector),
            isTelescopeMirrorLensType: filterData?.telescopeTypes.some(t => t === TelescopeType.MirrorLens),

            isLevenhukBrand: filterData?.brandsIds?.some(id => id === brands.Levenhuk),
            isBresserBrand: filterData?.brandsIds?.some(id => id === brands.Bresser),
            isSkyWatcherBrand: filterData?.brandsIds?.some(id => id === brands.SkyWatcher),

            isForChildren: filterData?.userLevels?.some(t => t === TelescopeUserLevel.ForChildren),
            isForBeginners: filterData?.userLevels?.some(t => t === TelescopeUserLevel.ForBeginners),
            isForConfidentUsers: filterData?.userLevels?.some(t => t === TelescopeUserLevel.ForConfidentUsers),
            isForProfessionals: filterData?.userLevels?.some(t => t === TelescopeUserLevel.ForProfessionals),

            isManualControlType: filterData?.telescopeControlTypes?.some(t => t === TelescopeControlType.Manual),
            isAutoguidanceControlType: filterData?.telescopeControlTypes?.some(t => t === TelescopeControlType.Autoguidance),

            isEquatorialMounting: filterData?.mountingTypes?.some(t => t === MountingType.Equatorial),
            isAzimutalMounting: filterData?.mountingTypes?.some(t => t === MountingType.Azimutal),
            isDobsonMounting: filterData?.mountingTypes?.some(t => t === MountingType.Dobson),
            isEquatorialEQ1Mounting: filterData?.mountingTypes?.some(t => t === MountingType.EquatorialEQ1),
        },
        ...filterData,
    };
};

const convertFormToFilterData = (formData: TelescopesFilterDataFilterFormData): TelescopesFilterData => {
    const { formSpecifics, ...restFilterData } = formData;

    return {
        ...restFilterData,
        telescopeTypes: filterValues([
            [formSpecifics.isTelescopeRefractorType, TelescopeType.Refractor],
            [formSpecifics.isTelescopeReflectorType, TelescopeType.Reflector],
            [formSpecifics.isTelescopeMirrorLensType, TelescopeType.MirrorLens],
        ]),
        brandsIds: filterValues([
            [formSpecifics.isLevenhukBrand, brands.Levenhuk],
            [formSpecifics.isBresserBrand, brands.Bresser],
            [formSpecifics.isSkyWatcherBrand, brands.SkyWatcher],
        ]),
        userLevels: filterValues([
            [formSpecifics.isForChildren, TelescopeUserLevel.ForChildren],
            [formSpecifics.isForBeginners, TelescopeUserLevel.ForBeginners],
            [formSpecifics.isForConfidentUsers, TelescopeUserLevel.ForConfidentUsers],
            [formSpecifics.isForProfessionals, TelescopeUserLevel.ForProfessionals],
        ]),
        telescopeControlTypes: filterValues([
            [formSpecifics.isManualControlType, TelescopeControlType.Manual],
            [formSpecifics.isAutoguidanceControlType, TelescopeControlType.Autoguidance],
        ]),
        mountingTypes: filterValues([
            [formSpecifics.isEquatorialMounting, MountingType.Equatorial],
            [formSpecifics.isAzimutalMounting, MountingType.Azimutal],
            [formSpecifics.isDobsonMounting, MountingType.Dobson],
            [formSpecifics.isEquatorialEQ1Mounting, MountingType.EquatorialEQ1],
        ]),
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
        render={({ values, invalid, ...renderProps }: FormRenderProps<TelescopesFilterDataFilterFormData>) => {
            const onTelescopeTypeChange = () => !fullView && renderProps.handleSubmit();
            const primarySearchContent = <>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isTelescopeRefractorType"
                        onChange={onTelescopeTypeChange}
                        label={telescopeTypes.refractor}
                        className="d-md-inline-block mr-3"
                    />
                </div>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isTelescopeReflectorType"
                        onChange={onTelescopeTypeChange}
                        label={telescopeTypes.reflector}
                        className="d-md-inline-block mr-3"
                    />
                </div>
                <div className="group-items">
                    <CheckboxFormControl
                        name="formSpecifics.isTelescopeMirrorLensType"
                        onChange={onTelescopeTypeChange}
                        label={telescopeTypes.mirrorLens}
                        className="d-md-inline-block mr-3"
                    />
                </div>
            </>;

            const advancedSearchContent = <>
                <FilterSection headerKey="ByPrice">
                    <div className="group-items">
                        <TextFormControl name="priceMin" />
                        <span>-</span>
                        <TextFormControl name="priceMax" />
                    </div>
                </FilterSection>
                <FilterSection headerKey="Brand">
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isLevenhukBrand"
                            label={telescopeBrands.levenhuk}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isBresserBrand"
                            label={telescopeBrands.bresser}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isSkyWatcherBrand"
                            label={telescopeBrands.skyWatcher}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                </FilterSection>
                <FilterSection headerKey="ByUserLevel">
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isForChildren"
                            label={telescopeUserLevels.forChldren}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isForBeginners"
                            label={telescopeUserLevels.forBeginners}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isForConfidentUsers"
                            label={telescopeUserLevels.forConfidentUsers}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isForProfessionals"
                            label={telescopeUserLevels.forProfessionals}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                </FilterSection>
                <FilterSection headerKey="MountingType">
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isEquatorial"
                            label={mountingTypes.equatorial}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isAzimutal"
                            label={mountingTypes.azimutal}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isDobson"
                            label={mountingTypes.dobson}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isEquatorialEQ1"
                            label={mountingTypes.equatorialEQ1}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                </FilterSection>
                <FilterSection headerKey="ByTelescopeControlType">
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isManualControlType"
                            label={telescopesControlTypes.manual}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
                    <div className="group-items">
                        <CheckboxFormControl
                            name="formSpecifics.isAutoguidanceControlType"
                            label={telescopesControlTypes.autoguidance}
                            className="d-md-inline-block mr-3"
                        />
                    </div>
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