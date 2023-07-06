using ToDoAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Collections.Generic;

namespace ToDoAPI.Data
{
    public class HobbyContext : DbContext
    {
        public HobbyContext(DbContextOptions<HobbyContext> opt) : base(opt)
        {

        }

        #region DbSet
        public DbSet<Hobby>? Hobbys { get; set; }
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<UserCart>? UserCarts { get; set; }
        public DbSet<UserCartProduct>? UserCartProducts { get; set; }
        public DbSet<Order>? Orders { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

            // 1 - N
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);


            // 1 - 1
            modelBuilder.Entity<Account>()
                .HasOne(a => a.UserCart)
                .WithOne(uc => uc.Account)
                .HasForeignKey<UserCart>(uc => uc.AccountId);

            // N - N
            modelBuilder.Entity<UserCartProduct>()
                .HasKey(ucp => new { ucp.UserCartId, ucp.ProductId });

            modelBuilder.Entity<UserCartProduct>()
                .HasOne(ucp => ucp.UserCart)
                .WithMany(uc => uc.UserCartProducts)
                .HasForeignKey(ucp => ucp.UserCartId);

            modelBuilder.Entity<UserCartProduct>()
                .HasOne(ucp => ucp.Product)
                .WithMany(p => p.UserCartProducts)
                .HasForeignKey(ucp => ucp.ProductId);

            // Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Account)
                .WithOne()
                .HasForeignKey<Order>(o => o.AccountId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderProducts)
                .WithOne(op => op.Order)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany()
                .HasForeignKey(op => op.ProductId);
        }
    }
}
