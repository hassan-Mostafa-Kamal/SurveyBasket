using Microsoft.EntityFrameworkCore;

namespace SurveyBasket.Api.persistence.EntitiesConfigurations
{
    public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(100);
            builder.Property(x => x.LastName).HasMaxLength(100);
            builder.OwnsMany(x => x.RefreshTokens)
                    .ToTable("RefreshTokens")
                    .WithOwner()
                    .HasForeignKey("UserId");

        }
    }
}
