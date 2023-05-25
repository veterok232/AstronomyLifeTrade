import React from "react";
import { contextStore } from "../../infrastructure/stores/contextStore";

interface Props {
    className?: string;
}

export const Greetings = (props: Props) => {
    return (
        <span className={props.className}>Здравствуйте, {contextStore.fullName}!</span>
    );
};