import React from "react";
import { Row, Col } from "reactstrap";
import { BinocularDetails } from "../../../dataModels/catalog/productDetails/binocularDetails";
import { DetailsCharacteristic } from "../../common/presentation/detailsCharacteristic";
import { FocusingMethod } from "../../../dataModels/enums/binocuclar/focusingMethod";
import { OpticsMaterial } from "../../../dataModels/enums/binocuclar/opticsMaterial";
import { PrismType } from "../../../dataModels/enums/binocuclar/prismType";
import { BinocularPurpose } from "../../../dataModels/enums/binocuclar/binocularPurpose";

interface Props {
    details: BinocularDetails;
}

const getFocusingMethod = (method: FocusingMethod) => {
    switch (method) {
        case FocusingMethod.Central:
            return "Центральный";
        case FocusingMethod.Differential:
            return "Дифференциальный";
    }
};

const getOpticsMaterial = (material: OpticsMaterial) => {
    switch (material) {
        case OpticsMaterial.Bak4:
            return "Стекло BaK-4";
        case OpticsMaterial.Bak7:
            return "Стекло BaK-7";
        case OpticsMaterial.Bak10:
            return "Стекло BaK-10";
        case OpticsMaterial.EcoGlass:
            return "Eco-Glass";
        case OpticsMaterial.ED:
            return "ED";
        case OpticsMaterial.K9:
            return "Стекло K9";
        case OpticsMaterial.OpticalPlastic:
            return "Оптический пластик";
        case OpticsMaterial.OpticalGlass:
            return "Оптическое стекло";
    }
};

const getPrismType = (type: PrismType) => {
    switch (type) {
        case PrismType.AbbeKonig:
            return "Abbe-Konig";
        case PrismType.GalileyScheme:
            return "Схема Галилея";
        case PrismType.Porro:
            return "Porro";
        case PrismType.Roof:
            return "Roof";
        case PrismType.RoofWithPhaseCorrection:
            return "Roof с фазовой коррекцией";
    }
};

const getPurpose = (purpose: BinocularPurpose) => {
    switch (purpose) {
        case BinocularPurpose.Army:
            return "Армейский";
        case BinocularPurpose.Astronomical:
            return "Астрономический";
        case BinocularPurpose.Children:
            return "Детский";
        case BinocularPurpose.HuntingAndFishing:
            return "Для охоты и рыбалки";
        case BinocularPurpose.Navy:
            return "Морской";
        case BinocularPurpose.Sport:
            return "Спортивный";
    }
};

export const BinocularCharacteristicsSection = (props: Props) => {
    return (
        <Row className="order-step-card p-3 mb-2">
            <Col>
                <Row>
                    <Col>
                        <DetailsCharacteristic name="Апертура (мм)" value={props.details.aperture?.toString()} />
                        <DetailsCharacteristic name="Диаметр выходного зрачка макс. (мм)" value={props.details.exitPupilDiameterMax?.toString()}  />
                        <DetailsCharacteristic name="Диаметр выходного зрачка мин. (мм)" value={props.details.exitPupilDiameterMin?.toString()} />
                        <DetailsCharacteristic name="Способ фокусировки" value={getFocusingMethod(props.details.focusingMethod)}  />
                        <DetailsCharacteristic name="Поле зрения макс. (градусов)" value={props.details.fovMax?.toString()}  />
                        <DetailsCharacteristic name="Поле зрения мин. (градусов)" value={props.details.fovMin?.toString()}  />
                        <DetailsCharacteristic name="Адаптер" value={props.details.hasAdapter?.toString()}  />
                        <DetailsCharacteristic name="Чехол" value={props.details.hasCase?.toString()} />
                        <DetailsCharacteristic name="Влагозащита" value={props.details.hasMoistureProtection?.toString()} />
                        <DetailsCharacteristic name="Межзрачковое расстояние мин. (мм.)" value={props.details.interpupillaryDistanseMin?.toString()} />
                        <DetailsCharacteristic name="Межзрачковое расстояние макс. (мм.)" value={props.details.interpupillaryDistanseMax?.toString()} />
                    </Col>
                    <Col>
                        <DetailsCharacteristic name="Фокусное расстояние мин. (мм.)" value={props.details.focusDistanceMin?.toString()}  />
                        <DetailsCharacteristic name="Материал оптики" value={getOpticsMaterial(props.details.opticsMaterial)}  />
                        <DetailsCharacteristic name="Тип призмы" value={getPrismType(props.details.prismType)} />
                        <DetailsCharacteristic name="Назначение" value={getPurpose(props.details.binocularPurpose)} />
                        <DetailsCharacteristic name="Относительная яркость мин." value={props.details.relativeBrightnessMin?.toString()} />
                        <DetailsCharacteristic name="Относительная яркость макс." value={props.details.relativeBrightnessMax?.toString()} />
                        <DetailsCharacteristic name="Вынос выходного зрачка мин. (мм.)" value={props.details.removalExitPupilMin?.toString()} />
                        <DetailsCharacteristic name="Вынос выходного зрачка макс. (мм.)" value={props.details.removalExitPupilMax?.toString()} />
                        <DetailsCharacteristic name="Увеличение мин." value={props.details.scaleMin?.toString()} />
                        <DetailsCharacteristic name="Увеличение макс." value={props.details.scaleMax?.toString()} />
                        <DetailsCharacteristic name="Вес" value={props.details.weight?.toString()}  />
                    </Col>
                </Row>
            </Col>
        </Row>);
};