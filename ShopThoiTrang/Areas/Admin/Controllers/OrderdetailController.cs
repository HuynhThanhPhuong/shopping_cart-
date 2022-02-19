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
    public class OrderdetailController : Controller
    {
        OderdetailDAO odetail = new OderdetailDAO();

        // GET: Admin/Orderdetail
        public ActionResult Index()
        {
            return View(odetail.getList());
        }

        // GET: Admin/Orderdetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orderdetail orderdetail = odetail.getRow(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            return View(orderdetail);
        }

        // GET: Admin/Orderdetail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Orderdetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Orderid,quantity,Productid,Price,Amount")] Orderdetail orderdetail)
        {
            if (ModelState.IsValid)
            {
                odetail.Insert(orderdetail);
                return RedirectToAction("Index");
            }

            return View(orderdetail);
        }

        // GET: Admin/Orderdetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orderdetail orderdetail = odetail.getRow(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            return View(orderdetail);
        }

        // POST: Admin/Orderdetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Orderid,quantity,Productid,Price,Amount")] Orderdetail orderdetail)
        {
            if (ModelState.IsValid)
            {
                odetail.Update(orderdetail);
                return RedirectToAction("Index");
            }
            return View(orderdetail);
        }

        // GET: Admin/Orderdetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orderdetail orderdetail = odetail.getRow(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            return View(orderdetail);
        }

        // POST: Admin/Orderdetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orderdetail orderdetail = odetail.getRow(id);
            odetail.Delete(orderdetail);
            return RedirectToAction("Index");
        }
    }
}
