using EcoActive.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoActive.DAL.Data
{
    public class ActivistConfiguration : IEntityTypeConfiguration<Activist>
    {
        public void Configure(EntityTypeBuilder<Activist> builder)
        {
            builder.HasOne(x => x.User)
                .WithOne(x => x.Activist)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Factories)
                .WithOne(x => x.Activist)
                .HasForeignKey(x => x.ActivistId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
