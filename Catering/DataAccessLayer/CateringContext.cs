using Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CateringContext : IdentityDbContext<Member>
    {
        public CateringContext() : base("CateringContext") //IdentityDbContext'den miras aldığımızda bunu yazmak zorundayız!
        {

        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductItem> ProductItems { get; set; }
        //public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region TableConfiguration
            modelBuilder.Entity<Product>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<ProductItem>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<ShoppingCart>()
                .HasKey(x => x.Id);
            /*modelBuilder.Entity<ProductCategory>()
                .HasKey(x => x.Id);*/
            modelBuilder.Entity<Certificate>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Notification>()
                .HasKey(x => x.Id);
            #endregion


            #region Relations
            modelBuilder.Entity<Member>()
                .HasMany(x => x.Certificates)
                .WithRequired(x => x.Member);
            //.HasForeignKey(x => x.MemberId);
            modelBuilder.Entity<Member>()
                .HasMany(x => x.Products);
            modelBuilder.Entity<Member>()
                .HasMany(x => x.Notifications)
                .WithRequired(x => x.Member);
            /*.HasForeignKey(x => x.MemberId);*/
            modelBuilder.Entity<Member>()
                .HasOptional(x => x.ShoppingCart)
                .WithRequired(x => x.Member);

            /*modelBuilder.Entity<Product>()
               .HasRequired(x => x.Category)
                .WithMany(x => x.Products);*/
            //.HasForeignKey(x => x.CategoryId);
            modelBuilder.Entity<Product>()
                .HasMany(x => x.ProductItems)
                .WithRequired(x => x.Product);

            modelBuilder.Entity<ShoppingCart>()
                .HasMany(x => x.ProductItems)
                .WithRequired(x => x.ShoppingCart);
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
