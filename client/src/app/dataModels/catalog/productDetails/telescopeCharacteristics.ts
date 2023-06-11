import { MountingType } from "../../enums/telescope/mountingType";
import { TelescopeControlType } from "../../enums/telescope/telescopeControlType";
import { TelescopeType } from "../../enums/telescope/telescopeType";
import { TelescopeUserLevel } from "../../enums/telescope/telescopeUserLevel";
import { TelescopeEyepiece } from "../telescopeEyepiece";

export interface TelescopeCharacteristics {
    aperture?: number;
    apertureRatio?: number;
    eyepieceFittingDiameter?: number;
    focusDistance?: number;
    maxUsefulScale?: number;
    minUsefulScale?: number;
    mountingType?: MountingType;
    telescopeControlType?: TelescopeControlType;
    scaleMax?: number;
    scaleMin?: number;
    seeker?: string;
    tripodHeight?: string;
    tripodMaterial?: string;
    type?: TelescopeType;
    telescopeUserLevel?: TelescopeUserLevel;
    weight?: number;
    telescopeEyepieces?: TelescopeEyepiece[];
}