using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EF
{
    public sealed class ReportsDbContext : DbContext
    {
        public ReportsDbContext(DbContextOptions<ReportsDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Employee>().HasKey(e => e.Id);
            modelBuilder.Entity<Employee>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Employee>().Property(e => e.Email).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Employee>().Property(e => e.Password).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Employee>().Property(e => e.Name).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Employee>().Property(e => e.HeadId);
            modelBuilder.Entity<Employee>().HasOne(e => e.Report)
                .WithOne(r => r.Employee).HasForeignKey<Report>(r => r.EmployeeId);
            modelBuilder.Entity<Employee>().HasMany(e => e.Subordinates)
                .WithOne(e => e.Head)
                .HasForeignKey(e => e.HeadId);
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Problems)
                .WithOne(p => p.Employee)
                .HasForeignKey(p => p.EmployeeId);

            modelBuilder.Entity<Problem>().ToTable("Problems");
            modelBuilder.Entity<Problem>().HasKey(p => p.ProblemId);
            modelBuilder.Entity<Problem>().Property(p => p.ProblemId).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Problem>().Property(p => p.Description).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Problem>().Property(p => p.ChangeTime);
            modelBuilder.Entity<Problem>().Property(p => p.CreationTime).IsRequired();
            modelBuilder.Entity<Problem>().Property(p => p.EProblemState).IsRequired();
            modelBuilder.Entity<Problem>().HasMany(p => p.Comments)
                .WithOne(c => c.Problem)
                .HasForeignKey(c => c.ProblemId);

            modelBuilder.Entity<Report>().ToTable("Reports");
            modelBuilder.Entity<Report>().HasKey(r => r.Id);
            modelBuilder.Entity<Report>().Property(r => r.Description).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<Report>().HasMany(r => r.Problems)
                .WithOne(p => p.Report)
                .HasForeignKey(p => p.ReportId);

            modelBuilder.Entity<Comment>().ToTable("Comments");
            modelBuilder.Entity<Comment>().HasKey(p => p.Id);
            modelBuilder.Entity<Comment>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Comment>().Property(p => p.CommentBody).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Comment>().Property(p => p.CreationTime).IsRequired();

            modelBuilder.Entity<EmployeeRole>().ToTable("EmployeeRoles");
            modelBuilder.Entity<EmployeeRole>().HasKey(e => new { e.EmployeeId, e.RoleId });
        }
    }
}