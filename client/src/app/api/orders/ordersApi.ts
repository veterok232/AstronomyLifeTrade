import { Result } from "../../dataModels/common/result";
import { MakeOrderModel } from "../../dataModels/orders/makeOrder/makeOrderModel";
import { OrderCustomerInfo } from "../../dataModels/orders/makeOrder/orderCustomerInfo";
import { httpGet, httpPost } from "../core/requestApi";

const resourceName = "orders";

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