import { AstronomicalCalculatorMostMatchingModel } from "../../dataModels/astronomicalCalculator/astronomicalCalculatorMostMatchingModel";
import { ProductListItem } from "../../dataModels/catalog/productListItem";
import { stringifyObjectToQueryString } from "../../utils/requestParameterUtils";
import { httpGet } from "../core/requestApi";

const resourceName = "astronomical-calculator";

export async function getMostMatchingTelescopes(
    model: AstronomicalCalculatorMostMatchingModel,
): Promise<ProductListItem[]> {
    return httpGet({
        url: `${resourceName}/get-most-matching-telescopes?${stringifyObjectToQueryString(model)}`,
    });
}