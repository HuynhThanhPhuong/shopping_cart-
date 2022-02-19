using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyLibrary;
using MyLibrary.Model;
using MyLibrary.DAO;
using PagedList;

namespace ShopThoiTrang.Controllers
{
    public class SiteController : Controller
    {
        LinkDAO linkDAO = new LinkDAO();
        ProductDAO productDAO = new ProductDAO();
        PostDAO postDAO = new PostDAO();
        CategoryDAO categoryDAO=new CategoryDAO();
        TopicDAO topicDAO=new TopicDAO();
        ContactDAO contDAO = new ContactDAO();
        OderdetailDAO oderdetailDAO = new OderdetailDAO();
        Contact contact = new Contact();
        // GET: Site
        public ActionResult Index(String slug="",int? page=1)
        {
            
            if(slug=="")
            {
                return this.Home();
            }
            else
            {
                Link row_link = linkDAO.getRow(slug);
                if(row_link!=null)
                {
                    string typelink = row_link.TypeLink;
                    switch(typelink)
                    {
                        case "category": { return this.ProductCategory(slug,page); }
                        case "topic": { return this.PostTopic(slug); }
                        case "page": { return this.PostPage(slug); }
                    }
                }
                else
                {
                    if(productDAO.getRow(slug)!=null)
                    {
                        return this.ProductDetail(slug);
                    }
                    if (postDAO.getRow(slug) != null)
                    {
                        return this.PostDetail(slug);
                    }
                    return this.Error404(slug);
                }
            }
            return this.Home();
        }
        public ActionResult Home()
        {
            var listcategory = categoryDAO.getList(0);
            return View("Home", listcategory);
        }
        public ActionResult ProductHome(int catid, string name)
        {
            ViewBag.NameCat = name;
            List<int> listcatid = categoryDAO.getListId(catid);
            int limit = 4;
            var list = productDAO.getList(listcatid, limit);
            return View("ProductHome", list);
        }
       
        public ActionResult Product(int? page)
        {
            int pageSize = 12;
            int pageNumber = page ?? 1;
            var list = productDAO.getList(pageSize, pageNumber);
            return View("Product", list);
        }
        public ActionResult ProductCategory(String slug,int? page)
        {
            int pageSize = 12;
            int pageNumber = page ?? 1;
            var row_cat = categoryDAO.getRow(slug);
            int catid = row_cat.Id;
            List<int> listcatid = categoryDAO.getListId(catid);
            var list = productDAO.getList(listcatid, pageSize, pageNumber);
            ViewBag.Slug = slug;
            ViewBag.Title = row_cat.Name;
            return View("ProductCategory", list);
        }
        public ActionResult ProductDetail(String slug)
        {
            int limit = 8;
            var row = productDAO.getRow(slug);
            int catid = row.Catid;//sp thuoc loai nao
            List<int> listcatid = categoryDAO.getListId(catid);
            var listother = productDAO.getList(listcatid,limit,row.Id,true);
            ViewBag.ListOther = listother;
            return View("ProductDetail", row);
        }
        public ActionResult Post()
        {
            return View("Post");
        }
        public ActionResult PostTopic(String slug)
        {
            var row_topic = topicDAO.getRow(slug);
            ViewBag.Title = row_topic.Name;
            int topicid = row_topic.Id;
            ViewBag.ListOther = postDAO.getList(topicid);
            return View("PostTopic", row_topic);
        }
        public ActionResult PostDetail(String slug)
        {
            int limit = 10;
            var row = postDAO.getRow(slug);
            int? topid = row.Topicid;
            ViewBag.Title = row.Title;
            ViewBag.ListOther = postDAO.getList(topid,limit,row.Id);
            return View("PostDetail", row);
        }
        public ActionResult PostPage(String slug)
        {
            var row = postDAO.getRow(slug);
            return View("PostPage", row);
        }
        public ActionResult Error404(String slug)
        {
            return View("Error404");
        }
        public ActionResult Contact()
        {
            var list = contDAO.getList();
            return View("Contact", list);
        }
    }
}