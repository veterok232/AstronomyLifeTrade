import React from "react";
import { Row, Col } from "reactstrap";
import { AccessoryDetails } from "../../../dataModels/catalog/productDetails/accessoryDetails";
import { AccessoryType } from "../../../dataModels/enums/accessory/accessoryType";
import { DetailsCharacteristic } from "../../common/presentation/detailsCharacteristic";

interface Props {
    details: AccessoryDetails;
}

const getAccessoryType = (type: AccessoryType) => {
    switch (type) {
        case AccessoryType.BarlowLens:
            return "Линза Барлоу";
        case AccessoryType.Ocular:
            return "Окуляр";
        case AccessoryType.LightFilter:
            return "Световой фильтр";
        case AccessoryType.SolarFilter:
            return "Солнечный фильтр";
        case AccessoryType.MountingsAndTripods:
            return "Монтировки и штативы";
        case AccessoryType.ForMountings:
            return "Для монтировок";
        case AccessoryType.Autoguidance:
            return "Автонаведение";
        case AccessoryType.BagsAndCases:
            return "Сумки и чехлы";
    }
};

export const AccessoryCharacteristicsSection = (props: Props) => {
    return (
        <Row className="order-step-card p-3 mb-2">
            <Col>
                <Row>
                    <Col>
                        <DetailsCharacteristic name="Тип аксессуара" value={getAccessoryType(props.details.accessoryType)}  />
                    </Col>
                    <Col>
                    </Col>
                </Row>
            </Col>
        </Row>);
};