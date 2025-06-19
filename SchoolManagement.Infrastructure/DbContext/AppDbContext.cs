using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure.DbContext;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions options) : base(options)
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
        
        modelBuilder.Entity<Admin>()
            .ToTable("Admin");

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Author)
            .WithMany(auth => auth.Posts)
            .HasForeignKey(p => p.AuthorId)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Author)
            .WithMany(auth => auth.Comments)
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.ClientCascade);
        
        // Configure the many-to-many relationship
        modelBuilder.Entity<StudentClassRoom>()
            .HasKey(sc => new { sc.StudentId, sc.ClassRoomId }); // Composite primary key

        modelBuilder.Entity<StudentClassRoom>()
            .HasOne(sc => sc.Student)
            .WithMany(s => s.StudentClassRooms)
            .HasForeignKey(sc => sc.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<StudentClassRoom>()
            .HasOne(sc => sc.ClassRoom)
            .WithMany(c => c.StudentClassRooms)
            .HasForeignKey(sc => sc.ClassRoomId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    public DbSet<Parent> Parents { get; set; }
    public DbSet<UploadedFile> UploadedFiles { get; set; }
    public DbSet<Lesson?> Lessons { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<ClassRoom> ClassRooms { get; set; }
    public DbSet<HomeWork> HomeWorks { get; set; }
    public DbSet<ResetCode> ResetCodes { get; set; }
    public DbSet<StudentClassRoom> StudentClassRooms { get; set; }
    public DbSet<HomeWorkSubmission?> HomeWorkSubmissions { get; set; }
}