using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Model;

namespace MyLibrary.DAO
{
    public class SliderDAO
    {
        private ThoiTrangDBContext db = new ThoiTrangDBContext();
        //tra ve danh sach
        public List<Slider> getList()
        {
            var list = db.Sliders.ToList();
            return list;
        }
        public List<Slider> getList(String position)
        {
            var list = db.Sliders.Where(m => m.Position == position && m.Status == 1).ToList();
            return list;
        }
        //tra ve so luong
        public long getCount()
        {
            var count = db.Sliders.Count();
            return count;
        }

        //tra ve 1 mau tin
        public Slider getRow(int? id)
        {
            var row = db.Sliders.Find(id);
            return row;
        }

        public void Insert(Slider row)
        {
            db.Sliders.Add(row);
            db.SaveChanges();
        }

        public void Update(Slider row)
        {
            db.Entry(row).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Slider row)
        {
            db.Sliders.Remove(row);
            db.SaveChanges();
        }
    }
}
