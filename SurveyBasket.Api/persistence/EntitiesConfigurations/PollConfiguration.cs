
namespace SurveyBasket.Api.persistence.EntitiesConfigurations
{
    public class PollConfiguration : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.HasIndex(p=>p.Titel).IsUnique();
            builder.Property(p => p.Titel).HasMaxLength(100);
            builder.Property(p => p.Summary).HasMaxLength(1500);
        }
    }
}
