using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure.DbContext;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

         // change names of tables that identity make it 
            modelBuilder.Entity<ApplicationUser>(e => e.ToTable("Users"));
            modelBuilder.Entity<IdentityRole>(e => e.ToTable("Roles"));
            modelBuilder.Entity<IdentityUserRole<string>>(e => e.ToTable("UserRoles"));
            modelBuilder.Entity<IdentityUserClaim<string>>(e => e.ToTable("UserClaims"));
            modelBuilder.Entity<IdentityUserLogin<string>>(e => e.ToTable("UserLogins"));
            modelBuilder.Entity<IdentityRoleClaim<string>>(e => e.ToTable("RoleCliams"));
            modelBuilder.Entity<IdentityUserToken<string>>(e => e.ToTable("UserTokens"));
            
            
            // TPT (Table Per Type) Configuration: Student and Teacher will have their own tables
            modelBuilder.Entity<Student>()
                .ToTable("Student");  
            
            modelBuilder.Entity<Teacher>()
                .ToTable("Teacher");
    }
    public DbSet<Parent> Parents { get; set; }
    public DbSet<UploadedFile> UploadedFiles {get; set;}
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<ClassRoom> ClassRooms { get; set; }
    public DbSet<Homeworke> Homeworkes { get; set; }
}