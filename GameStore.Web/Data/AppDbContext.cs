using Microsoft.EntityFrameworkCore;
using GameStore.Web.Data.Entities;

namespace GameStore.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<Category> Categories { get; set; }
}