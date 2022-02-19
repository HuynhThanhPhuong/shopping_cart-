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
    public class TopicController : Controller
    {
        TopicDAO topDAO = new TopicDAO();

        // GET: Admin/Topic
        public ActionResult Index()
        {
            return View(topDAO.getList("Index"));
        }
        public ActionResult Trash()
        {
            return View(topDAO.getList("Trash"));
        }
        // GET: Admin/Topic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = topDAO.getRow(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Admin/Topic/Create
        public ActionResult Create()
        {
            ViewBag.ListTopic = new SelectList(topDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(topDAO.getList("Index"), "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/Topic/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Topic topic)
        {
            if (ModelState.IsValid)
            {
                topic.Slug = MyString.Str_slug(topic.Name);
                topic.Create_At = DateTime.Now;
                topic.Create_By = int.Parse(Session["UserId"].ToString());
                topic.Update_At = DateTime.Now;
                topic.Update_By = int.Parse(Session["UserId"].ToString());
                topDAO.Insert(topic);
                return RedirectToAction("Index");
            }
            ViewBag.ListTopic = new SelectList(topDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(topDAO.getList("Index"), "Orders", "Name", 0);
            return View(topic);
        }

        // GET: Admin/Topic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = topDAO.getRow(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListTopic = new SelectList(topDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(topDAO.getList("Index"), "Orders", "Name", 0);
            return View(topic);
        }

        // POST: Admin/Topic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Topic topic)
        {
            if (ModelState.IsValid)
            {
                topic.Slug = MyString.Str_slug(topic.Name);
                topic.Update_At = DateTime.Now;
                topic.Update_By = int.Parse(Session["UserId"].ToString());
                topDAO.Update(topic);
                return RedirectToAction("Index");
            }
            ViewBag.ListTopic = new SelectList(topDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(topDAO.getList("Index"), "Orders", "Name", 0);
            return View(topic);
        }

        // GET: Admin/Topic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = topDAO.getRow(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Admin/Topic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id == null)
            {
                TempData["XMessage"] = new MyMessage("Không có Id", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            Topic topic = topDAO.getRow(id);
            if (topic == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            topDAO.Delete(topic);
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
            Topic topic = topDAO.getRow(id);
            if (topic == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            topic.Status = (topic.Status == 1) ? 2 : 1;
            topic.Update_At = DateTime.Now;
            topic.Update_By = int.Parse(Session["UserId"].ToString());
            topDAO.Update(topic);
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
            Topic topic = topDAO.getRow(id);
            if (topic == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            topic.Status = 0;
            topic.Update_At = DateTime.Now;
            topic.Update_By = int.Parse(Session["UserId"].ToString());
            topDAO.Update(topic);
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
            Topic topic = topDAO.getRow(id);
            if (topic == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");//Chuyển hướng trang
            }
            topic.Status = 2;
            topic.Update_At = DateTime.Now;
            topic.Update_By = int.Parse(Session["UserId"].ToString());
            topDAO.Update(topic);
            TempData["XMessage"] = new MyMessage("Khôi phục mẫu tin thành công", "success");
            return RedirectToAction("Index");//Chuyển hướng trang
        }

    }
}
