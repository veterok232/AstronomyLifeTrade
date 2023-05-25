import React from "react";
import { FieldArray } from "react-final-form-arrays";

interface FormControlsArrayProps {
    name: string;
    addButtonTextKey?: string;
    maxItemsNumber?: number;
    children: (childName: string, i: number) => React.ReactNode;
    className?: string;
    hideCancelButtons?: boolean;
    validator?: (value: string[]) => string[] | Promise<string[]>;
    onRemove?: (index: number) => void | Promise<void>;
}

export function FormControlsArray(props: FormControlsArrayProps) {
    return (
        <FieldArray name={props.name} validate={props.validator}>
            {({ fields }) => (
                <>
                    {fields.map((name, i) => (
                        props.children(name, i)
                    ))}
                </>
            )}
        </FieldArray>
    );
}
