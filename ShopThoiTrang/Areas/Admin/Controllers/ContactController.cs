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

namespace ShopThoiTrang.Areas.Admin.Controllers
{
    public class ContactController : Controller
    {
        ContactDAO contDAO = new ContactDAO();

        // GET: Admin/Contact
        public ActionResult Index()
        {
            return View(contDAO.getList());
        }

        // GET: Admin/Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = contDAO.getRow(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Admin/Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Contact/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fullname,Email,Phone,Title,Detail,Replaydetail,Slug,metadesc,Create_At,Create_By,Update_At,Update_By,Status")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.Create_At = DateTime.Now;
                contDAO.Insert(contact);
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: Admin/Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = contDAO.getRow(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Admin/Contact/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fullname,Email,Phone,Title,Detail,Replaydetail,Slug,metadesc,Create_At,Create_By,Update_At,Update_By,Status")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.Update_At = DateTime.Now;
                contDAO.Update(contact);
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Admin/Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = contDAO.getRow(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Admin/Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = contDAO.getRow(id);
            contDAO.Delete(contact);
            return RedirectToAction("Index");
        }

    }
}
