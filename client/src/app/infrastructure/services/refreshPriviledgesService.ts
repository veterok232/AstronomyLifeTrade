import jwt_decode from "jwt-decode";
import { contextStore } from "../stores/contextStore";

export const refreshContextPrivileges = (token: string) => {
    const data: { privilege: string[] } = jwt_decode(token);

    contextStore.setPermissions(Array.isArray(data.privilege) ? data.privilege : [data.privilege]);
};