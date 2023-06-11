import React from "react";
import { Row, Col } from "reactstrap";
import { RichSelectFormControl } from "../../../common/controls/formControls/richSelectFormControl";
import { LabeledValue } from "../../../common/controls/labeledValue";
import { DetailsCharacteristic } from "../../../common/presentation/detailsCharacteristic";
import { AccessoryType } from "../../../../dataModels/enums/accessory/accessoryType";

const accessoryTypes: LabeledValue[] = [{
    label: "Линза Барлоу",
    value: AccessoryType.BarlowLens,
}, {
    label: "Окуляр",
    value: AccessoryType.Ocular,
}, {
    label: "Световой фильтр",
    value: AccessoryType.LightFilter,
}, {
    label: "Солнечный фильтр",
    value: AccessoryType.SolarFilter,
}, {
    label: "Монтировки и штативы",
    value: AccessoryType.MountingsAndTripods,
}, {
    label: "Для монтировок",
    value: AccessoryType.ForMountings,
}, {
    label: "Автонаведение",
    value: AccessoryType.Autoguidance,
}, {
    label: "Сумки и чехлы",
    value: AccessoryType.BagsAndCases,
}];

export const AccessoriesCharacteristicsSection = () => {
    return (
    <Row className="order-step-card p-3 mb-2">
        <Col>
            <Row>
                <Col>
                    <DetailsCharacteristic className="mb-5" isEditMode name="Тип аксессуара" value={<RichSelectFormControl className="characteristic-input" name={"characteristics.accessoryType"} clearable options={accessoryTypes} />}  />
                </Col>
                <Col>
                </Col>
            </Row>
        </Col>
    </Row>);
};