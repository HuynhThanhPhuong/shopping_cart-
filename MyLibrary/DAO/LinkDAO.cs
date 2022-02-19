using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Model;

namespace MyLibrary.DAO
{
    public class LinkDAO
    {
        private ThoiTrangDBContext db = new ThoiTrangDBContext();
        //tra ve danh sach
        public List<Link> getList(String page = "index")
        {
            if (page == "Index")
            {
                var list = db.Links
                .Where(m => m.Status != 0)
                .ToList();
                return list;
            }
            else
            {
                var list = db.Links
                .Where(m => m.Status == 0)
                .ToList();
                return list;
            }
        }
        public List<Link> getList(int parentid)
        {
            var list = db.Links.ToList();
            return list;
        }
        //tra ve so luong
        public long getCount()
        {
            var count = db.Links.Count();
            return count;
        }

        //tra ve 1 mau tin
        public Link getRow(int? id)
        {
            var row = db.Links.Find(id);
            return row;
        }

        public Link getRow(String slug)
        {
            var row = db.Links.Where(m => m.Slug == slug).FirstOrDefault();
            return row;
        }

        public void Insert(Link row)
        {
            db.Links.Add(row);
            db.SaveChanges();
        }

        public void Update(Link row)
        {
            db.Entry(row).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Link row)
        {
            db.Links.Remove(row);
            db.SaveChanges();
        }
    }
}
