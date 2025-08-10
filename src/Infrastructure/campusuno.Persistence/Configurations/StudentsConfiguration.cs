using campusuno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace campusuno.Persistence.Configurations
{
    public class StudentsConfiguration : IEntityTypeConfiguration<Students>
    {
        public void Configure(EntityTypeBuilder<Students> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn().IsRequired();
            builder.Property(x => x.CreateAt).IsRequired().HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.CreateBy).IsRequired(false).HasMaxLength(25);
            builder.Property(x => x.UpdateAt).IsRequired(false).HasColumnType("DATETIME");
            builder.Property(x => x.UpdateBy).IsRequired(false).HasMaxLength(25);
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(2048);
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PhoneNumber).IsRequired(false).HasMaxLength(15);
            builder.Property(x => x.Address).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.DateOfBirth).IsRequired(false).HasColumnType("DATETIME");
        }
    }
}
