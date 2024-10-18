using Microsoft.EntityFrameworkCore;

namespace CMCS.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<SupportingDocument> SupportingDocuments { get; set; }
        public DbSet<Approval> Approvals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SupportingDocument>()
                .HasKey(sd => sd.SupportingDocumentID);

            modelBuilder.Entity<SupportingDocument>()
                .HasOne(sd => sd.Claim)
                .WithMany(c => c.SupportingDocuments)
                .HasForeignKey(sd => sd.ClaimID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
