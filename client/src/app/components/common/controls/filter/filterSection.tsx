import React from "react";
import { Row } from "reactstrap";
import { Local } from "../../../localization/local";

export const FilterSection = (props: { headerKey?: string; children: React.ReactNode }) => {
    return (<div className="mb-4">
        {props.headerKey &&
            <h6 className="pb-3">
                <b><Local id={props.headerKey} /></b>
            </h6>
        }
        <Row>
            {props.children}
        </Row>
        <hr className="mt-4" />
    </div>);
};