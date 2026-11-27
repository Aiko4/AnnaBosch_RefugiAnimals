using AnnaBosch_RefugiAnimals.AnnaBosch_RefugiAnimals.Model;
using Microsoft.EntityFrameworkCore;

namespace Model;

public class RefugiContext : DbContext
{
    public RefugiContext() { }

    public DbSet<Animal> Animal { get; set; }
    public DbSet<Xip> Xip { get; set; }
    public DbSet<Adoptant> Adoptant { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlite("Data Source=RefugiAnimals.db").UseLazyLoadingProxies(); 
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>()
            .HasOne(c => c.Xip)
            .WithOne(p => p.Animal)
            .HasForeignKey<Animal>("XipID")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Animal>()
            .HasOne(m => m.Adoptant)
            .WithMany(p => p.Animals)
            .HasForeignKey("AdoptantID")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}