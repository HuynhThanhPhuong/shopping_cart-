using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MyLibrary.Model
{
    public class ThoiTrangDBContext: DbContext
    {
        public ThoiTrangDBContext() : base("name=ChuoiKN") { }
        public virtual DbSet<Category> Categorys { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Link> Links { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Orderdetail> Orderdetails { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
