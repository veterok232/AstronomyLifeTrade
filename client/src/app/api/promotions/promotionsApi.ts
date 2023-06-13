import { Result } from "../../dataModels/common/result";
import { PromotionModel } from "../../dataModels/promotions/promotionModel";
import { httpGet } from "../core/requestApi";

const resourceName = "promotions";

export async function getPromotion(promocode: string): Promise<Result<PromotionModel>> {
    return httpGet({
        url: `${resourceName}/${promocode}`,
    });
}