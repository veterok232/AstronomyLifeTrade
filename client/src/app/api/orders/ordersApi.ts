import { ListResult } from "../../dataModels/common/listResult";
import { Result } from "../../dataModels/common/result";
import { MakeOrderModel } from "../../dataModels/orders/makeOrder/makeOrderModel";
import { OrderCustomerInfo } from "../../dataModels/orders/makeOrder/orderCustomerInfo";
import { OrderDetailsModel } from "../../dataModels/orders/orderDetailsModel";
import { OrderListItem } from "../../dataModels/orders/orderListItem";
import { OrdersSearchRequest } from "../../dataModels/orders/ordersSearchRequest";
import { RemoveOrderItemModel } from "../../dataModels/orders/removeOrderItemModel";
import { stringifyObjectToQueryString } from "../../utils/requestParameterUtils";
import { httpGet, httpPost } from "../core/requestApi";

const resourceName = "orders";

export async function searchOrders(searchRequest: OrdersSearchRequest): Promise<ListResult<OrderListItem>> {
    return httpGet({
        url: `${resourceName}/search?${stringifyObjectToQueryString(searchRequest)}`,
        disableSuccessfulToast: true,
    });
}

export async function getOrderDetails(orderId: string): Promise<OrderDetailsModel> {
    return httpGet({
        url: `${resourceName}/details/${orderId}`,
        disableSuccessfulToast: true,
    });
}

export async function getOrderCustomerInfo(): Promise<OrderCustomerInfo> {
    return httpGet({
        url: `${resourceName}/get-customer-info`,
        disableSuccessfulToast: true,
    });
}

export async function makeOrder(data: MakeOrderModel): Promise<Result<number>> {
    return httpPost({
        url: `${resourceName}/make-order`,
        body: data,
        disableSuccessfulToast: true,
    });
}

export async function removeOrderItem(data: RemoveOrderItemModel) {
    return httpPost({
        url: `${resourceName}/remove-order-item`,
        body: data,
        disableSuccessfulToast: true,
    });
}

export async function postponeOrder(orderId: string) {
    return httpPost({
        url: `${resourceName}/postpone-order/${orderId}`,
    });
}

export async function cancelOrder(orderId: string) {
    return httpPost({
        url: `${resourceName}/cancel-order/${orderId}`,
    });
}

export async function approveOrder(orderId: string) {
    return httpPost({
        url: `${resourceName}/approve-order/${orderId}`,
    });
}

export async function closeOrder(orderId: string) {
    return httpPost({
        url: `${resourceName}/close-order/${orderId}`,
    });
}