import React from "react";
import { Row } from "reactstrap";
import { RadioGroupFormControl } from "../../common/controls/formControls/radioGroupFormControl";
import { localizer } from "../../localization/localizer";
import { PaymentMethod } from "../../../dataModels/enums/paymentMethod";
import { required } from "../../common/controls/validation/validators";

export const OrderPaymentSection = () => {
    return (<>
        <Row className="section-description">
            <p>Выберите способ получения оплаты товара.</p>
        </Row>
        <Row className="">
            <RadioGroupFormControl
                className=""
                name="paymentMethod"
                isNumericValue
                validator={required}
                options={[
                    {
                        label: localizer.get("PaymentMethod.Cash"),
                        value: PaymentMethod.Cash,
                    },
                    {
                        label: localizer.get("PaymentMethod.Card"),
                        value: PaymentMethod.Card,
                    },
                ]}
            />
        </Row>
    </>);
};