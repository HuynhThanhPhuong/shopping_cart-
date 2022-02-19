using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyLibrary.DAO;
using MyLibrary.Model;
using ShopThoiTrang.Library;

namespace ShopThoiTrang.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        UserDAO userDAO = new UserDAO();
        // GET: Admin/Auth
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection filed)
        {
            String user = filed["username"];
            String pass = MyString.ToMD5(filed["password"]);
            String error = "";
            User user_row = userDAO.getRow(user);
            if(user_row!=null)
            {
                if(user_row.Password.Equals(pass))  
                {
                    Session["UserAdmin"] = user_row.Username;
                    Session["UserId"] = user_row.Id.ToString();
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    error = "Mật khẩu không chính xác";
                }
            }
            else
            {
                error = "Tên đăng nhập không tồn tại";
            }
            ViewBag.StrError = "<div class='text-danger'>" + error + "</div>";
            return View();
        }
        public ActionResult Logout()
        {
            Session["UserAdmin"] = "";
            Session["UserId"] = "";
            return Redirect("~/Admin/login");
        }
    }
}