using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Model;

namespace MyLibrary.DAO
{
    public class TopicDAO
    {
        private ThoiTrangDBContext db = new ThoiTrangDBContext();
        //tra ve danh sach
        public List<Topic> getList(int topicid, int notid)
        {
            var list = db.Topics.Where(m => m.Status == 1 && m.Id != notid)
                .OrderByDescending(m => m.Create_At)
                .ToList();
            return list;
        }
        public List<Topic> getList(String page = "index")
        {
            if (page == "Index")
            {
                var list = db.Topics
                .Where(m => m.Status != 0)
                .OrderBy(m => m.Orders)
                .ToList();
                return list;
            }
            else
            {
                var list = db.Topics
                .Where(m => m.Status == 0)
                .OrderBy(m => m.Orders)
                .ToList();
                return list;
            }
        }
        public List<Topic> getList(int parentid = 0)
        {
            var list = db.Topics.Where(m => m.Parentid == parentid && m.Status == 1)
                .OrderBy(m => m.Orders)
                .ToList();
            return list;
        }
        //tra ve so luong
        public long getCount()
        {
            var count = db.Topics.Count();
            return count;
        }

        //tra ve 1 mau tin
        public Topic getRow(int? id)
        {
            var row = db.Topics.Find(id);
            return row;
        }

        public Topic getRow(String slug)
        {
            var row = db.Topics.Where(m => m.Slug == slug).FirstOrDefault();
            return row;
        }

        public void Insert(Topic row)
        {
            db.Topics.Add(row);
            db.SaveChanges();
        }

        public void Update(Topic row)
        {
            db.Entry(row).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Topic row)
        {
            db.Topics.Remove(row);
            db.SaveChanges();
        }
    }
}
