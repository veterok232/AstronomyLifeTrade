import { EntityModel } from "./entityModel";
import { HasVersion } from "./hasVersion";

export interface VersionedEntity extends EntityModel, HasVersion {
}