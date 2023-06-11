import { BinocularPurpose } from "../enums/binocuclar/binocularPurpose";
import { FocusingMethod } from "../enums/binocuclar/focusingMethod";
import { OpticsMaterial } from "../enums/binocuclar/opticsMaterial";

export interface BinocularFilterData {
    brandsIds?: string[];
    priceMin?: number;
    priceMax?: number;
    focusingMethods?: FocusingMethod[];
    opticsMaterials?: OpticsMaterial[];
    binocularPurposes?: BinocularPurpose[];
}