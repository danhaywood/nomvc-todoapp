using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Infrastructure;

namespace Domain
{
    public class ToDoItemMapping : EntityTypeConfiguration<ToDoItem>
    {
        public ToDoItemMapping()
        {
            this.HasKey(t => t.Id);        
            this.ToTable("ToDoItem", "ToDo");

            this.Property(t => t.Id);
            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(30);
            this.Property(t => t.Category)
                .IsRequired();
            this.Property(t => t.Complete)
                .IsRequired();
            this.Property(t => t.DueBy)
                .IsOptional()
                .HasColumnType("datetime2");
            this.Property(t => t.Notes)
                .IsOptional()
                .HasMaxLength(2000);
            this.Property(t => t.Cost)
                .IsOptional();
        }
    }
}