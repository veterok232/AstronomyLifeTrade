import { PromotionType } from "../enums/promotionType";

export interface PromotionModel {
    name: string;
    promoRate: number;
    startDate: Date;
    endDate?: Date;
    promotionType: PromotionType;
    promoCode: string;
}