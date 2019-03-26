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
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region TableConfiguration
            modelBuilder.Entity<Product>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<ShoppingCart>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Certificate>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Notification>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Order>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<OrderItem>()
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
                .HasMany(x => x.ProductItems)
                .WithRequired(x => x.Product);*/
            modelBuilder.Entity<ShoppingCart>()
                .HasMany(x => x.Products)
                .WithRequired(x => x.ShoppingCart);
            modelBuilder.Entity<Order>()
                .HasRequired(x => x.Member)
                .WithMany(x => x.Orders);
            modelBuilder.Entity<OrderItem>()
                .HasRequired(x => x.Order)
                .WithMany(x => x.OrderItems);
			modelBuilder.Entity<OrderItem>()
				.HasRequired(x => x.Product)
				.WithMany(x => x.OrderItems);
			#endregion

			base.OnModelCreating(modelBuilder);
        }
    }
}
