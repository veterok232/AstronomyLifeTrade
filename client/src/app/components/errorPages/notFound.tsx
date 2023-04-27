import React from "react";
import { ErrorPage } from "./errorPage";
import notFoundImage from "../../../static/images/errors/not-found.svg";

export function NotFound() {
    return (<ErrorPage imageSrc={notFoundImage}
        titleKey="ErrorPage_NotFound_Title"
        descriptionKey="ErrorPage_NotFound_Description" />);
}