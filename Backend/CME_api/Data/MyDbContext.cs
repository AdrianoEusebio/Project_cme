using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    { }
    public DbSet<User> Users { get; set; }

    public DbSet<User_Group> User_Groups { get; set; }
}