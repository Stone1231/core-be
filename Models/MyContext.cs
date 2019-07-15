using Microsoft.EntityFrameworkCore;
namespace Backend.Models
{
    public class MyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Dept> Depts { get; set; }
        public DbSet<Proj> Projs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Filename=./db.sqlite3");
        }

        public MyContext(DbContextOptions<MyContext> options)
      : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserRelProj>()
                .HasKey(t => new { t.UserId, t.ProjId });

            builder.Entity<UserRelProj>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserRelProjs)
                .HasForeignKey(pt => pt.UserId);

            builder.Entity<UserRelProj>()
                .HasOne(pt => pt.Proj)
                .WithMany(t => t.UserRelProjs)
                .HasForeignKey(pt => pt.ProjId);

            base.OnModelCreating(builder);
        }
    }
}