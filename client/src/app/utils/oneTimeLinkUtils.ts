import { apiRootUrl } from "../api/core/requestApi";
import { createOneTimeToken } from "../api/identity/identityApi";
import { OneTimeTokenTermType } from "../dataModels/enums/oneTimeTokenTermType";

export async function getOneTimeLink(resource: string): Promise<string> {
    const token = await createOneTimeToken(OneTimeTokenTermType.Short);

    return `${apiRootUrl}/${resource}${resource.includes("?") ? "&" : "?"}auth-token=${token}`;
}