using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infraestructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(e => e.UserId);

            builder.Property(e => e.UserId)
           .HasColumnName("IdUsuario");

            builder.Property(e => e.FirstName)
            .HasColumnName("Nombre")
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.LastName)
                .HasColumnName("Apellidos")
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Email)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.DateOfBirth)
            .HasColumnName("FechaNacimiento")
            .HasColumnType("date");

            

            builder.Property(e => e.Thelephone)
            .HasColumnName("Telefono")
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.IsActive)
            .HasColumnName("Activo")
            .ValueGeneratedNever();
        }
    }
}
