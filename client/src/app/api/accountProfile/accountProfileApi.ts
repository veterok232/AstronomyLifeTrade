import { StatisticsQuery } from "../../components/accountProfile/staffProfile";
import { ManagersLookupItemsRequest } from "../../components/accountProfile/statisticsFilter";
import { UsersLookupItemsRequest } from "../../components/accountProfile/usersManagementSection";
import { OrdersAggregatedDataItem } from "../../components/common/statisticsDiagram/ordersStatisticsDiagram/ordersStatisticsDiagram";
import { SaveUserInfoModel } from "../../dataModels/accountProfile/saveUserInfoModel";
import { UserInfoModel } from "../../dataModels/accountProfile/userInfoModel";
import { Address } from "../../dataModels/address";
import { NamedObject } from "../../dataModels/common/namedObject";
import { Result } from "../../dataModels/common/result";
import { stringifyObjectToQueryString } from "../../utils/requestParameterUtils";
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

export async function getOrdersStatistics(query: StatisticsQuery): Promise<Array<OrdersAggregatedDataItem>> {
    return httpGet({
        url: `${resourceName}/get-orders-statistics?${stringifyObjectToQueryString(query)}`,
    });
}

export function getManagersLookupItems(request: ManagersLookupItemsRequest): Promise<Array<NamedObject>> {
    return httpGet({
        url: `${resourceName}/managers-lookup-items?${stringifyObjectToQueryString(request)}`,
        silent: true,
    });
}

export function getUsersLookupItems(request: UsersLookupItemsRequest): Promise<Array<NamedObject>> {
    return httpGet({
        url: `${resourceName}/users-lookup-items?${stringifyObjectToQueryString(request)}`,
        silent: true,
    });
}

export async function assignAsManager(userAssignmentId: string): Promise<Result> {
    return httpPost({
        url: `${resourceName}/assign-as-manager/${userAssignmentId}`,
    });
}

export async function assignAsAdministrator(userAssignmentId: string): Promise<Result> {
    return httpPost({
        url: `${resourceName}/assign-as-administrator/${userAssignmentId}`,
    });
}

export async function blockUser(userAssignmentId: string): Promise<Result> {
    return httpPost({
        url: `${resourceName}/block-user/${userAssignmentId}`,
    });
}

export async function unblockUser(userAssignmentId: string): Promise<Result> {
    return httpPost({
        url: `${resourceName}/unblock-user/${userAssignmentId}`,
    });
}