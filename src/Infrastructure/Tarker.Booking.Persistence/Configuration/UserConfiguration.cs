using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tarker.Booking.Domain.Entities.User;

namespace Tarker.Booking.Persistence.Configuration;

public class UserConfiguration
{
    public UserConfiguration(EntityTypeBuilder<UserEntity> entityBuilder)
    {
        entityBuilder.HasKey(e => e.UserId);
        entityBuilder.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
        entityBuilder.Property(e => e.LastName).IsRequired().HasMaxLength(50);
        entityBuilder.Property(e => e.UserName).IsRequired().HasMaxLength(50);
        entityBuilder.Property(e => e.Password).IsRequired().HasMaxLength(50);
        entityBuilder.HasMany(e => e.Bookings)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId);
    }
}
