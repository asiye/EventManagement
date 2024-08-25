using EventManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply configurations for all entities that inherit from BaseEntity
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    // Automatically register all DbSet properties for entities inheriting from BaseEntity
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (type.IsClass && !type.IsAbstract && typeof(BaseEntity).IsAssignableFrom(type))
            {
                // Use reflection to create DbSet properties dynamically
                var method = typeof(ModelBuilder).GetMethod("Entity", new Type[] { });
                method = method?.MakeGenericMethod(type);
                method?.Invoke(this, null);
            }
        }

        base.OnConfiguring(optionsBuilder);
    }
}
