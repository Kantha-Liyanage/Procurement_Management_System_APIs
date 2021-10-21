using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ProcurementManagementSystemServices.Models
{
    public partial class ProcurementManagmentContext : DbContext
    {
        public ProcurementManagmentContext()
        {
        }

        public ProcurementManagmentContext(DbContextOptions<ProcurementManagmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Depot> Depots { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<MaterialCategory> MaterialCategories { get; set; }
        public virtual DbSet<PurchaseRequisition> PurchaseRequisitions { get; set; }
        public virtual DbSet<PurchaseRequisitionItem> PurchaseRequisitionItems { get; set; }
        public virtual DbSet<Site> Sites { get; set; }
        public virtual DbSet<SiteBudget> SiteBudgets { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserSite> UserSites { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;uid=root;pwd=root;database=procurement_managment_system", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.6.4-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("address");

                entity.HasIndex(e => e.Country, "FK_address_country");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AddressLine1)
                    .HasMaxLength(200)
                    .HasColumnName("address_line_1")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(200)
                    .HasColumnName("address_line_2")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.City)
                    .HasMaxLength(200)
                    .HasColumnName("city")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Country)
                    .HasMaxLength(3)
                    .HasColumnName("country")
                    .IsFixedLength(true);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(20)
                    .HasColumnName("postal_code")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Province)
                    .HasMaxLength(200)
                    .HasColumnName("province")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.State)
                    .HasMaxLength(200)
                    .HasColumnName("state")
                    .HasDefaultValueSql("''");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.Country)
                    .HasConstraintName("FK_address_country");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("contact");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(300)
                    .HasColumnName("email");

                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .HasColumnName("fax");

                entity.Property(e => e.PersonName)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("person_name")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Telephone1)
                    .HasMaxLength(20)
                    .HasColumnName("telephone_1");

                entity.Property(e => e.Telephone2)
                    .HasMaxLength(20)
                    .HasColumnName("telephone_2");

                entity.Property(e => e.Telephone3)
                    .HasMaxLength(20)
                    .HasColumnName("telephone_3");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PRIMARY");

                entity.ToTable("country");

                entity.Property(e => e.Code)
                    .HasMaxLength(3)
                    .HasColumnName("code")
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Depot>(entity =>
            {
                entity.ToTable("depot");

                entity.HasIndex(e => e.AddressId, "FK_depot_address");

                entity.HasIndex(e => e.ContactId, "FK_depot_contact");

                entity.HasIndex(e => e.SupplierId, "FK_depot_supplier");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AddressId)
                    .HasColumnType("int(11)")
                    .HasColumnName("address_id");

                entity.Property(e => e.ContactId)
                    .HasColumnType("int(11)")
                    .HasColumnName("contact_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(300)
                    .HasColumnName("name");

                entity.Property(e => e.SupplierId)
                    .HasColumnType("int(11)")
                    .HasColumnName("supplier_id");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Depots)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_depot_address");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Depots)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_depot_contact");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Depots)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_depot_supplier");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("material");

                entity.HasIndex(e => e.CategoryId, "FK_material_material_category");

                entity.HasIndex(e => e.SupplierId, "FK_material_supplier");

                entity.HasIndex(e => e.UnitOfMeasureId, "FK_material_unit_of_measure");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("category_id");

                entity.Property(e => e.LeadTimeDays).HasColumnName("lead_time_days");

                entity.Property(e => e.Name)
                    .HasMaxLength(300)
                    .HasColumnName("name");

                entity.Property(e => e.PriceUnit).HasColumnName("price_unit");

                entity.Property(e => e.SupplierId)
                    .HasColumnType("int(11)")
                    .HasColumnName("supplier_id");

                entity.Property(e => e.UnitOfMeasureId)
                    .HasColumnType("int(11)")
                    .HasColumnName("unit_of_measure_id");

                entity.Property(e => e.UnitPrice).HasColumnName("unit_price");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_material_material_category");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_material_supplier");

                entity.HasOne(d => d.UnitOfMeasure)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.UnitOfMeasureId)
                    .HasConstraintName("FK_material_unit_of_measure");
            });

            modelBuilder.Entity<MaterialCategory>(entity =>
            {
                entity.ToTable("material_category");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(300)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<PurchaseRequisition>(entity =>
            {
                entity.ToTable("purchase_requisition");

                entity.HasIndex(e => e.SiteId, "FK_purchase_requisition_site");

                entity.HasIndex(e => e.CreatedBy, "FK_purchase_requisition_user");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(300)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("created_date");

                entity.Property(e => e.IsOpen)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_open");

                entity.Property(e => e.Remarks)
                    .HasColumnType("text")
                    .HasColumnName("remarks");

                entity.Property(e => e.SiteId)
                    .HasColumnType("int(11)")
                    .HasColumnName("site_id");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PurchaseRequisitions)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_purchase_requisition_user");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.PurchaseRequisitions)
                    .HasForeignKey(d => d.SiteId)
                    .HasConstraintName("FK_purchase_requisition_site");
            });

            modelBuilder.Entity<PurchaseRequisitionItem>(entity =>
            {
                entity.HasKey(e => new { e.PurchaseRequisitionId, e.ItemId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("purchase_requisition_item");

                entity.HasIndex(e => e.MaterialId, "FK_purchase_requisition_item_material");

                entity.HasIndex(e => e.Status, "FK_purchase_requisition_item_purchase_requisition_status");

                entity.HasIndex(e => e.ApprovedBy, "FK_purchase_requisition_item_user");

                entity.Property(e => e.PurchaseRequisitionId)
                    .HasColumnType("int(11)")
                    .HasColumnName("purchase_requisition_id");

                entity.Property(e => e.ItemId)
                    .HasColumnType("int(11)")
                    .HasColumnName("item_id");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(300)
                    .HasColumnName("approved_by");

                entity.Property(e => e.ApprovedDate)
                    .HasColumnType("date")
                    .HasColumnName("approved_date");

                entity.Property(e => e.ApprovedQuantity).HasColumnName("approved_quantity");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("created_date");

                entity.Property(e => e.MaterialId)
                    .HasColumnType("int(11)")
                    .HasColumnName("material_id");

                entity.Property(e => e.Remarks)
                    .HasColumnType("text")
                    .HasColumnName("remarks");

                entity.Property(e => e.RequiredDate)
                    .HasColumnType("date")
                    .HasColumnName("required_date");

                entity.Property(e => e.RequiredQuantity).HasColumnName("required_quantity");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.PurchaseRequisitionItems)
                    .HasForeignKey(d => d.ApprovedBy)
                    .HasConstraintName("FK_purchase_requisition_item_user");

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.PurchaseRequisitionItems)
                    .HasForeignKey(d => d.MaterialId)
                    .HasConstraintName("FK_purchase_requisition_item_material");

                entity.HasOne(d => d.PurchaseRequisition)
                    .WithMany(p => p.PurchaseRequisitionItems)
                    .HasForeignKey(d => d.PurchaseRequisitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_purchase_requisition_item_purchase_requisition");
            });

            modelBuilder.Entity<Site>(entity =>
            {
                entity.ToTable("site");

                entity.HasIndex(e => e.AddressId, "FK_site_address");

                entity.HasIndex(e => e.ContactId, "FK_site_contact");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AddressId)
                    .HasColumnType("int(11)")
                    .HasColumnName("address_id");

                entity.Property(e => e.ContactId)
                    .HasColumnType("int(11)")
                    .HasColumnName("contact_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(300)
                    .HasColumnName("name");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Sites)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_site_address");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Sites)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_site_contact");
            });

            modelBuilder.Entity<SiteBudget>(entity =>
            {
                entity.HasKey(e => new { e.SiteId, e.MaterialCategoryId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("site_budget");

                entity.HasIndex(e => e.MaterialCategoryId, "FK_site_budget_material_category");

                entity.HasIndex(e => e.Supervisor, "FK_site_budget_user");

                entity.Property(e => e.SiteId)
                    .HasColumnType("int(11)")
                    .HasColumnName("site_id");

                entity.Property(e => e.MaterialCategoryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("material_category_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Supervisor)
                    .HasMaxLength(300)
                    .HasColumnName("supervisor");

                entity.HasOne(d => d.MaterialCategory)
                    .WithMany(p => p.SiteBudgets)
                    .HasForeignKey(d => d.MaterialCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_site_budget_material_category");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.SiteBudgets)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_site_budget_site");

                entity.HasOne(d => d.SupervisorNavigation)
                    .WithMany(p => p.SiteBudgets)
                    .HasForeignKey(d => d.Supervisor)
                    .HasConstraintName("FK_site_budget_user");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("supplier");

                entity.HasIndex(e => e.BillToAddressId, "FK_supplier_address");

                entity.HasIndex(e => e.ContactId, "FK_supplier_contact");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.BillToAddressId)
                    .HasColumnType("int(11)")
                    .HasColumnName("bill_to_address_id");

                entity.Property(e => e.ContactId)
                    .HasColumnType("int(11)")
                    .HasColumnName("contact_id");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Name)
                    .HasMaxLength(300)
                    .HasColumnName("name");

                entity.HasOne(d => d.BillToAddress)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.BillToAddressId)
                    .HasConstraintName("FK_supplier_address");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_supplier_contact");
            });

            modelBuilder.Entity<UnitOfMeasure>(entity =>
            {
                entity.ToTable("unit_of_measure");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PRIMARY");

                entity.ToTable("user");

                entity.HasIndex(e => e.UserType, "FK_user_user_type");

                entity.Property(e => e.Username)
                    .HasMaxLength(300)
                    .HasColumnName("username");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .HasColumnName("last_name");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(600)
                    .HasColumnName("password_hash");

                entity.Property(e => e.UserType)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_type");

                entity.HasOne(d => d.UserTypeNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserType)
                    .HasConstraintName("FK_user_user_type");
            });

            modelBuilder.Entity<UserSite>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.Site })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("user_site");

                entity.HasIndex(e => e.Site, "FK_user_site_site");

                entity.Property(e => e.Username)
                    .HasMaxLength(300)
                    .HasColumnName("username");

                entity.Property(e => e.Site)
                    .HasColumnType("int(11)")
                    .HasColumnName("site");

                entity.HasOne(d => d.SiteNavigation)
                    .WithMany(p => p.UserSites)
                    .HasForeignKey(d => d.Site)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_site_site");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.UserSites)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_site_user");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("user_type");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(300)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
