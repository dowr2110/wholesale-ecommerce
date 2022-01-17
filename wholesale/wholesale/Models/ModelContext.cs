using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace wholesale.Models
{
    public class ModelContext : DbContext
    {
        public ModelContext() : base("name=ModelContext")
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public DbSet<ShopCard> ShopCards { get; set; }
        public DbSet<ShopCartForOrder> ShopCartForOrders { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Wishlist> WishLists { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

        public DbSet<News> Newses { get; set; }


    }
}