import React from "react";
import { IconicSearchButton } from "../../common/controls/buttons/iconicSearchButton";
import { TextFormControl } from "../../common/controls/formControls/textFormControl";
import { Form as FinalForm, FormRenderProps } from "react-final-form";
import { Form } from "reactstrap";

interface Props {
    className?: string;
    onSearch: () => void;
};

interface SearchFormData {
    searchString: string;
}

export const CatalogSearchBar = (props: Props) => {
    return (
        <div>
            <FinalForm onSubmit={props.onSearch}
                render={({ values, ...renderProps }: FormRenderProps<SearchFormData>) => {
                    return (
                    <Form>
                        <TextFormControl name={"seachString"} />
                        <IconicSearchButton onSearch={props.onSearch}/>
                    </Form>);
            }} />
        </div>
    );
};