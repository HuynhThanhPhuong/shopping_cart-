using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyLibrary.DAO;

namespace ShopThoiTrang.Controllers
{
    public class ModuleController : Controller
    {
        MenuDAO menuDAO = new MenuDAO();
        SliderDAO sliderDAO = new SliderDAO();
        CategoryDAO cateDAO = new CategoryDAO();
        PostDAO postDAO = new PostDAO();
        // GET: Module
        public ActionResult MainMenu()
        {
            var list = menuDAO.getList(0,"mainmenu");
            return View("MainMenu",list);
        }
        public ActionResult Slideshow()
        {
            var list = sliderDAO.getList("slideshow ");
            return View("Slideshow",list) ;
        }
        public ActionResult ListCategory()
        {
            var list = cateDAO.getList(0);
            return View("ListCategory", list);
        }
        public ActionResult ProductHot()
        {
            return View("ProductHot");
        }
        public ActionResult CategoryList()
        {
            var list = cateDAO.getList(0);
            return View("CategoryList", list);
        }
        public ActionResult LastNew()
        {
            var list = postDAO.getList(1);
            return View("LastNew", list);
        }
    }
}