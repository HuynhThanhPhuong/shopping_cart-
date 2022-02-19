using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Model;

namespace MyLibrary.DAO
{
    public class OderdetailDAO
    {
        private ThoiTrangDBContext db = new ThoiTrangDBContext();
        //tra ve danh sach
        public List<Orderdetail> getList()
        {
            var list = db.Orderdetails.ToList();
            return list;
        }

        //tra ve so luong
        public long getCount()
        {
            var count = db.Orderdetails.Count();
            return count;
        }

        //tra ve 1 mau tin
        public Orderdetail getRow(int? id)
        {
            var row = db.Orderdetails.Find(id);
            return row;
        }


        public void Insert(Orderdetail row)
        {
            db.Orderdetails.Add(row);
            db.SaveChanges();
        }

        public void Update(Orderdetail row)
        {
            db.Entry(row).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Orderdetail row)
        {
            db.Orderdetails.Remove(row);
            db.SaveChanges();
        }
    }
}
