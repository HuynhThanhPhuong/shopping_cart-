using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyLibrary.Model;
using MyLibrary.DAO;
using ShopThoiTrang.Library;

namespace ShopThoiTrang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ProductDAO proDAO = new ProductDAO();
        CategoryDAO catDAO = new CategoryDAO();
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(proDAO.getList("Index"));
        }
        public ActionResult Trash()
        {
            return View(proDAO.getList("Trash"));
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = proDAO.getRow(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.ListCatid = new SelectList(catDAO.getList("Index"), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Products products)
        {
            if (ModelState.IsValid)
            {
                products.Slug = MyString.Str_slug(products.Name);
                products.Create_At = DateTime.Now;

                products.Update_At = DateTime.Now;
                proDAO.Insert(products);
                return RedirectToAction("Index");
            }
            ViewBag.ListCatid = new SelectList(catDAO.getList("Index"), "Id", "Name", 0);
            return View(products);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = proDAO.getRow(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCatid = new SelectList(catDAO.getList("Index"), "Id", "Name", 0);
            return View(products);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Products products)
        {
            if (ModelState.IsValid)
            {
                products.Slug = MyString.Str_slug(products.Name);

                products.Update_At = DateTime.Now;

                products.Update_At = DateTime.Now;
                proDAO.Update(products);
                return RedirectToAction("Index");
            }
            ViewBag.ListCatid = new SelectList(catDAO.getList("Index"), "Id", "Name", 0);
            return View(products);
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["XMessage"] = new MyMessage("Không có Id", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            Products products = proDAO.getRow(id);
            if (products == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            proDAO.Delete(products);
            TempData["XMessage"] = new MyMessage("Thay đổi trạng thái thành công", "success");
            return RedirectToAction("Trash");//Chuyển hướng trang về thùng rác
        }
        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id == null)
            {
                TempData["XMessage"] = new MyMessage("Không có Id", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            Category category = catDAO.getRow(id);
            if (category == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            catDAO.Delete(category);
            TempData["XMessage"] = new MyMessage("Xóa mẫu tin thành công", "success");
            return RedirectToAction("Trash");//Chuyển hướng trang về thùng rác
        }
        //Trạng thái
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["XMessage"] = new MyMessage("Không có Id", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            Products products = proDAO.getRow(id);
            if (products == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            products.Status = (products.Status == 1) ? 2 : 1;
            products.Update_At = DateTime.Now;
            products.Update_By = int.Parse(Session["UserId"].ToString());
            proDAO.Update(products);
            TempData["XMessage"] = new MyMessage("Thay đổi trạng thái thành công", "success");
            return RedirectToAction("Index");//Chuyển hướng trang
        }
        // Xóa vào thùng rác
        public ActionResult Deltrash(int? id)
        {
            if (id == null)
            {
                TempData["XMessage"] = new MyMessage("Không có Id", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            Products products = proDAO.getRow(id);
            if (products == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            products.Status = 0;
            products.Update_At = DateTime.Now;

            proDAO.Update(products);
            TempData["XMessage"] = new MyMessage("Xóa vào thùng rác thành công", "success");
            return RedirectToAction("Index");//Chuyển hướng trang
        }
        // Khôi phục mẫu tin
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["XMessage"] = new MyMessage("Không có Id", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            Products products = proDAO.getRow(id);
            if (products == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            products.Status = 2;
            products.Update_At = DateTime.Now;

            proDAO.Update(products);
            TempData["XMessage"] = new MyMessage("Khôi phục mẫu tin thành công", "success");
            return RedirectToAction("Index");//Chuyển hướng trang
        }
        
    }
}
