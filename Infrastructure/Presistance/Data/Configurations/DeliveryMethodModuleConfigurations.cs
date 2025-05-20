

namespace Presistence.Data.Configurations
{
    public class DeliveryMethodModuleConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("DeliveryMethods");
            builder.Property(DM => DM.Price)
                .HasColumnType("decimal(8,2)");
            builder.Property(DM => DM.Description)
                .HasColumnType("varchar(100)");
            builder.Property(DM => DM.ShortName)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(DM => DM.DeliveryTime)
                .HasColumnType("varchar")
                .HasMaxLength(50);
        }
    }
}
