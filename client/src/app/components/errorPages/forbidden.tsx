import React from "react";
import { ErrorPage } from "./errorPage";
import forbiddenImage from "../../../static/images/errors/forbidden.svg";

export function Forbidden() {
    return (<ErrorPage imageSrc={forbiddenImage}
        titleKey="ErrorPage_Forbidden_Title"
        descriptionKey="ErrorPage_Forbidden_Description" />);
}