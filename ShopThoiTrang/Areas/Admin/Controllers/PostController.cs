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
    public class PostController : Controller
    {
        PostDAO pDAO = new PostDAO();
        TopicDAO topDAO = new TopicDAO();

        // GET: Admin/Post
        public ActionResult Index()
        {
            return View(pDAO.getList("Index"));
        }
        public ActionResult Trash()
        {
            return View(pDAO.getList("Trash"));
        }
        // GET: Admin/Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = pDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Post/Create
        public ActionResult Create()
        {
            ViewBag.ListTopicid = new SelectList(topDAO.getList("Index"), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.Slug = MyString.Str_slug(post.Title);
                post.Create_At = DateTime.Now;

                post.Update_At = DateTime.Now;
                pDAO.Insert(post);
                return RedirectToAction("Index");
            }
            ViewBag.ListTopicid = new SelectList(topDAO.getList("Index"), "Id", "Name", 0);
            return View(post);
        }

        // GET: Admin/Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = pDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListTopicid = new SelectList(topDAO.getList("Index"), "Id", "Name", 0);
            return View(post);
        }

        // POST: Admin/Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                post.Slug = MyString.Str_slug(post.Title);
                post.Create_At = DateTime.Now;

                post.Update_At = DateTime.Now;
                pDAO.Update(post);
                return RedirectToAction("Index");
            }
            ViewBag.ListTopicid = new SelectList(topDAO.getList("Index"), "Id", "Name", 0);
            return View(post);
        }

        // GET: Admin/Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["XMessage"] = new MyMessage("Không có Id", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            Post post = pDAO.getRow(id);
            if (post == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            pDAO.Delete(post);
            TempData["XMessage"] = new MyMessage("Thay đổi trạng thái thành công", "success");
            return RedirectToAction("Trash");//Chuyển hướng trang về thùng rác
        }

        // POST: Admin/Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id == null)
            {
                TempData["XMessage"] = new MyMessage("Không có Id", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            Post post = pDAO.getRow(id);
            if (post == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            pDAO.Delete(post);
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
            Post post = pDAO.getRow(id);
            if (post == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            post.Status = (post.Status == 1) ? 2 : 1;
            post.Update_At = DateTime.Now;
            post.Update_By = int.Parse(Session["UserId"].ToString());
            pDAO.Update(post);
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
            Post post = pDAO.getRow(id);
            if (post == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            post.Status = 0;
            post.Update_At = DateTime.Now;

            pDAO.Update(post);
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
            Post post = pDAO.getRow(id);
            if (post == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            post.Status = 2;
            post.Update_At = DateTime.Now;

            pDAO.Update(post);
            TempData["XMessage"] = new MyMessage("Khôi phục mẫu tin thành công", "success");
            return RedirectToAction("Index");//Chuyển hướng trang
        }
    }
}
