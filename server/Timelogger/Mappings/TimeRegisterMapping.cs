using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timelogger.Domain.Entities;

namespace Timelogger.Data.Mappings
{
    public class TimeRegisterMapping : IEntityTypeConfiguration<TimeRegister>
    {
        public void Configure(EntityTypeBuilder<TimeRegister> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.ProjectId).IsRequired();
            builder.Property(s => s.Start).IsRequired();
            builder.Property(s => s.End).IsRequired();

            builder.HasOne(s => s.Project)
                .WithMany(t => t.TimeRegisters)
                .HasForeignKey(s => s.ProjectId);
        }
    }
}
