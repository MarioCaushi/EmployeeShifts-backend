using Microsoft.EntityFrameworkCore;
using EmployeeShift_backend.Models;

namespace EmployeeShift_backend.Data
{
    public class EmployeeShiftDbContext : DbContext
    {
        public EmployeeShiftDbContext() { }

        public EmployeeShiftDbContext(DbContextOptions<EmployeeShiftDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureDatabaseSettings(modelBuilder);
            ConfigureEmployeeEntity(modelBuilder);
            ConfigureEventEntity(modelBuilder);
            ConfigureManagerEntity(modelBuilder);
            ConfigureShiftEntity(modelBuilder);

            OnModelCreatingPartial();
        }

        private void ConfigureDatabaseSettings(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");
        }

        private void ConfigureEmployeeEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

                entity.ToTable("Employee");
                entity.HasIndex(e => e.Email, "Email").IsUnique();
                entity.HasIndex(e => e.ManagerId, "ManagerID");
                entity.HasIndex(e => e.ShiftId, "ShiftID");

                entity.Property(e => e.EmployeeId).ValueGeneratedNever().HasColumnName("EmployeeID");
                entity.Property(e => e.Address).HasMaxLength(200);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("datetime");
                entity.Property(e => e.Email).HasMaxLength(64);
                entity.Property(e => e.LastName).HasMaxLength(64);
                entity.Property(e => e.ManagerId).HasColumnName("ManagerID");
                entity.Property(e => e.Name).HasMaxLength(64);
                entity.Property(e => e.PhoneNumber).HasMaxLength(64);
                entity.Property(e => e.Position).HasMaxLength(64);
                entity.Property(e => e.ShiftId).HasColumnName("ShiftID");
                entity.Property(e => e.Status).HasMaxLength(64);
                entity.Property(e => e.UpdatedAt).ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employee_ibfk_1");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ShiftId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employee_ibfk_2");
            });
        }

        private void ConfigureEventEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.EventId).HasName("PRIMARY");

                entity.ToTable("Event");
                entity.HasIndex(e => e.EmployeeId, "EmployeeID");

                entity.Property(e => e.EventId).HasColumnName("EventID");
                entity.Property(e => e.Description).HasColumnType("text");
                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
                entity.Property(e => e.EventDate).HasDefaultValueSql("curdate()");
                entity.Property(e => e.EventTime).HasDefaultValueSql("curtime()").HasColumnType("time");
                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("event_ibfk_1");
            });
        }

        private void ConfigureManagerEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manager>(entity =>
            {
                entity.HasKey(e => e.ManagerId).HasName("PRIMARY");

                entity.ToTable("Manager");
                entity.HasIndex(e => e.Email, "Email").IsUnique();
                entity.HasIndex(e => e.ManagerId, "ManagerID").IsUnique();
                entity.HasIndex(e => e.Username, "Username").IsUnique();

                entity.Property(e => e.ManagerId).HasColumnName("ManagerID");
                entity.Property(e => e.Address).HasMaxLength(200);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("datetime");
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.LastName).HasMaxLength(64);
                entity.Property(e => e.Name).HasMaxLength(64);
                entity.Property(e => e.Password).HasMaxLength(255);
                entity.Property(e => e.PhoneNumber).HasMaxLength(64);
                entity.Property(e => e.UpdatedAt).ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnType("datetime");
                entity.Property(e => e.Username).HasMaxLength(64);
            });
        }

        private void ConfigureShiftEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shift>(entity =>
            {
                entity.HasKey(e => e.ShiftId).HasName("PRIMARY");

                entity.ToTable("Shift");
                entity.Property(e => e.ShiftId).HasColumnName("ShiftID");
                entity.Property(e => e.ShiftDays).HasMaxLength(200);
                entity.Property(e => e.ShiftHours).HasMaxLength(200);
                entity.Property(e => e.ShiftType).HasMaxLength(64);
            });
        }

        private void OnModelCreatingPartial()
        {
        }
    }
}