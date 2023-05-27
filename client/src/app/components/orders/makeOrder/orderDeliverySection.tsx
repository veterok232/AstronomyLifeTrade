import React from "react";
import { Row } from "reactstrap";
import { RadioGroupFormControl } from "../../common/controls/formControls/radioGroupFormControl";
import { localizer } from "../../localization/localizer";
import { DeliveryType } from "../../../dataModels/enums/deliveryType";
import { required } from "../../common/controls/validation/validators";

export const OrderDeliverySection = () => {
    return (<>
        <Row className="section-description">
            <p>Выберите способ получения товара. При выборе способа доставки курьером, введите адрес, куда нам нужно будет доставить товар. Стоимость доставки курьером - 20 руб.</p>
        </Row>
        <Row className="">
            <RadioGroupFormControl
                className=""
                name="deliveryType"
                isNumericValue
                validator={required}
                options={[
                    {
                        label: localizer.get("DeliveryType.SelfPick"),
                        value: DeliveryType.SelfPick,
                    },
                    {
                        label: localizer.get("DeliveryType.Courier"),
                        value: DeliveryType.Courier,
                    },
                ]}
            />
        </Row>
    </>);
};