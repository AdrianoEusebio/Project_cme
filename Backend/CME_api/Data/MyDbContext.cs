using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<Receiving> Receivings { get; set; }
    public DbSet<Distribution> Distributions { get; set; }
    public DbSet<Washing> Washings { get; set; }
    public DbSet<ProcessHistory> ProcessHistories { get; set; }
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserGroup>()
            .Property(ug => ug.IdUserGroup)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<User>()
            .HasOne(u => u.UserGroup)
            .WithMany(g => g.Users)
            .HasForeignKey(u => u.IdGroup)
            .HasConstraintName("FK_Users_UserGroups_IdGroup");

        modelBuilder.Entity<Material>()
            .Property(m => m.Status)
            .HasConversion<string>();

        modelBuilder.Entity<ProcessHistory>()
        .Property(p => p.EnumStatus)
        .HasConversion<string>();

        modelBuilder.Entity<Receiving>()
            .HasIndex(r => r.SerialMaterial);

        modelBuilder.Entity<Washing>()
            .HasIndex(w => w.SerialMaterial);

        modelBuilder.Entity<Distribution>()
            .HasIndex(w => w.SerialMaterial);

    }
}