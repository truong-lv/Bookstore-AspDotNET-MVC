using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Bookstore_AspDotNET_MVC.Models;

#nullable disable

namespace Bookstore_AspDotNET_MVC.Data
{
    public partial class BOOKSTOREContext : DbContext
    {
        public BOOKSTOREContext()
        {
        }

        public BOOKSTOREContext(DbContextOptions<BOOKSTOREContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AddressDetail> AddressDetails { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookDiscount> BookDiscounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<PublishingCompany> PublishingCompanies { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserAddress> UserAddresses { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Userinfor> Userinfors { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=DESKTOP-PCNAJ04\\SQLEXPRESS; initial Catalog=BOOKSTORE; Integrated Security=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AddressDetail>(entity =>
            {
                entity.HasKey(e => e.AddressId)
                    .HasName("PK__address___CAA247C8D3EE980F");

                entity.HasOne(d => d.Warrd)
                    .WithMany(p => p.AddressDetails)
                    .HasForeignKey(d => d.WarrdId)
                    .HasConstraintName("FK_address_detail_ward");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.IdAuthor)
                    .HasName("PK__author__7411B254ED283315");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.IdBook)
                    .HasName("PK__book__DAE712E8BD98D29E");

                entity.Property(e => e.Picture).IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_BOOK_CATEGORY");

                entity.HasOne(d => d.IdAuthorNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.IdAuthor)
                    .HasConstraintName("FK_BOOK_AUTHOR");

                entity.HasOne(d => d.IdCompanyNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.IdCompany)
                    .HasConstraintName("FK_BOOK_COMANY");
            });

            modelBuilder.Entity<BookDiscount>(entity =>
            {
                entity.HasKey(e => new { e.IdBook, e.IdDícount });

                entity.HasOne(d => d.IdBookNavigation)
                    .WithMany(p => p.BookDiscounts)
                    .HasForeignKey(d => d.IdBook)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_book_discount_book");

                entity.HasOne(d => d.IdDícountNavigation)
                    .WithMany(p => p.BookDiscounts)
                    .HasForeignKey(d => d.IdDícount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_book_discount_discount");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.Property(e => e.IdDiscount).ValueGeneratedNever();
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_district_province");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasOne(d => d.IdBookNavigation)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.IdBook)
                    .HasConstraintName("FK_ITEM_BOOK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ITEM_USERINFOR");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.NameOfCustomer).IsUnicode(false);

                entity.Property(e => e.PhoneOfCustomer).IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_ODERS_ADDRESSDETAIL");

                entity.HasOne(d => d.IdPaymentNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdPayment)
                    .HasConstraintName("FK_orders_payment");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ORDERS_USERINFOR");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasOne(d => d.IdBookNavigation)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.IdBook)
                    .HasConstraintName("FK_ORDERDETAIL_BOOK");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_ORDERDETAIL_ORDERS");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.IdPayment).ValueGeneratedNever();

                entity.Property(e => e.PaymentType).IsUnicode(false);
            });

            modelBuilder.Entity<PublishingCompany>(entity =>
            {
                entity.HasKey(e => e.IdCompany)
                    .HasName("PK__publishi__5D0E9F0627D8EE34");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => new { e.IdBook, e.UserId })
                    .HasName("PK__review__317CF198E2C01A3A");

                entity.HasOne(d => d.IdBookNavigation)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.IdBook)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_REVIEW_BOOK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_REVIEW_USERINFOR");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PK__roles__3D48441D607836CF");

                entity.Property(e => e.RoleName).IsUnicode(false);
            });

            modelBuilder.Entity<UserAddress>(entity =>
            {
                entity.HasKey(e => new { e.AddressId, e.UserId })
                    .HasName("PK__user_add__2139A4B8579DB4A4");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USERADDRESS_ADDRESSDETAIL");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USERADDRESS_USERINFOR");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.IdRole)
                    .HasConstraintName("FK_USERROLE_ROLE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_USERROLE_USERINFOR");
            });

            modelBuilder.Entity<Userinfor>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__userinfo__B9BE370FD79949DA");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Gender).IsFixedLength(true);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.HasOne(d => d.District)
                    .WithMany(p => p.Wards)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_ward_district");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
