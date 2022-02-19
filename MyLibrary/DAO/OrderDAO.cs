using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Model;

namespace MyLibrary.DAO
{
    public class OrderDAO
    {
        private ThoiTrangDBContext db = new ThoiTrangDBContext();
        //tra ve danh sach
        public List<Order> getList()
        {
            var list = db.Orders.ToList();
            return list;
        }

        //tra ve so luong
        public long getCount()
        {
            var count = db.Orders.Count();
            return count;
        }

        //tra ve 1 mau tin
        public Order getRow(int? id)
        {
            var row = db.Orders.Find(id);
            return row;
        }


        public void Insert(Order row)
        {
            db.Orders.Add(row);
            db.SaveChanges();
        }

        public void Update(Order row)
        {
            db.Entry(row).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Order row)
        {
            db.Orders.Remove(row);
            db.SaveChanges();
        }
    }
}
