using FinalPratic2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalPratic2.Configuration
{
    public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
            builder.ToTable(opt =>
            {
                opt.HasCheckConstraint("CK_Trainers_Experience", "[Experience]>3");
            });
            builder.Property(x => x.ImagePath).IsRequired().HasMaxLength(1024);
            builder.HasOne(x => x.Category).WithMany(x => x.Trainers).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
