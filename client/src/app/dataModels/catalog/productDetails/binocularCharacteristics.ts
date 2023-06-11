import { BinocularPurpose } from "../../enums/binocuclar/binocularPurpose";
import { FocusingMethod } from "../../enums/binocuclar/focusingMethod";
import { OpticsMaterial } from "../../enums/binocuclar/opticsMaterial";
import { PrismType } from "../../enums/binocuclar/prismType";

export interface BinocularCharacteristics {
    aperture?: number;
    exitPupilDiameterMax?: number;
    exitPupilDiameterMin?: number;
    focusingMethod?: FocusingMethod;
    fovMin?: number;
    fovMax?: number;
    hasAdapter?: string;
    hasCase?: string;
    hasMoistureProtection?: string;
    interpupillaryDistanseMin?: number;
    interpupillaryDistanseMax?: number;
    focusDistanceMin?: number;
    opticsMaterial?: OpticsMaterial;
    prismType?: PrismType;
    binocularPurpose?: BinocularPurpose;
    relativeBrightnessMin?: number;
    relativeBrightnessMax?: number;
    removalExitPupilMin?: number;
    removalExitPupilMax?: number;
    scaleMin?: number;
    scaleMax?: number;
    weight?: number;
}