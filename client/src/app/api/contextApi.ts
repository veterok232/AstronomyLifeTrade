import { ContextResponse } from "../dataModels/common/contextResponse";
import { httpGet } from "./core/requestApi";

export function getContext(): Promise<ContextResponse> {
    return httpGet({
        url: "contexts",
    });
}