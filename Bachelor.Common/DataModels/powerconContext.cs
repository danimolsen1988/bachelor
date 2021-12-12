using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bachelor.Common.DataModels
{
    public partial class powerconContext : DbContext
    {
        public powerconContext()
        {
        }

        public powerconContext(DbContextOptions<powerconContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asset> Assets { get; set; } = null!;
        public virtual DbSet<AssetDevice> AssetDevices { get; set; } = null!;
        public virtual DbSet<AssetType> AssetTypes { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Device> Devices { get; set; } = null!;
        public virtual DbSet<Hardware> Hardwares { get; set; } = null!;
        public virtual DbSet<UnitType> UnitTypes { get; set; } = null!;
        public virtual DbSet<WebUser> WebUsers { get; set; } = null!;
        public virtual DbSet<WebUserAsset> WebUserAssets { get; set; } = null!;
        public virtual DbSet<WebUserType> WebUserTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:powercon.database.windows.net,1433;Initial Catalog=powercon;Persist Security Info=False;User ID=powercon;Password=Qwerty11;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.ToTable("Asset");

                entity.Property(e => e.AssetTypeId).HasColumnName("assetType_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");
            });

            modelBuilder.Entity<AssetDevice>(entity =>
            {
                entity.ToTable("AssetDevice");

                entity.Property(e => e.AssetId).HasColumnName("asset_id");

                entity.Property(e => e.DeviceId).HasColumnName("device_id");
            });

            modelBuilder.Entity<AssetType>(entity =>
            {
                entity.ToTable("AssetType");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.AdOrganizationId)
                    .HasMaxLength(100)
                    .HasColumnName("ad_organization_id");

                entity.Property(e => e.AdOrganizationName).HasColumnName("ad_organization_name");

                entity.Property(e => e.FullCompanyName).HasColumnName("fullCompanyName");
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("Device");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.DeviceId)
                    .HasMaxLength(50)
                    .HasColumnName("deviceId");

                entity.Property(e => e.DeviceTypeId).HasColumnName("deviceType_id");

                entity.Property(e => e.FriendlyName)
                    .HasMaxLength(50)
                    .HasColumnName("friendlyName");

                entity.Property(e => e.HardwareId).HasColumnName("hardware_id");

                entity.Property(e => e.UnitTypeId).HasColumnName("unitType_id");
            });

            modelBuilder.Entity<Hardware>(entity =>
            {
                entity.ToTable("Hardware");

                entity.Property(e => e.Gtin)
                    .HasMaxLength(150)
                    .HasColumnName("GTIN");

                entity.Property(e => e.HardwareId)
                    .HasMaxLength(50)
                    .HasColumnName("hardwareId");

                entity.Property(e => e.HardwareName)
                    .HasMaxLength(150)
                    .HasColumnName("hardwareName");

                entity.Property(e => e.Mpn)
                    .HasMaxLength(70)
                    .HasColumnName("MPN");

                entity.Property(e => e.ServiceInfo).HasColumnName("serviceInfo");
            });

            modelBuilder.Entity<UnitType>(entity =>
            {
                entity.ToTable("UnitType");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Unit)
                    .HasMaxLength(10)
                    .HasColumnName("unit");
            });

            modelBuilder.Entity<WebUser>(entity =>
            {
                entity.ToTable("WebUser");

                entity.Property(e => e.AdUserId)
                    .HasMaxLength(100)
                    .HasColumnName("ad_user_id");

                entity.Property(e => e.AdUserName)
                    .HasMaxLength(100)
                    .HasColumnName("ad_user_name");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.UserTypeId).HasColumnName("userType_id");
            });

            modelBuilder.Entity<WebUserAsset>(entity =>
            {
                entity.ToTable("WebUserAsset");

                entity.Property(e => e.AssetId).HasColumnName("asset_id");

                entity.Property(e => e.WebUserId).HasColumnName("webUser_id");
            });

            modelBuilder.Entity<WebUserType>(entity =>
            {
                entity.ToTable("WebUserType");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.UserLevel).HasColumnName("userLevel");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
