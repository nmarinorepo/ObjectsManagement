using Microsoft.EntityFrameworkCore;
using ObjectsManagement.Domain.Entities;

namespace ObjectsManagement.Persistence.Context
{
    public class ObjectsContext : DbContext
    {
        public ObjectsContext(DbContextOptions<ObjectsContext> options) : base(options)
        {

        }

        public DbSet<ObjectMainEntity> ObjectsMain { get; set; }

        public DbSet<ObjectRelationshipEntity> ObjectRelationships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ObjectMainEntity>()
                .HasMany(e => e.ObjectRelationships)
                .WithOne(e => e.ObjectMain)
                .HasForeignKey(e => e.ObjectMainId)
                .IsRequired(false);
        }
    }
}
