using FinalPratic2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FinalPratic2.Context;

public class AppDbContext : IdentityDbContext<AppUser>
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Trainer> Trainers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> option):base(option) { }


}
