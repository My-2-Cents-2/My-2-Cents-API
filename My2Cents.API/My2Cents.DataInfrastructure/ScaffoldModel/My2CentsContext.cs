using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace My2Cents.DataInfrastructure
{
    public partial class My2CentsContext : IdentityDbContext<ApplicationUser, ApplicationRole, int,
                                            IdentityUserClaim<int>, ApplicationUserRole, IdentityUserLogin<int>,
                                            IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public My2CentsContext()
        {
        }

        public My2CentsContext(DbContextOptions<My2CentsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AccountType> AccountTypes { get; set; } = null!;
        public virtual DbSet<Crypto> Cryptos { get; set; } = null!;
        public virtual DbSet<CryptoAsset> CryptoAssets { get; set; } = null!;
        public virtual DbSet<CryptoOrderHistory> CryptoOrderHistories { get; set; } = null!;
        public virtual DbSet<Stock> Stocks { get; set; } = null!;
        public virtual DbSet<StockAsset> StockAssets { get; set; } = null!;
        public virtual DbSet<StockOrderHistory> StockOrderHistories { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<UserProfile> UserProfiles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.HasIndex(e => e.AccountTypeId, "IX_Account_AccountTypeID");

                entity.HasIndex(e => e.UserId, "IX_Account_UserID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.AccountTypeId).HasColumnName("AccountTypeID");

                entity.Property(e => e.Interest).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotalBalance).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.AccountTypeId)
                    .HasConstraintName("ACC_FK_AccTypeID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("ACC_FK_UserID");
            });

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.ToTable("AccountType");

                entity.Property(e => e.AccountTypeId).HasColumnName("AccountTypeID");

                entity.Property(e => e.AccountType1)
                    .HasMaxLength(40)
                    .HasColumnName("AccountType");
            });

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            modelBuilder.Entity<ApplicationRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            modelBuilder.Entity<Crypto>(entity =>
            {
                entity.ToTable("Crypto");

                entity.Property(e => e.CryptoId).HasColumnName("CryptoID");

                entity.Property(e => e.CurrentPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.LastUpdate).HasColumnType("smalldatetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.ShortenedName).HasMaxLength(255);
            });

            modelBuilder.Entity<CryptoAsset>(entity =>
            {
                entity.ToTable("CryptoAsset");

                entity.Property(e => e.CryptoAssetId).HasColumnName("CryptoAssetID");

                entity.Property(e => e.BuyDate).HasColumnType("smalldatetime");

                entity.Property(e => e.BuyPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CryptoId).HasColumnName("CryptoID");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.StopLoss).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TakeProfit).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Crypto)
                    .WithMany(p => p.CryptoAssets)
                    .HasForeignKey(d => d.CryptoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CryptoAss__Crypt__00DF2177");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CryptoAssets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CryptoAss__UserI__7FEAFD3E");
            });

            modelBuilder.Entity<CryptoOrderHistory>(entity =>
            {
                entity.HasKey(e => e.CryptoOrderId)
                    .HasName("PK__CryptoOr__48E76F306D355A0A");

                entity.ToTable("CryptoOrderHistory");

                entity.Property(e => e.CryptoOrderId).HasColumnName("CryptoOrderID");

                entity.Property(e => e.CryptoId).HasColumnName("CryptoID");

                entity.Property(e => e.OrderPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.OrderTime).HasColumnType("smalldatetime");

                entity.Property(e => e.OrderType).HasMaxLength(255);

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Crypto)
                    .WithMany(p => p.CryptoOrderHistories)
                    .HasForeignKey(d => d.CryptoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CryptoOrd__Crypt__7D0E9093");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CryptoOrderHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CryptoOrd__UserI__7C1A6C5A");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("Stock");

                entity.Property(e => e.StockId).HasColumnName("StockID");

                entity.Property(e => e.CurrentPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.LastUpdate).HasColumnType("smalldatetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.ShortenedName).HasMaxLength(255);
            });

            modelBuilder.Entity<StockAsset>(entity =>
            {
                entity.ToTable("StockAsset");

                entity.Property(e => e.StockAssetId).HasColumnName("StockAssetID");

                entity.Property(e => e.BuyDate).HasColumnType("smalldatetime");

                entity.Property(e => e.BuyPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.StockId).HasColumnName("StockID");

                entity.Property(e => e.StopLoss).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TakeProfit).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.StockAssets)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StockAsse__Stock__7755B73D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.StockAssets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StockAsse__UserI__76619304");
            });

            modelBuilder.Entity<StockOrderHistory>(entity =>
            {
                entity.HasKey(e => e.StockOrderId)
                    .HasName("PK__StockOrd__928D048E6F9F4C1E");

                entity.ToTable("StockOrderHistory");

                entity.Property(e => e.StockOrderId).HasColumnName("StockOrderID");

                entity.Property(e => e.OrderPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.OrderTime).HasColumnType("smalldatetime");

                entity.Property(e => e.OrderType).HasMaxLength(255);

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.StockId).HasColumnName("StockID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.StockOrderHistories)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StockOrde__Stock__73852659");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.StockOrderHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StockOrde__UserI__72910220");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasIndex(e => e.AccountId, "IX_Transactions_AccountID");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Authorized).HasMaxLength(20);

                entity.Property(e => e.LineAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TransactionName).HasMaxLength(100);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("Ts_FK_AccID");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__User_Pro__1788CCAC4512CFFA");

                entity.ToTable("User_Profile");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserID");

                entity.Property(e => e.City).HasMaxLength(40);

                entity.Property(e => e.Employer).HasMaxLength(150);

                entity.Property(e => e.FirstName).HasMaxLength(30);

                entity.Property(e => e.LastName).HasMaxLength(30);

                entity.Property(e => e.MailingAddress).HasMaxLength(250);

                entity.Property(e => e.Phone).HasMaxLength(36);

                entity.Property(e => e.SecondaryEmail).HasMaxLength(250);

                entity.Property(e => e.State).HasMaxLength(40);

                entity.Property(e => e.WorkAddress).HasMaxLength(250);

                entity.Property(e => e.WorkPhone).HasMaxLength(36);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserProfile)
                    .HasForeignKey<UserProfile>(d => d.UserId)
                    .HasConstraintName("FK_UserID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
