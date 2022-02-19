using MyLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyLibrary.DAO;

namespace ShopThoiTrang.Controllers
{
    public class CartController : Controller
    {
        private string CartSession = "CartSession";
        OderdetailDAO oderdetailDAO = new OderdetailDAO();
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = oderdetailDAO.getList();
            if(cart!=null)
            {
                list = oderdetailDAO.getList();
            }
            return View(list);
        }
        public ActionResult AddItem(int productid,int quantity)
        {
            var proDAO = new ProductDAO().getList(productid);
            var cart = Session[CartSession];
            if(cart!=null)
            {
                var list = oderdetailDAO.getList();
                if (list.Exists(x=>x.Productid==productid))
                {
                    foreach (var item in list)
                    {
                        if (item.Productid == productid)
                        {
                            item.quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new Orderdetail();
                    item.Productid = productid;
                    item.quantity = quantity;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                var item = new Orderdetail();
                item.Productid = productid;
                item.quantity = quantity;

                var list = new List<Orderdetail>();
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
    }
}