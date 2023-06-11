import React from "react";
import { Col, Label, Row } from "reactstrap";
import { Local } from "../../localization/local";

interface Props {
    className?: string;
    labelKey: string;
    value: string | number | JSX.Element;
    isHorizontalView?: boolean;
    supressMargins?: boolean;
}

export const LabeledField = (props: Props) => {
    return (
        <Row className={props.className}>
            {!props.isHorizontalView &&
                <div className={`labeled-field ${props.supressMargins && "pb-0"}`}>
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
                    <Col className="d-flex justify-content-start align-items-center">
                        <Label className="field-key my-0">
                            <Local id={props.labelKey} />
                        </Label>
                    </Col>
                    <Col className="d-flex align-items-center justify-content-end w-100">
                        <span className="field-value my-0 float-right">
                            {props.value}
                        </span>
                    </Col>
                </Row>
                }
        </Row>
    );
};
