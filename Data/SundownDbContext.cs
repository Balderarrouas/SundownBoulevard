using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SundownBoulevard.Data.Config;
using SundownBoulevard.Entities;

namespace SundownBoulevard.Data

{
    public class SundownDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        
        
        public SundownDbContext(DbContextOptions<SundownDbContext> options) : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new BookingConfig());
            base.OnModelCreating(builder);
        }
        
        private static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseLoggerFactory(MyLoggerFactory);
            builder.EnableSensitiveDataLogging();
        }
        
    }
}