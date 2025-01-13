using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimulationTask.Models;

namespace SimulationTask.DAL.Confiuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(12);

            builder.HasOne(u => u.Position)
                .WithMany(p => p.Users)
                .HasForeignKey(f => f.positionId);
        }
    }
}
