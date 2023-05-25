import React from "react";

interface Props {
    name: string;
    value: string;
}

export const DetailsCharacteristic = (props: Props) => {
    return (
        <div>
            {props.name}: {props.value}
        </div>
    );
};