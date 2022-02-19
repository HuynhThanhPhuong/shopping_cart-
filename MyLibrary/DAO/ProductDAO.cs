using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Model;
using PagedList;

namespace MyLibrary.DAO
{
    public class ProductDAO
    {
        private ThoiTrangDBContext db = new ThoiTrangDBContext();
        public List<Products> getList(string page = "Index")
        {
            if (page == "Index")
            {
                var list = db.Products
                .Where(m => m.Status != 0)

                .ToList();
                return list;
            }
            else
            {
                var list = db.Products
                .Where(m => m.Status == 0)

                .ToList();
                return list;
            }

        }
        public List<Products> getList(int? catid = 0)
        {
            var list = db.Products
                .Where(m => m.Catid == catid && m.Status == 1)

                .ToList();
            return list;
        }
        //phan trang
        public PagedList.IPagedList<Products> getList(int pageSize, int pageNumber)
        {
            var list = db.Products.Where(m => m.Status == 1)
                .OrderByDescending(m => m.Create_At)
                .ToPagedList(pageNumber, pageSize);
            return list;
        }
        //lay sp theo loai
        public List<Products> getList(List<int> listcatid, int limit,int notid,bool check=true)
        {
            var list = db.Products.Where(m => m.Status == 1 && m.Id!=notid && listcatid.Contains(m.Catid))
                .OrderByDescending(m => m.Create_At)
                .Take(limit)
                .ToList();
            return list;
        }
        public List<Products> getList(List<int> listcatid, int limit)
        {
            var list = db.Products.Where(m => m.Status == 1 && listcatid.Contains(m.Catid))
                .OrderByDescending(m => m.Create_At)
                .Take(limit)
                .ToList();
            return list;
        }
        //tra ve danh sach
        public PagedList.IPagedList<Products> getList(List<int> listcatid, int pageSize, int pageNumber)
        {
            var list = db.Products.Where(m => m.Status == 1 && listcatid.Contains(m.Catid))
                .OrderByDescending(m=>m.Create_At)
                .ToPagedList(pageNumber, pageSize); 
            return list;
        }


        //tra ve so luong
        public long getCount()
        {
            var count = db.Products.Count();
            return count;
        }

        //tra ve 1 mau tin
        public Products getRow(int? id)
        {
            var row = db.Products.Find(id);
            return row;
        }

        public Products getRow(String slug)
        {
            var row = db.Products.Where(m => m.Slug == slug && m.Status==1).FirstOrDefault();
            return row;
        }

        public void Insert(Products row)
        {
            db.Products.Add(row);
            db.SaveChanges();
        }

        public void Update(Products row)
        {
            db.Entry(row).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Products row)
        {
            db.Products.Remove(row);
            db.SaveChanges();
        }
    }
}
