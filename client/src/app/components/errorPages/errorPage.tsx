import React from "react";
import { Container } from "reactstrap";
import { getDefaultPageRoute } from "../../utils/routeUtils";
import { Local } from "../localization/local";
import { Link } from "react-router-dom";

interface Props {
    imageSrc: string;
    titleKey: string;
    descriptionKey: string;
}

export function ErrorPage(props: Props) {
    return (<Container className="error-page d-flex flex-column align-items-center p-3 p-md-5">
        <img className="illustration" src={props.imageSrc} />
        <div className="info">
            <h1 className="ui-page-header text-center"><Local id={props.titleKey} /></h1>
            <p className="text-center"><Local id={props.descriptionKey} /></p>
            <Link to={getDefaultPageRoute()} className="bg-primary border-0 btn btn-secondary d-block mx-auto">
                <Local id="ErrorPage_RedirectButton" />
            </Link>
        </div>
    </Container>);
}