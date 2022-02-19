using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Model;

namespace MyLibrary.DAO
{
    public class UserDAO
    {
        private ThoiTrangDBContext db = new ThoiTrangDBContext();
        //tra ve danh sach
        public List<User> getList()
        {
            var list = db.Users.ToList();
            return list;
        }

        //tra ve so luong
        public long getCount()
        {
            var count = db.Users.Count();
            return count;
        }

        //tra ve 1 mau tin
        public User getRow(int? id)
        {
            var row = db.Users.Find(id);
            return row;
        }
        public User getRow(String username)
        {
            var row = db.Users
                .Where(m => m.Roles == 1 && m.Status == 1 && (m.Username == username || m.Email == username))
                .FirstOrDefault();
            return row;
        }

        public void Insert(User row)
        {
            db.Users.Add(row);
            db.SaveChanges();
        }

        public void Update(User row)
        {
            db.Entry(row).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(User row)
        {
            db.Users.Remove(row);
            db.SaveChanges();
        }
    }
}
