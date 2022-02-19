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
    public class LinkController : Controller
    {
        LinkDAO linkDAO = new LinkDAO();

        // GET: Admin/Link
        public ActionResult Index()
        {
            return View(linkDAO.getList("Index"));
        }
        public ActionResult Trash()
        {
            return View(linkDAO.getList("Trash"));
        }
        // GET: Admin/Link/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = linkDAO.getRow(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // GET: Admin/Link/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Link/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Link link)
        {
            if (ModelState.IsValid)
            {
                linkDAO.Insert(link);
                return RedirectToAction("Index");
            }

            return View(link);
        }

        // GET: Admin/Link/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = linkDAO.getRow(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // POST: Admin/Link/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Slug,TypeLink,Tableld,Status")] Link link)
        {
            if (ModelState.IsValid)
            {
                linkDAO.Update(link);
                return RedirectToAction("Index");
            }
            return View(link);
        }

        // GET: Admin/Link/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = linkDAO.getRow(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // POST: Admin/Link/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Link link = linkDAO.getRow(id);
            linkDAO.Delete(link);
            return RedirectToAction("Index");
        }

    }
}
