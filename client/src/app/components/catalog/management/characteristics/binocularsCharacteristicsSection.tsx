import React from "react";
import { Row, Col } from "reactstrap";
import { RichSelectFormControl } from "../../../common/controls/formControls/richSelectFormControl";
import { TextFormControl } from "../../../common/controls/formControls/textFormControl";
import { LabeledValue } from "../../../common/controls/labeledValue";
import { DetailsCharacteristic } from "../../../common/presentation/detailsCharacteristic";
import { FocusingMethod } from "../../../../dataModels/enums/binocuclar/focusingMethod";
import { OpticsMaterial } from "../../../../dataModels/enums/binocuclar/opticsMaterial";
import { PrismType } from "../../../../dataModels/enums/binocuclar/prismType";
import { BinocularPurpose } from "../../../../dataModels/enums/binocuclar/binocularPurpose";

const focusingMethods: LabeledValue[] = [{
    label: "Центральный",
    value: FocusingMethod.Central,
}, {
    label: "Дифференциальный",
    value: FocusingMethod.Differential,
}];

const opticsMaterials: LabeledValue[] = [{
    label: "Стекло BaK-4",
    value: OpticsMaterial.Bak4,
}, {
    label: "Стекло BaK-7",
    value: OpticsMaterial.Bak7,
}, {
    label: "Стекло BaK-10",
    value: OpticsMaterial.Bak10,
}, {
    label: "Eco-Glass",
    value: OpticsMaterial.EcoGlass,
}, {
    label: "ED",
    value: OpticsMaterial.ED,
}, {
    label: "Стекло K9",
    value: OpticsMaterial.K9,
}, {
    label: "Оптический пластик",
    value: OpticsMaterial.OpticalPlastic,
}, {
    label: "Оптическое стекло",
    value: OpticsMaterial.OpticalGlass,
}];

const prismTypes: LabeledValue[] = [{
    label: "Abbe-Konig",
    value: PrismType.AbbeKonig,
}, {
    label: "Схема Галилея",
    value: PrismType.GalileyScheme,
}, {
    label: "Porro",
    value: PrismType.Porro,
}, {
    label: "Roof",
    value: PrismType.Roof,
}, {
    label: "Roof с фазовой коррекцией",
    value: PrismType.RoofWithPhaseCorrection,
}];

const binocularPurposes: LabeledValue[] = [{
    label: "Армейский",
    value: BinocularPurpose.Army,
}, {
    label: "Астрономический",
    value: BinocularPurpose.Astronomical,
}, {
    label: "Детский",
    value: BinocularPurpose.Children,
}, {
    label: "Для охоты и рыбалки",
    value: BinocularPurpose.HuntingAndFishing,
}, {
    label: "Морской",
    value: BinocularPurpose.Navy,
}, {
    label: "Спортивный",
    value: BinocularPurpose.Sport,
}];

export const BinocularsCharacteristicsSection = () => {
    return (
    <Row className="order-step-card p-3 mb-2">
        <Col>
            <Row>
                <Col>
                    <DetailsCharacteristic className="mb-5" isEditMode name="Апертура (мм)" value={<TextFormControl name={"characteristics.aperture"} />} />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Диаметр выходного зрачка макс. (мм)" value={<TextFormControl name={"characteristics.exitPupilDiameterMax"} />}  />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Диаметр выходного зрачка мин. (мм)" value={<TextFormControl name={"characteristics.exitPupilDiameterMin"} />} />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Способ фокусировки" value={<RichSelectFormControl className="characteristic-input" name={"characteristics.focusingMethod"} clearable options={focusingMethods} />}  />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Поле зрения макс. (градусов)" value={<TextFormControl name={"characteristics.fovMin"} />}  />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Поле зрения мин. (градусов)" value={<TextFormControl name={"characteristics.fovMax"} />}  />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Адаптер" value={<TextFormControl name={"characteristics.hasAdapter"} />}  />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Чехол" value={<TextFormControl name={"characteristics.hasCase"} />} />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Влагозащита" value={<TextFormControl name={"characteristics.hasMoistureProtection"} />} />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Межзрачковое расстояние мин. (мм.)" value={<TextFormControl name={"characteristics.interpupillaryDistanseMin"} />} />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Межзрачковое расстояние макс. (мм.)" value={<TextFormControl name={"characteristics.interpupillaryDistanseMax"} />} />
                </Col>
                <Col>
                    <DetailsCharacteristic className="mb-5" isEditMode name="Фокусное расстояние мин. (мм.)" value={<TextFormControl name={"characteristics.focusDistanceMin"} />}  />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Материал оптики" value={<RichSelectFormControl className="characteristic-input" name={"characteristics.opticsMaterial"} clearable options={opticsMaterials} />}  />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Тип призмы" value={<RichSelectFormControl className="characteristic-input" name={"characteristics.prismType"} clearable options={prismTypes} />} />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Назначение" value={<RichSelectFormControl className="characteristic-input" name={"characteristics.binocularPurpose"} clearable options={binocularPurposes} />} />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Относительная яркость мин." value={<TextFormControl name={"characteristics.relativeBrightnessMin"} />} />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Относительная яркость макс." value={<TextFormControl name={"characteristics.relativeBrightnessMax"} />} />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Вынос выходного зрачка мин. (мм.)" value={<TextFormControl name={"characteristics.removalExitPupilMin"} />} />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Вынос выходного зрачка макс. (мм.)" value={<TextFormControl name={"characteristics.removalExitPupilMax"} />} />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Увеличение мин." value={<TextFormControl name={"characteristics.scaleMin"} />} />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Увеличение макс." value={<TextFormControl name={"characteristics.scaleMax"} />} />
                    <DetailsCharacteristic className="mb-5" isEditMode name="Вес" value={<TextFormControl name={"characteristics.weight"} />}  />
                </Col>
            </Row>
        </Col>
    </Row>);
};