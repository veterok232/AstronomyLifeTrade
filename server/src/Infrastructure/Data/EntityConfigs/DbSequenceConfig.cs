using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.EntityConfigs;

public static class DbSequenceConfig
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<int>(DbConfigConstants.Sequences.OrderNumberSequence)
            .StartsAt(10000)
            .IncrementsBy(1);

        modelBuilder.HasSequence<int>(DbConfigConstants.Sequences.ReserveRequestNumberSequence)
            .StartsAt(10000)
            .IncrementsBy(1);

        modelBuilder.HasSequence<int>(DbConfigConstants.Sequences.PaperworkOrderNumberSequence)
            .StartsAt(1)
            .IncrementsBy(1);
    }
}