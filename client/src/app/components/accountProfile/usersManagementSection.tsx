import React from "react";
import { FormRenderProps, Form as FinalForm } from "react-final-form";
import { RichSelectFormControl } from "../common/controls/formControls/richSelectFormControl";
import { localizer } from "../localization/localizer";
import { LabeledValue } from "../common/controls/labeledValue";
import { assignAsAdministrator, assignAsManager, blockUser, getUsersLookupItems, unblockUser } from "../../api/accountProfile/accountProfileApi";
import { Button, Col, Form, Row } from "reactstrap";
import { Local } from "../localization/local";
import { notifications } from "../toast/toast";
import { showConfirmation } from "../common/confirmationModal";
import { required } from "../common/controls/validation/validators";
import { FormApi, ValidationErrors } from "final-form";

export interface UsersLookupItemsRequest {
    searchValue?: string;
    selectedId?: string;
}

interface UsersManagementFormData {
    userAssignmentId?: string;
}

export const getUsersSelectItems = async (request: UsersLookupItemsRequest): Promise<LabeledValue[]> => {
    const result = await getUsersLookupItems(request);
    return result.map(r => ({ value: r.id, label: r.name }));
};

const onAssignAsManager = async (
    handleSubmit: () => Promise<ValidationErrors>,
    valid: boolean,
    form: FormApi<UsersManagementFormData, Partial<UsersManagementFormData>>,
    userAssignmentId: string
) => {
    await handleSubmit();

    if (!valid) {
        return;
    }

    form.reset();

    showConfirmation({
        body: localizer.get("AssignAsManagerConfirmation_Body"),
        onConfirmClick: async () => {
            const result = await assignAsManager(userAssignmentId);

            if (!result.isSucceeded) {
                notifications.localizedError(result.errors[0]);
                return;
            }
        }
    });
};

const onAssignAsAdministrator = async (
    handleSubmit: () => Promise<ValidationErrors>,
    valid: boolean,
    form: FormApi<UsersManagementFormData, Partial<UsersManagementFormData>>,
    userAssignmentId: string
) => {
    await handleSubmit();

    if (!valid) {
        return;
    }

    showConfirmation({
        body: localizer.get("AssignAsAdministratorConfirmation_Body"),
        onConfirmClick: async () => {
            const result = await assignAsAdministrator(userAssignmentId);

            form.reset();

            if (!result.isSucceeded) {
                notifications.localizedError(result.errors[0]);
                return;
            }
        }
    });
};

const onBlockUser = async (
    handleSubmit: () => Promise<ValidationErrors>,
    valid: boolean,
    form: FormApi<UsersManagementFormData, Partial<UsersManagementFormData>>,
    userAssignmentId: string
) => {
    await handleSubmit();

    if (!valid) {
        return;
    }

    showConfirmation({
        body: localizer.get("BlockUserConfirmation_Body"),
        onConfirmClick: async () => {
            const result = await blockUser(userAssignmentId);

            form.reset();

            if (!result.isSucceeded) {
                notifications.localizedError(result.errors[0]);
                return;
            }
        }
    });
};

const onUnblockUser = async (
    handleSubmit: () => Promise<ValidationErrors>,
    valid: boolean,
    form: FormApi<UsersManagementFormData, Partial<UsersManagementFormData>>,
    userAssignmentId: string
) => {
    await handleSubmit();

    if (!valid) {
        return;
    }

    showConfirmation({
        body: localizer.get("UnblockUserConfirmation_Body"),
        onConfirmClick: async () => {
            const result = await unblockUser(userAssignmentId);

            form.reset();

            if (!result.isSucceeded) {
                notifications.localizedError(result.errors[0]);
                return;
            }
        }
    });
};

export const UsersManagementSection = () => {
    return (
        <FinalForm
            onSubmit={() => null}
            render={({ values, valid, form, handleSubmit }: FormRenderProps<UsersManagementFormData>) => (
                <Form onSubmit={handleSubmit}>
                    <Row>
                        <Col>
                            <RichSelectFormControl
                                name="userAssignmentId"
                                label="UsersManagement_User"
                                optionsLoader={async (inputValue) =>
                                    await getUsersSelectItems({
                                        searchValue: inputValue,
                                        selectedId: values.userAssignmentId,
                                    })
                                }
                                clearable
                                placeholder={localizer.get("Name")}
                                onChange={() => null}
                                validator={required}
                                searchable
                            />
                        </Col>
                    </Row>
                    <Row>
                        <Col>
                            <Button className="mr-2 button-approve"
                                onClick={() => onAssignAsManager(handleSubmit, valid, form, values.userAssignmentId)}>
                                <Local id="AssignAsManager" />
                            </Button>
                            <Button className="mr-2 button-postpone"
                                onClick={() => onAssignAsAdministrator(handleSubmit, valid, form, values.userAssignmentId)}>
                                <Local id="AssignAsAdministrator" />
                            </Button>
                            <Button className="mr-2 button-cancel"
                                onClick={() => onBlockUser(handleSubmit, valid, form, values.userAssignmentId)}>
                                <Local id="BlockUser" />
                            </Button>
                            <Button className="mr-2 button-cancel"
                                onClick={() => onUnblockUser(handleSubmit, valid, form, values.userAssignmentId)}>
                                <Local id="UnblockUser" />
                            </Button>
                        </Col>
                    </Row>
                </Form>)}
        />
    );
};