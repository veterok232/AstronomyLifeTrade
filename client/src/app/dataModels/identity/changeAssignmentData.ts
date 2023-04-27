import { ClientIdentificationData } from "./clientIdentificationData";

export interface ChangeAssignmentData extends ClientIdentificationData {
    assignmentId: string;
}