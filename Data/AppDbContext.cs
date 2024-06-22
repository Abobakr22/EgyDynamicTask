
using EgyDynamicTask.Models;
using Microsoft.EntityFrameworkCore;

namespace EgyDynamicTask.Data;

public partial class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Call> Calls { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<SalesMan> SalesMen { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=EgyDynamic;Trusted_Connection=True;TrustServerCertificate=True;"
    //        );

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Call>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Calls__3213E83F978C13B7");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CallDate)
                .HasColumnType("datetime")
                .HasColumnName("call_date");
            entity.Property(e => e.CallTitle)
                .HasMaxLength(100)
                .HasColumnName("call_title");
            entity.Property(e => e.CallType)
                .HasMaxLength(100)
                .HasColumnName("call_type");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");
            entity.Property(e => e.EnteredBy)
                .HasMaxLength(100)
                .HasColumnName("entered_by");
            entity.Property(e => e.EntryDate)
                .HasColumnType("datetime")
                .HasColumnName("entry_date");
            entity.Property(e => e.IsCompleted).HasColumnName("is_completed");
            entity.Property(e => e.IsIncoming).HasColumnName("is_incoming");
            entity.Property(e => e.LastModifiedBy)
                .HasMaxLength(100)
                .HasColumnName("last_modified_by");
            entity.Property(e => e.LastModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("last_modified_date");
            entity.Property(e => e.ProjectId).HasColumnName("Project_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Calls)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Calls__Customer___403A8C7D");
                //.OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(d => d.Employee).WithMany(p => p.Calls)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Calls__Employee___4222D4EF");

            entity.HasOne(d => d.Project).WithMany(p => p.Calls)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__Calls__Project_i__412EB0B6");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3213E83F6B5848B5");

            entity.ToTable("Customer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerAddress)
                .HasMaxLength(255)
                .HasColumnName("customer_address");
            entity.Property(e => e.CustomerClassification)
                .HasMaxLength(100)
                .HasColumnName("customer_classification");
            entity.Property(e => e.CustomerCode)
                .HasMaxLength(50)
                .HasColumnName("customer_code");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .HasColumnName("Customer_name");
            entity.Property(e => e.CustomerSource)
                .HasMaxLength(100)
                .HasColumnName("customer_source");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.EnteredBy)
                .HasMaxLength(100)
                .HasColumnName("entered_by");
            entity.Property(e => e.EntryDate)
                .HasColumnType("datetime")
                .HasColumnName("entry_date");
            entity.Property(e => e.Job)
                .HasMaxLength(100)
                .HasColumnName("job");
            entity.Property(e => e.LastModifiedBy)
                .HasMaxLength(100)
                .HasColumnName("last_modified_by");
            entity.Property(e => e.LastModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("last_modified_date");
            entity.Property(e => e.Nationality)
                .HasMaxLength(50)
                .HasColumnName("nationality");
            entity.Property(e => e.Phone1)
                .HasMaxLength(15)
                .HasColumnName("phone1");
            entity.Property(e => e.Phone2)
                .HasMaxLength(15)
                .HasColumnName("phone2");
            entity.Property(e => e.Residence)
                .HasMaxLength(100)
                .HasColumnName("residence");
            entity.Property(e => e.SalesManId).HasColumnName("SalesMan_id");
            entity.Property(e => e.Whatsapp)
                .HasMaxLength(15)
                .HasColumnName("whatsapp");

            entity.HasOne(d => d.SalesMan).WithMany(p => p.Customers)
                .HasForeignKey(d => d.SalesManId)
                .HasConstraintName("FK__Customer__SalesM__3D5E1FD2");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC27D20D4495");

            entity.ToTable("Employee");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Project__3214EC270DE6F180");

            entity.ToTable("Project");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<SalesMan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SalesMan__3214EC27F17D0147");

            entity.ToTable("SalesMan");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
