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
    public class CategoryController : BaseController
    {
        CategoryDAO catDAO = new CategoryDAO();

        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(catDAO.getList("Index"));
        }
        public ActionResult Trash()
        {
            return View(catDAO.getList("Trash"));
        }
        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = catDAO.getRow(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(catDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(catDAO.getList("Index"), "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = MyString.Str_slug(category.Name);
                category.Create_At = DateTime.Now;
                category.Create_By = int.Parse(Session["UserId"].ToString());
                category.Update_At = DateTime.Now;
                category.Update_By = int.Parse(Session["UserId"].ToString());
                catDAO.Insert(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = catDAO.getRow(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCat = new SelectList(catDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(catDAO.getList("Index"), "Orders", "Name", 0);
            return View(category);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = MyString.Str_slug(category.Name);
                category.Update_At = DateTime.Now;
                category.Update_By = int.Parse(Session["UserId"].ToString());
                catDAO.Update(category);
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(catDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(catDAO.getList("Index"), "Orders", "Name", 0);
            return View(category);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = catDAO.getRow(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id== null)
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
            Category category = catDAO.getRow(id);
            if (category == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            category.Status = (category.Status == 1) ? 2 : 1;
            category.Update_At = DateTime.Now;
            category.Update_By = int.Parse(Session["UserId"].ToString());
            catDAO.Update(category);
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
            Category category = catDAO.getRow(id);
            if (category == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            category.Status = 0;
            category.Update_At = DateTime.Now;
            category.Update_By = int.Parse(Session["UserId"].ToString());
            catDAO.Update(category);
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
            Category category = catDAO.getRow(id);
            if (category == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            category.Status = 2;
            category.Update_At = DateTime.Now;
            category.Update_By = int.Parse(Session["UserId"].ToString());
            catDAO.Update(category);
            TempData["XMessage"] = new MyMessage("Khôi phục mẫu tin thành công", "success");
            return RedirectToAction("Index");//Chuyển hướng trang
        }
        
    }
}
