using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Model;
using System.Data.Entity;

namespace MyLibrary.DAO
{
    public class ContactDAO
    {
        private ThoiTrangDBContext db = new ThoiTrangDBContext();
        public List<Contact> getList()
        {
            var list = db.Contacts.ToList();
            return list;
        }

        //tra ve so luong
        public long getCount()
        {
            var count = db.Contacts.Count();
            return count;
        }

        //tra ve 1 mau tin
        public Contact getRow(int? id)
        {
            var row = db.Contacts.Find(id);
            return row;
        }

        public Contact getRow(String slug)
        {
            var row = db.Contacts.Where(m => m.Slug == slug).FirstOrDefault();
            return row;
        }

        public void Insert(Contact row)
        {
            db.Contacts.Add(row);
            db.SaveChanges();
        }

        public void Update(Contact row)
        {
            db.Entry(row).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Contact row)
        {
            db.Contacts.Remove(row);
            db.SaveChanges();
        }
    }
}
