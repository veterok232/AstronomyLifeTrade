import React from "react";
import { Row, Col } from "reactstrap";
import { MountingType } from "../../../../dataModels/enums/telescope/mountingType";
import { TelescopeControlType } from "../../../../dataModels/enums/telescope/telescopeControlType";
import { TelescopeType } from "../../../../dataModels/enums/telescope/telescopeType";
import { TelescopeUserLevel } from "../../../../dataModels/enums/telescope/telescopeUserLevel";
import { DetailsCharacteristic } from "../../../common/presentation/detailsCharacteristic";
import { TextFormControl } from "../../../common/controls/formControls/textFormControl";
import { RichSelectFormControl } from "../../../common/controls/formControls/richSelectFormControl";
import { LabeledValue } from "../../../common/controls/labeledValue";

/* const getEyepieces = (eyepieces: TelescopeEyepiece[]): string => {
    if (isEmpty(eyepieces)) {
        return "Нет";
    }

    let result: string;

    for (let i = 0; i < eyepieces.length; i++) {
        result = result.concat(`${eyepieces[i].name} ${eyepieces[i].effectiveScale && "(" + eyepieces[i].effectiveScale + ")"}`);
    }

    return result;
}; */

const telescopeTypesOptions: LabeledValue[] = [{
    label: "Рефлектор",
    value: TelescopeType.Reflector,
}, {
    label: "Рефрактор",
    value: TelescopeType.Refractor,
}, {
    label: "Зеркально-линзовый",
    value: TelescopeType.MirrorLens,
}];

const userLevelsOptions: LabeledValue[] = [{
    label: "Для детей",
    value: TelescopeUserLevel.ForChildren,
}, {
    label: "Для начинающих",
    value: TelescopeUserLevel.ForBeginners,
}, {
    label: "Для уверенных пользователей",
    value: TelescopeUserLevel.ForConfidentUsers,
}, {
    label: "Для профессионалов",
    value: TelescopeUserLevel.ForProfessionals,
}];

const mountingTypeOptions: LabeledValue[] = [{
    label: "Экваториальная",
    value: MountingType.Equatorial,
}, {
    label: "Азимутальная",
    value: MountingType.Azimutal,
}, {
    label: "Добсона",
    value: MountingType.Dobson,
}, {
    label: "Экваториальная EQ1",
    value: MountingType.EquatorialEQ1,
}];

const controlTypesOptions: LabeledValue[] = [{
    label: "Ручное",
    value: TelescopeControlType.Manual,
}, {
    label: "С автонаведением",
    value: TelescopeControlType.Autoguidance,
}];

export const TelescopeCharacteristicsSection = () => {
    return (
    <Row className="order-step-card p-3 mb-2">
        <Col>
            <Row>
                <Col>
                    <DetailsCharacteristic className="mb-5" isEditMode name="Апертура (мм)" value={<TextFormControl name={"characteristics.aperture"} />} />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Светосила (относительное отверстие)" value={<TextFormControl name={"characteristics.apertureRatio"} />}  />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Посадочный диаметр окуляров (дюймов)" value={<TextFormControl name={"characteristics.eyepieceFittingDiameter"} />} />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Фокусное расстояние (мм)" value={<TextFormControl name={"characteristics.focusDistance"} />}  />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Максимальное полезное увеличение (крат.)" value={<TextFormControl name={"characteristics.maxUsefulScale"} />}  />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Минимальное полезное увеличение (крат.)" value={<TextFormControl name={"characteristics.minUsefulScale"} />}  />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Тип монтировки" value={<RichSelectFormControl className="characteristic-input" name={"characteristics.mountingType"} clearable options={mountingTypeOptions} />}  />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Тип управления телескопом" value={<RichSelectFormControl className="characteristic-input" name={"characteristics.telescopeControlType"} clearable options={controlTypesOptions} />} />
                    {/* <DetailsCharacteristic name="Окуляры" value={<TextFormControl name={"characteristics.telescopeEyepieces"} />} /> */}
                </Col>
                <Col>
                    <DetailsCharacteristic className="mb-5" isEditMode name="Увеличение максимальное (крат.)" value={<TextFormControl name={"characteristics.scaleMax"} />}  />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Увеличение минимальное (крат.)" value={<TextFormControl name={"characteristics.scaleMin"} />}  />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Искатель" value={<TextFormControl name={"characteristics.seeker"} />}  />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Высота штатива" value={<TextFormControl name={"characteristics.tripodHeight"} />} />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Материал штатива" value={<TextFormControl name={"characteristics.tripodMaterial"} />} />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Тип телескопа" value={<RichSelectFormControl className="characteristic-input" name={"characteristics.type"} clearable options={telescopeTypesOptions} />} />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Пользовательский уровень" value={<RichSelectFormControl className="characteristic-input" name={"characteristics.telescopeUserLevel"} clearable options={userLevelsOptions} />}  />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Вес" value={<TextFormControl name={"characteristics.weight"} />}  />
                </Col>
            </Row>
        </Col>
    </Row>);
};