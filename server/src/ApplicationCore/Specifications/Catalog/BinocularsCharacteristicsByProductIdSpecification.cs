using ApplicationCore.Entities;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Catalog;

public class BinocularsCharacteristicsByProductIdSpecification : DataTransformSpecification<Binocular, BinocularCharacteristics>
{
    public BinocularsCharacteristicsByProductIdSpecification(Guid productId)
        : base(
            b => new BinocularCharacteristics
            {
                Aperture = b.Aperture,
                ExitPupilDiameterMax = b.ExitPupilDiameterMax,
                ExitPupilDiameterMin = b.ExitPupilDiameterMin,
                FocusingMethod = b.FocusingMethod,
                FovMin = b.FovMin,
                FovMax = b.FovMax,
                HasAdapter = b.HasAdapter,
                HasCase = b.HasCase,
                HasMoistureProtection = b.HasMoistureProtection,
                InterpupillaryDistanseMin = b.InterpupillaryDistanseMin,
                InterpupillaryDistanseMax = b.InterpupillaryDistanseMax,
                FocusDistanceMin = b.FocusDistanceMin,
                OpticsMaterial = b.OpticsMaterial,
                PrismType = b.PrismType,
                BinocularPurpose = b.Purpose,
                RelativeBrightnessMin = b.RelativeBrightnessMin,
                RelativeBrightnessMax = b.RelativeBrightnessMax,
                RemovalExitPupilMin = b.RemovalExitPupilMin,
                RemovalExitPupilMax = b.RemovalExitPupilMax,
                ScaleMin = b.ScaleMin,
                ScaleMax = b.ScaleMax,
                Weight = b.Weight
            },
            b => b.ProductId == productId)
    {
    }
}