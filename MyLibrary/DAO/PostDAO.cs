using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Model;

namespace MyLibrary.DAO
{
    public class PostDAO
    {
        private ThoiTrangDBContext db = new ThoiTrangDBContext();
        //tra ve danh sach
        public List<Post> getList(String page = "index")
        {
            if (page == "Index")
            {
                var list = db.Posts
                .Where(m => m.Status != 0)
                .ToList();
                return list;
            }
            else
            {
                var list = db.Posts
                .Where(m => m.Status == 0)
                .ToList();
                return list;
            }
        }
        public List<Post> getList(int topicid)
        {
            var list = db.Posts.Where(m=>m.Status==1 && m.Topicid==topicid)
                .ToList();
            return list;
        }
        public List<Post> getList(int? topid, int limit,int notid)
        {
            var list = db.Posts.Where(m => m.Topicid == topid && m.Status == 1 && m.Id!=notid)
                .OrderByDescending(m => m.Create_At)
                .Take(limit)
                .ToList();
            return list;
        }
        //tra ve so luong
        public long getCount()
        {
            var count = db.Posts.Count();
            return count;
        }

        //tra ve 1 mau tin
        public Post getRow(int? id)
        {
            var row = db.Posts.Find(id);
            return row;
        }

        public Post getRow(String slug)
        {
            var row = db.Posts.Where(m => m.Slug == slug && m.Status==1).FirstOrDefault();
            return row;
        }

        public void Insert(Post row)
        {
            db.Posts.Add(row);
            db.SaveChanges();
        }

        public void Update(Post row)
        {
            db.Entry(row).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Post row)
        {
            db.Posts.Remove(row);
            db.SaveChanges();
        }
    }
}
