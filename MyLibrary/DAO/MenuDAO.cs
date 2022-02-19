using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Model;

namespace MyLibrary.DAO
{
    public class MenuDAO
    {
        private ThoiTrangDBContext db = new ThoiTrangDBContext();
        //tra ve danh sach
        public List<Menu> getList(String page = "index")
        {
            if (page == "Index")
            {
                var list = db.Menus
                .Where(m => m.Status != 0)
                .OrderBy(m => m.Orders)
                .ToList();
                return list;
            }
            else
            {
                var list = db.Menus
                .Where(m => m.Status == 0)
                .OrderBy(m => m.Orders)
                .ToList();
                return list;
            }
        }
        public List<Menu> getList(int parentid=0, String position="mainmenu")
        {
            var list = db.Menus.Where(m=>m.Position==position && m.Status==1 && m.Parentid==parentid)
                .OrderBy(m=>m.Orders).ToList();
            return list;
        }
        public List<Menu> getList(int parentid)
        {
            var list = db.Menus.Where(m => m.Parentid == parentid && m.Status == 1).ToList();
            return list;
        }
        //tra ve so luong
        public long getCount()
        {
            var count = db.Menus.Count();
            return count;
        }

        //tra ve 1 mau tin
        public Menu getRow(int? id)
        {
            var row = db.Menus.Find(id);
            return row;
        }


        public void Insert(Menu row)
        {
            db.Menus.Add(row);
            db.SaveChanges();
        }

        public void Update(Menu row)
        {
            db.Entry(row).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Menu row)
        {
            db.Menus.Remove(row);
            db.SaveChanges();
        }
    }
}
