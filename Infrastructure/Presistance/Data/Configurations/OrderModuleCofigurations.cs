using Order = DomainLayer.Models.OrderModule.Order;

namespace Presistence.Data.Configurations
{
    class OrderModuleCofigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.Property(O => O.SubTotal)
                .HasColumnType("decimal(8,2)");
            builder.HasMany(O => O.Items)
                .WithOne();
            builder.HasOne(O => O.DeliveryMethod)
                .WithMany()
                .HasForeignKey(O => O.DelivreyMethodId);
            builder.OwnsOne(O => O.Address);

                
        }
    }
}
