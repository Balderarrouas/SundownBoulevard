using Microsoft.EntityFrameworkCore;
using SundownBoulevard.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace SundownBoulevard.Data.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(user => user.Email).IsUnique();
            builder.HasKey(user => user.UserId);
            builder.HasQueryFilter(x => x.DeletedAt == null);
        }
    }
}