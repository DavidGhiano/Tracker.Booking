using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tarker.Booking.Domain.Entities.Customer;

namespace Tarker.Booking.Persistence.Configuration;

public class CustomerConfiguration
{
    public CustomerConfiguration(EntityTypeBuilder<CustomerEntity> entityBuilder)
    {
        entityBuilder.HasKey(e => e.CustomerId);
        entityBuilder.Property(e => e.FullName).IsRequired().HasMaxLength(50);
        entityBuilder.Property(e => e.DocumentNumber).IsRequired().HasMaxLength(8);
        entityBuilder.HasMany(e => e.Bookings)
            .WithOne(e => e.Customer)
            .HasForeignKey(e => e.CustomerId);
    }

}
