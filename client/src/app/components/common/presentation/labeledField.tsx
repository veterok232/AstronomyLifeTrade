import React from "react";
import { Col, Label, Row } from "reactstrap";
import { Local } from "../../localization/local";

interface Props {
    labelKey: string;
    value: string | number | JSX.Element;
    isHorizontalView?: boolean;
}

export const LabeledField = (props: Props) => {
    return (
        <>
            {!props.isHorizontalView &&
                <div className="labeled-field">
                    <Label className="field-key mb-1">
                        <Local id={props.labelKey} />
                    </Label>
                    <span className="field-value d-block">
                        {props.value}
                    </span>
                </div>
            }
            {props.isHorizontalView &&
                <Row className="justify-content-between horizontal-labeled-field">
                    <Col className="float-left">
                        <Label className="field-key float-left mb-1">
                            <Local id={props.labelKey} />
                        </Label>
                    </Col>
                    <Col className="align-items-center">
                        <span className="field-value my-auto d-block float-right">
                            {props.value}
                        </span>
                    </Col>
                </Row>
                }
        </>
    );
};
