import React from "react";
import { Row, Col } from "reactstrap";
import { TelescopeDetails } from "../../../dataModels/catalog/productDetails/telescopeDetails";
import { Local } from "../../localization/local";
import { DetailsCharacteristic } from "../../common/presentation/detailsCharacteristic";
import { TelescopeEyepiece } from "../../../dataModels/catalog/telescopeEyepiece";
import { TelescopeType } from "../../../dataModels/enums/telescope/telescopeType";
import { TelescopeUserLevel } from "../../../dataModels/enums/telescope/telescopeUserLevel";
import { MountingType } from "../../../dataModels/enums/telescope/mountingType";
import { TelescopeControlType } from "../../../dataModels/enums/telescope/telescopeControlType";
import { isEmpty } from "lodash";

interface Props {
    details: TelescopeDetails;
}

const getEyepieces = (eyepieces: TelescopeEyepiece[]): string => {
    if (isEmpty(eyepieces)) {
        return "Нет";
    }

    let result: string;

    for (let i = 0; i < eyepieces.length; i++) {
        result = result.concat(`${eyepieces[i].name} ${eyepieces[i].effectiveScale && "(" + eyepieces[i].effectiveScale + ")"}`);
    }

    return result;
};

const getTelescopeType = (type: TelescopeType) => {
    switch (type) {
        case TelescopeType.Reflector:
            return "Рефлектор";
        case TelescopeType.Refractor:
            return "Рефрактор";
        case TelescopeType.MirrorLens:
            return "Зеркально-линзовый";
    }
};

const getUserLevel = (level: TelescopeUserLevel) => {
    switch (level) {
        case TelescopeUserLevel.ForChildren:
            return "Для детей";
        case TelescopeUserLevel.ForBeginners:
            return "Для начинающих";
        case TelescopeUserLevel.ForConfidentUsers:
            return "Для уверенных пользователей";
        case TelescopeUserLevel.ForProfessionals:
            return "Для профессионалов";
    }
};

const getMountingType = (type: MountingType) => {
    switch (type) {
        case MountingType.Equatorial:
            return "Экваториальная";
        case MountingType.Azimutal:
            return "Азимутальная";
        case MountingType.Dobson:
            return "Добсона";
        case MountingType.EquatorialEQ1:
            return "Экваториальная EQ1";
    }
};

const getTelescopeControlType = (type: TelescopeControlType) => {
    switch (type) {
        case TelescopeControlType.Manual:
            return "Ручное";
        case TelescopeControlType.Autoguidance:
            return "С автонаведением";
    }
};

export const TelescopeCharacteristicsSection = (props: Props) => {
    return (
    <Row className="order-step-card p-3 mb-2">
        <Col>
            <Row className="mb-2">
                <h1 className="ui-section-header pt-2"><Local id="Characteristics"/></h1>
            </Row>
            <Row>
                <Col>
                    <DetailsCharacteristic name="Апертура (мм)" value={props.details.aperture?.toString()} />
                    <DetailsCharacteristic name="Светосила (относительное отверстие)" value={props.details.apertureRatio?.toString()}  />
                    <DetailsCharacteristic name="Посадочный диаметр окуляров (дюймов)" value={props.details.eyepieceFittingDiameter?.toString()}  />
                    <DetailsCharacteristic name="Фокусное расстояние (мм)" value={props.details.focusDistance?.toString()}  />
                    <DetailsCharacteristic name="Максимальное полезное увеличение (крат.)" value={props.details.maxUsefulScale?.toString()}  />
                    <DetailsCharacteristic name="Минимальное полезное увеличение (крат.)" value={props.details.minUsefulScale?.toString()}  />
                    <DetailsCharacteristic name="Тип монтировки" value={getMountingType(props.details.mountingType)}  />
                    <DetailsCharacteristic name="Тип управления телескопом" value={getTelescopeControlType(props.details.telescopeControlType)}  />
                    <DetailsCharacteristic name="Окуляры" value={getEyepieces(props.details.telescopeEyepieces)}  />
                </Col>
                <Col>
                    <DetailsCharacteristic name="Увеличение максимальное (крат.)" value={props.details.scaleMax?.toString()}  />
                    <DetailsCharacteristic name="Увеличение минимальное (крат.)" value={props.details.scaleMin?.toString()}  />
                    <DetailsCharacteristic name="Искатель" value={props.details.seeker?.toString()}  />
                    <DetailsCharacteristic name="Высота штатива" value={props.details.tripodHeight?.toString()}  />
                    <DetailsCharacteristic name="Материал штатива" value={props.details.tripodMaterial?.toString()}  />
                    <DetailsCharacteristic name="Тип телескопа" value={getTelescopeType(props.details.type)} />
                    <DetailsCharacteristic name="Пользовательский уровень" value={getUserLevel(props.details.telescopeUserLevel)}  />
                    <DetailsCharacteristic name="Вес" value={props.details.weight?.toString()}  />
                </Col>
            </Row>
        </Col>
    </Row>);
};