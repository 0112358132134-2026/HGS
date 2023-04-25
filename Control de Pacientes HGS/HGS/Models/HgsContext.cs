using Microsoft.EntityFrameworkCore;

namespace HGS.Models;

public partial class HgsContext : DbContext
{
    public HgsContext()
    {
    }

    public HgsContext(DbContextOptions<HgsContext> options) : base(options)
    {
    }

    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Areasucursal> Areasucursals { get; set; }

    public virtual DbSet<Bed> Beds { get; set; }

    public virtual DbSet<Bedpatient> Bedpatients { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Speciality> Specialities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();
            var connectionString = configuration.GetConnectionString("hgs");

            if (connectionString != null)
            {
                optionsBuilder.UseMySQL(connectionString);
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("administrator");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .HasColumnName("password");
            entity.Property(e => e.User)
                .HasMaxLength(30)
                .HasColumnName("user");
        });

        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("area");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Areasucursal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("areasucursal");

            entity.HasIndex(e => e.AreaId, "area_id");

            entity.HasIndex(e => e.BranchId, "branch_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AreaId).HasColumnName("area_id");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");

            entity.HasOne(d => d.Area).WithMany(p => p.Areasucursals)
                .HasForeignKey(d => d.AreaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("areasucursal_ibfk_1");

            entity.HasOne(d => d.Branch).WithMany(p => p.Areasucursals)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("areasucursal_ibfk_2");
        });

        modelBuilder.Entity<Bed>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("bed");

            entity.HasIndex(e => e.AreaSucursalId, "areaSucursal_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Annotations)
                .HasMaxLength(255)
                .HasColumnName("annotations");
            entity.Property(e => e.AreaSucursalId).HasColumnName("areaSucursal_id");
            entity.Property(e => e.Size)
                .HasMaxLength(15)
                .HasColumnName("size");
            entity.Property(e => e.State).HasColumnName("state");

            entity.HasOne(d => d.AreaSucursal).WithMany(p => p.Beds)
                .HasForeignKey(d => d.AreaSucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bed_ibfk_1");
        });

        modelBuilder.Entity<Bedpatient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("bedpatient");

            entity.HasIndex(e => e.BedId, "bed_id");

            entity.HasIndex(e => e.DoctorId, "doctor_id");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Annotations)
                .HasColumnType("text")
                .HasColumnName("annotations");
            entity.Property(e => e.BedId).HasColumnName("bed_id");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("endDate");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Reason)
                .HasColumnType("text")
                .HasColumnName("reason");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("startDate");
            entity.Property(e => e.State).HasColumnName("state");

            entity.HasOne(d => d.Bed).WithMany(p => p.Bedpatients)
                .HasForeignKey(d => d.BedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bedpatient_ibfk_1");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Bedpatients)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bedpatient_ibfk_3");

            entity.HasOne(d => d.Patient).WithMany(p => p.Bedpatients)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bedpatient_ibfk_2");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("branch");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Municipality)
                .HasMaxLength(100)
                .HasColumnName("municipality");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("doctor");

            entity.HasIndex(e => e.SpecialtyId, "specialty_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthdate)
                .HasColumnType("date")
                .HasColumnName("birthdate");
            entity.Property(e => e.CollegiateNumber)
                .HasMaxLength(9)
                .HasColumnName("collegiateNumber");
            entity.Property(e => e.Dpi)
                .HasMaxLength(13)
                .HasColumnName("dpi");
            entity.Property(e => e.Lastname)
                .HasMaxLength(20)
                .HasColumnName("lastname");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .HasColumnName("password");
            entity.Property(e => e.SpecialtyId).HasColumnName("specialty_id");
            entity.Property(e => e.User)
                .HasMaxLength(30)
                .HasColumnName("user");

            entity.HasOne(d => d.Specialty).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.SpecialtyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doctor_ibfk_1");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("patient");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthdate)
                .HasColumnType("date")
                .HasColumnName("birthdate");
            entity.Property(e => e.Dpi)
                .HasMaxLength(13)
                .HasColumnName("dpi");
            entity.Property(e => e.Lastname)
                .HasMaxLength(20)
                .HasColumnName("lastname");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.Observations)
                .HasColumnType("text")
                .HasColumnName("observations");
        });

        modelBuilder.Entity<Speciality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("speciality");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<HGSModel.Patient> Patient { get; set; } = default!;

    public DbSet<HGSModel.Branch> Branch { get; set; } = default!;

    public DbSet<HGSModel.Area> Area { get; set; } = default!;

    public DbSet<HGSModel.Speciality> Speciality { get; set; } = default!;

    public DbSet<HGSModel.Doctor> Doctor { get; set; } = default!;

    public DbSet<HGSModel.Areasucursal> Areasucursal { get; set; } = default!;
}