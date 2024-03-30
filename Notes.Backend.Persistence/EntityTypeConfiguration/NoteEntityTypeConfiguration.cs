using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Backend.Domain.Entities;

namespace Notes.Backend.Persistence.EntityTypeConfiguration
{
    public class NoteEntityTypeConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Title).IsRequired();
            builder.Property(e => e.Content).IsRequired();
            builder.Property(e => e.ModifiedTime).IsRequired();

            builder.HasOne(e => e.User).WithMany(u => u.Notes)
                .HasForeignKey(e => e.UserId);
        }
    }
}
