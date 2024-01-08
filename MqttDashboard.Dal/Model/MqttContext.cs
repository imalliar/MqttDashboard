using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MqttDashboard.Dal.Identity;

namespace MqttDashboard.Dal.Model;

public partial class MqttContext(DbContextOptions<MqttContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, int>(options)
{
    public DbSet<Locale> Locales => Set<Locale>();
    public DbSet<Application> Applications => Set<Application>();
    public DbSet<Site> Sites => Set<Site>();
    public DbSet<Sensor> Sensors => Set<Sensor>();
    public DbSet<AlarmStatus> AlarmStatuses => Set<AlarmStatus>();
    public DbSet<AlarmEvent> AlarmEvents => Set<AlarmEvent>();
    public DbSet<SensorStatus> SensorStatuses => Set<SensorStatus>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<BaseEntity>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DateAdded).IsRequired();
            entity.HasIndex(e => e.DateAdded).IsDescending();
            entity.Property(f => f.DateAdded).HasDefaultValueSql("GETDATE()");
        });

        builder.Entity<BaseEntity>().UseTpcMappingStrategy();

        builder.Entity<Locale>(entity =>
        {
            entity.ToTable("Locale");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(250);
            entity.HasIndex(e => e.Name);
        });

        builder.Entity<Application>(entity =>
        {
            entity.ToTable("Application");
            entity.Property(f => f.Name).IsRequired().HasMaxLength(250);
            entity.Property(f => f.Description).HasMaxLength(1000);
            entity.HasIndex(f => f.Name);
            entity.HasMany(f => f.Sites).WithOne(f => f.Application)
                .HasForeignKey(f => f.ApplicationId).HasPrincipalKey(f => f.Id);
        });

        builder.Entity<Site>(entity =>
        {
            entity.ToTable("Site");
            entity.Property(f => f.Name).HasMaxLength(250).IsRequired();
            entity.Property(f => f.Description).HasMaxLength(1000);
            entity.HasIndex(f => f.Name);
            entity.HasMany(f => f.Sensors).WithOne(f => f.Site)
                .HasForeignKey(f => f.SiteId).HasPrincipalKey(f => f.Id).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<AlarmStatus>(entity =>
        {
            entity.ToTable("AlarmStatus");
            entity.HasIndex(f => f.Name);
            entity.Property(f => f.Name).HasMaxLength(100).IsRequired();
            entity.HasMany(f => f.AlarmEvents).WithOne(f => f.AlarmStatus)
                .HasForeignKey(f => f.AlarmStatusId).HasPrincipalKey(f => f.Id);
        });

        builder.Entity<AlarmEvent>(entity =>
        {
            entity.ToTable("AlarmEvent");
            entity.HasIndex(f => f.TimeOccurred).IsDescending();
        });

        builder.Entity<Sensor>(entity =>
        {
            entity.ToTable("Sensor");
            entity.Property(f => f.Name).HasMaxLength(250).IsRequired();
            entity.Property(f => f.Description).HasMaxLength(1000);
            entity.HasMany(f => f.AlarmEvents).WithOne(f => f.Sensor)
                .HasForeignKey(f => f.SensorId).HasPrincipalKey(f => f.Id);
        });

        builder.Entity<SensorStatus>(entity =>
        {
            entity.ToTable("SensorStatus");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DateAdded).IsRequired();
            entity.HasIndex(e => e.DateAdded).IsDescending();
            entity.HasOne(f => f.Sensor).WithOne(f => f.SensorStatus)
                .HasForeignKey<SensorStatus>(f => f.Id).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(f => f.AlarmEvent).WithOne(f => f.SensorStatus)
                .HasForeignKey<SensorStatus>(f => f.EventId).HasPrincipalKey<AlarmEvent>(f => f.Id);
        });

        base.OnModelCreating(builder);
        OnModelCreatingPartial(builder);
    }
}