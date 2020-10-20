using System;
using System.Collections.Generic;
using System.Text;
using Core_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Product-Tag Many to many
            modelBuilder.Entity<ProductTag>()
                .HasKey(pt => new { pt.ProductId, pt.TagId });
            modelBuilder.Entity<ProductTag>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.ProductTags)
                .HasForeignKey(pt => pt.ProductId);
            modelBuilder.Entity<ProductTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.ProductTags)
                .HasForeignKey(pt => pt.TagId);
            ////////////////////
            //Product-Payment type Many to many
            modelBuilder.Entity<ProductPaymentType>()
                .HasKey(pp => new { pp.ProductId,pp.PaymentTypeId});

            modelBuilder.Entity<ProductPaymentType>()
               .HasOne(pp => pp.Product)
               .WithMany(p => p.ProductPaymentTypes)
               .HasForeignKey(pp => pp.ProductId);
            modelBuilder.Entity<ProductPaymentType>()
                .HasOne(pp => pp.PaymentType)
                .WithMany(pt => pt.ProductPaymentTypes)
                .HasForeignKey(pp => pp.PaymentTypeId);
            ////////////////////
            //Product-Order(cart) Many to many
            modelBuilder.Entity<ProductCart>()
                .HasKey(pc => new { pc.ProductId, pc.CartId });

            //modelBuilder.Entity<ProductCart>()
            //    .Property(pc => pc.Quantity)
            //    .IsRequired();

            modelBuilder.Entity<ProductCart>()
               .HasOne(pc => pc.Product)
               .WithMany(p => p.ProductCarts)
               .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductCart>()
                .HasOne(pc => pc.Cart)
                .WithMany(c => c.ProductCarts)
                .HasForeignKey(pc => pc.CartId);






            base.OnModelCreating(modelBuilder);


        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Catigory> Catigories { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductPaymentType> ProductPaymentTypes { get; set; }
        public virtual DbSet<ProductTag> ProductTags { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
       
        public virtual DbSet<ProductCart> ProductCarts { get; set; }
        public virtual DbSet<Image> Images { get; set; }





    }
}
