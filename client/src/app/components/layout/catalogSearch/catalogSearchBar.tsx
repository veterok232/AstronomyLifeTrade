/* eslint-disable @typescript-eslint/no-unused-vars */
import React from "react";
import { IconicSearchButton } from "../../common/controls/buttons/iconicSearchButton";
import { TextFormControl } from "../../common/controls/formControls/textFormControl";
import { Form as FinalForm, FormRenderProps } from "react-final-form";
import { Col, Form, Row } from "reactstrap";
import { FormApi } from "final-form";

interface Props {
    className?: string;
    onSearch: (searchString: string) => void;
}

interface SearchFormData {
    searchString: string;
}

export const CatalogSearchBar = (props: Props) => {
    const onSearch = (searchString: string, form: FormApi<SearchFormData, Partial<SearchFormData>>) => {
        if (!searchString) {
            return;
        }

        props.onSearch(searchString);
        form.reset();
    };

    return (
        <div>
            <FinalForm onSubmit={() => null}
                render={({ values, form, ...renderProps }: FormRenderProps<SearchFormData>) => {
                    return (
                    <Form className="d-flex">
                        <Row className="align-items-center mx-4">
                            <Col className="align-content-center m-0 p-0">
                                <TextFormControl
                                    className="search-bar search m-0 p-0" name="searchString"
                                    placeholder="Например, телескоп" />
                            </Col>
                            <Col className="align-content-center m-0 p-0">
                                <IconicSearchButton
                                    className="search-bar search-button m-0 p-0"
                                    onSearch={() => onSearch(values.searchString, form)} />
                            </Col>
                        </Row>
                    </Form>);
            }} />
        </div>
    );
};