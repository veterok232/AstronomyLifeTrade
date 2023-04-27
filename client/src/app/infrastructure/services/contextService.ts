import { flow } from "mobx";
import { ContextResponse } from "../../dataModels/common/contextResponse";
import { getContext } from "../../api/contextApi";
import { contextStore } from "../stores/contextStore";

export const contextActions = {
    load: flow(function* () {
        const context: ContextResponse = yield getContext();
        contextStore.setContext(context);
    }),
};