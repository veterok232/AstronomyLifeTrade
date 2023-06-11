using ApplicationCore.Entities;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Catalog;

public class TelescopesCharacteristicsByProductIdSpecification : DataTransformSpecification<Telescope, TelescopeCharacteristics>
{
    public TelescopesCharacteristicsByProductIdSpecification(Guid productId)
        : base(
            t => new TelescopeCharacteristics
            {
                Aperture = t.Aperture,
                ApertureRatio = t.ApertureRatio,
                EyepieceFittingDiameter = t.EyepieceFittingDiameter,
                FocusDistance = t.FocusDistance,
                MaxUsefulScale = t.MaxUsefulScale,
                MinUsefulScale = t.MinUsefulScale,
                MountingType = t.MountingType,
                TelescopeControlType = t.TelescopeControlType,
                ScaleMax = t.ScaleMax,
                ScaleMin = t.ScaleMin,
                Seeker = t.Seeker,
                TripodHeight = t.TripodHeight,
                TripodMaterial = t.TripodMaterial,
                Type = t.Type,
                TelescopeUserLevel = t.TelescopeUserLevel,
                Weight = t.Weight,
                TelescopeEyepieces = t.TelescopeEyepieces,
            },
            t => t.ProductId == productId)
    {
    }
}