using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SundownBoulevard.Entities;

namespace SundownBoulevard.Data.Config
{
   
    
        public class BookingConfig : IEntityTypeConfiguration<Booking>
        {
            public void Configure(EntityTypeBuilder<Booking> builder)
            {
                builder.HasKey(booking => booking.BookingId);
                builder.HasQueryFilter(x => x.DeletedAt == null);
            }
        }
    
}