using BidwotaGiri_Dot_Net_Assignment.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BidwotaGiri_Dot_Net_Assignment.Areas.Identity.Data
{
    public class BidwotaGiri_Dot_Net_AssignmentContext : IdentityDbContext<BidwotaGiri_Dot_Net_AssignmentUser>
    {
        public BidwotaGiri_Dot_Net_AssignmentContext(DbContextOptions<BidwotaGiri_Dot_Net_AssignmentContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Apply additional entity configurations
            builder.ApplyConfiguration(new BidwotaGiri_Dot_Net_AssignmentUserEntityConfiguration());
        }

    }

    public class BidwotaGiri_Dot_Net_AssignmentUserEntityConfiguration : IEntityTypeConfiguration<BidwotaGiri_Dot_Net_AssignmentUser>
    {
        public void Configure(EntityTypeBuilder<BidwotaGiri_Dot_Net_AssignmentUser> builder)
        {
            // Ensure columns use the correct data type
            builder.Property(x => x.FirstName)
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)"); // Explicitly set SQL column type

            builder.Property(x => x.LastName)
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)");
        }
    }
}
