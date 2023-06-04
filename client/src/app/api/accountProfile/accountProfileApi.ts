import { SaveUserInfoModel } from "../../dataModels/accountProfile/saveUserInfoModel";
import { UserInfoModel } from "../../dataModels/accountProfile/userInfoModel";
import { Address } from "../../dataModels/address";
import { httpGet, httpPost } from "../core/requestApi";

const resourceName = "account-profile";

export async function getUserInfo(): Promise<UserInfoModel> {
    return httpGet({
        url: `${resourceName}/get-user-info`,
    });
}

export async function saveUserInfo(data: SaveUserInfoModel) {
    return httpPost({
        url: `${resourceName}/save-user-info`,
        body: data,
    });
}

export async function saveUserAddress(data: Address) {
    return httpPost({
        url: `${resourceName}/save-user-address`,
        body: data,
    });
}