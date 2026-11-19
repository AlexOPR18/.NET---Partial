using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Infrastructure.Persistence.Configurations;

public sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");
        builder.HasKey(booking => booking.Id);
        builder.Property(booking => booking.Id).ValueGeneratedNever();

        builder.HasOne<Room>()
            .WithMany()
            .HasForeignKey(booking => booking.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(booking => booking.BookedBy).IsRequired().HasMaxLength(Booking.MaxBookedByLength);
        builder.Property(booking => booking.Attendees).IsRequired();
        builder.Property(booking => booking.Status).HasConversion<string>().HasMaxLength(20);
        builder.Ignore(booking => booking.IsActive);

        builder.ComplexProperty(booking => booking.Period, period =>
        {
            period.Property(range => range.StartUtc)
                .HasColumnName("StartUtc")
                .HasConversion<UtcDateTimeConverter>();
            period.Property(range => range.EndUtc)
                .HasColumnName("EndUtc")
                .HasConversion<UtcDateTimeConverter>();
        });

        builder.HasIndex(booking => new { booking.RoomId, booking.Status });
    }
}
