/* eslint-disable @typescript-eslint/no-unused-vars */
import React from "react";
import { IconicSearchButton } from "../../common/controls/buttons/iconicSearchButton";
import { TextFormControl } from "../../common/controls/formControls/textFormControl";
import { Form as FinalForm, FormRenderProps } from "react-final-form";
import { Col, Form, Row } from "reactstrap";

interface Props {
    className?: string;
    onSearch: () => void;
}

interface SearchFormData {
    searchString: string;
}

export const CatalogSearchBar = (props: Props) => {
    return (
        <div>
            <FinalForm onSubmit={props.onSearch}
                render={({ values, ...renderProps }: FormRenderProps<SearchFormData>) => {
                    return (
                    <Form className="d-flex">
                        <Row className="align-items-center mx-4">
                            <Col className="align-content-center m-0 p-0">
                                <TextFormControl className="search-bar search m-0 p-0" name="seachString" />
                            </Col>
                            <Col className="align-content-center m-0 p-0">
                                <IconicSearchButton className="search-bar search-button m-0 p-0" onSearch={props.onSearch}/>
                            </Col>
                        </Row>
                    </Form>);
            }} />
        </div>
    );
};