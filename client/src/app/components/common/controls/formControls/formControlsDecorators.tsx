import React from "react";
import { Validator } from "../validation/validators";
import { localizer } from "../../../localization/localizer";
import { FormGroup } from "reactstrap";
import { Field } from "react-final-form";


export interface CoreFormControlProps {
    name: string;
    validator?: Validator;
    readonly?: boolean;
}

export interface FormControlProps extends CoreFormControlProps {
    label?: string | JSX.Element;
    placeholder?: string;
    onChange?: (val: any) => void;
    className?: string;
    markRequired?: boolean;
}

const childName = (name: string, parentName?: string) => parentName ? `${parentName}.${name}` : name;

function getLabel(label: string | JSX.Element): JSX.Element {
    return typeof (label) === "string"
        ? <label>{localizer.get(label)}</label>
        : label;
}

export function withFormWrapper<TOutProps extends FormControlProps>(WrappedComponent: React.ComponentType<TOutProps>) {
    const componentWrapper = (props: TOutProps) => {
        return (<FormGroup className={`${props.className || ""} ${props.readonly ? "form-group--readonly" : ""} ${props.markRequired ? "required" : ""}`}>
            {props.label && getLabel(props.label)}
            <WrappedComponent {...props} />
        </FormGroup>);
    };
    componentWrapper.displayName = `WithFormControl${WrappedComponent.displayName || WrappedComponent.name || "Component"}`;
    return componentWrapper;
}

const FieldPrefixContext = React.createContext("");

const FieldPrefix = (props: { children: any; prefix: string }) => (
    <FieldPrefixContext.Provider value={props.prefix}>
        {props.children}
    </FieldPrefixContext.Provider>
);

export const PrefixedField = ({ name, ...props }: any) => (
    <FieldPrefixContext.Consumer>
        {/* eslint-disable-next-line @typescript-eslint/no-unsafe-argument */}
        {prefix => <Field name={childName(name, prefix)} {...props} />}
    </FieldPrefixContext.Consumer>
);

export function withParent<TProps = {}>(WrappedComponent: React.ComponentType<TProps>, parentName: string, props?: TProps) {
    return <FieldPrefix prefix={parentName}>
        <WrappedComponent parentName={parentName} {...props} />
    </FieldPrefix>;
}