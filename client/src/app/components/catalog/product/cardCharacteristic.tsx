import React from "react";
import { CharacteristicModel } from "../../../dataModels/catalog/characteristicModel";

interface Props {
    characteristic: CharacteristicModel;
    className?: string;
}

export const CardCharacteristic = (props: Props) => {
    return (
        <div className={`card-characteristic ${props.className}`}><span>{props.characteristic.name}: {props.characteristic.value}</span></div>
    );
};