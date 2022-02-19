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
    public class MenuController : Controller
    {
        MenuDAO menuDAO = new MenuDAO();
        LinkDAO linkDAO = new LinkDAO();
        // GET: Admin/Menu
        public ActionResult Index()
        {
            return View(menuDAO.getList("Index"));
        }
        public ActionResult Trash()
        {
            return View(menuDAO.getList("Trash"));
        }
        // GET: Admin/Menu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Admin/Menu/Create
        public ActionResult Create()
        {
            ViewBag.ListLink = new SelectList(menuDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(menuDAO.getList("Index"), "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/Menu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                menu.Link = MyString.Str_slug(menu.Name);
                menu.Create_At = DateTime.Now;
                menu.Create_By = int.Parse(Session["UserId"].ToString());
                menu.Update_At = DateTime.Now;
                menu.Update_By = int.Parse(Session["UserId"].ToString());
                menuDAO.Insert(menu);
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        // GET: Admin/Menu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListLink = new SelectList(menuDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(menuDAO.getList("Index"), "Orders", "Name", 0);
            return View(menu);
        }

        // POST: Admin/Menu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                menu.Link = MyString.Str_slug(menu.Name);
                menu.Update_At = DateTime.Now;
                menu.Update_By = int.Parse(Session["UserId"].ToString());
                menuDAO.Update(menu);
                return RedirectToAction("Index");
            }
            ViewBag.ListLink = new SelectList(menuDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(menuDAO.getList("Index"), "Orders", "Name", 0);
            return View(menu);
        }

        // GET: Admin/Menu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Admin/Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id == null)
            {
                TempData["XMessage"] = new MyMessage("Không có Id", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            menuDAO.Delete(menu);
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
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            menu.Status = (menu.Status == 1) ? 2 : 1;
            menu.Update_At = DateTime.Now;
            menu.Update_By = int.Parse(Session["UserId"].ToString());
            menuDAO.Update(menu);
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
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            menu.Status = 0;
            menu.Update_At = DateTime.Now;
            menu.Update_By = int.Parse(Session["UserId"].ToString());
            menuDAO.Update(menu);
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
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            menu.Status = 2;
            menu.Update_At = DateTime.Now;
            menu.Update_By = int.Parse(Session["UserId"].ToString());
            menuDAO.Update(menu);
            TempData["XMessage"] = new MyMessage("Khôi phục mẫu tin thành công", "success");
            return RedirectToAction("Index");//Chuyển hướng trang
        }
    }
}
