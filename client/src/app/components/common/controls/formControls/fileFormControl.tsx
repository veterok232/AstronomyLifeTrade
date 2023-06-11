/* eslint-disable @typescript-eslint/no-unused-vars */
import React from "react";
import { FieldRenderProps } from "react-final-form";
import { convertExtensionsListToAcceptString } from "../../../../utils/fileUtils";
import { joinWithSeparator } from "../../../../utils/formattingUtils";
import { Local } from "../../../localization/local";
import { ValidationList } from "../../presentation/validationList";
import { fileListSizeValidator, fileSizeValidator } from "../validation/fileValidators";
import { composeValidators } from "../validation/validators";
import { FormControlProps, PrefixedField, withFormWrapper } from "./formControlsDecorators";

interface FileFormControlProps extends FormControlProps {
    multiple?: boolean;
    accept?: string[];
    initialFileName?: string;
}

const FileControl = (props: FileFormControlProps) => {
    const [fileName, setFileName] = React.useState(props.initialFileName || "");
    const sizeValidator = props.multiple ? fileListSizeValidator : fileSizeValidator;
    const validator = props.validator ? composeValidators(props.validator, sizeValidator) : sizeValidator;

    const validate = (file: File) => {
        if (!file && props.initialFileName) {
            return undefined;
        }

        return validator(file);
    };

    const onChangeInternal = (files: FileList, formOnChange: (value: FileList | File) => void) => {
        if (!files?.length) {
            return;
        }

        formOnChange(props.multiple ? files : files[0]);
        setFileName(joinWithSeparator(Array.from(files).map((x) => x.name.split("\\").pop())));
    };

    return (
        <PrefixedField name={props.name} validate={validate}>
            {({ input: { value, onChange, ...input }, meta }: FieldRenderProps<FileList | File, HTMLInputElement>) => {
                const showError = meta.error && meta.touched;
                return (
                    <>
                        <div className={`form-group__file-wrapper ${showError && "form-control--error"}`}>
                            <label className="w-100">
                                <input
                                    type="file"
                                    {...input}
                                    accept={convertExtensionsListToAcceptString(props.accept)}
                                    multiple={props.multiple}
                                    className="d-none"
                                    readOnly={props.readonly}
                                    onChange={(e) => {
                                        onChangeInternal(e.target.files, onChange);
                                        props.onChange?.(e.target.files);
                                    }}
                                />
                                {fileName && <span className="file-name text-truncate">{fileName}</span>}
                                {!fileName && <span className="placeholder text-truncate">{props.placeholder}</span>}
                                <span className="browse-button">
                                    <Local id="FormControl_BrowseFile" />
                                </span>
                            </label>
                        </div>
                        <ValidationList showValidations={showError} validations={meta.error} />
                    </>
                );
            }}
        </PrefixedField>
    );
};

export const FileFormControl = withFormWrapper(FileControl);