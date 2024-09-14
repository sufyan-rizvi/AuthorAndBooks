using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AuthorAndBooks.Data;
using AuthorAndBooks.Models;
using AuthorAndBooks.ViewModels;
using NHibernate.Linq;
using BC = BCrypt.Net;

namespace AuthorAndBooks.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        
        [AllowAnonymous]
        public ActionResult Index()
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var authors = session.Query<Author>().FetchMany(a=>a.Books).ToList();
                return View(authors);
            }
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(AuthorLoginVM loginVM)
        {
            if(!ModelState.IsValid)
                return View();
            
            using( var s = NHibernateHelper.CreateSession())
            {
                using(var txn = s.BeginTransaction())
                {
                    var author = s.Query<Author>().FirstOrDefault(u=>u.Name == loginVM.Name);
                    if(author != null)
                    {
                        if (BC.BCrypt.Verify(loginVM.Password, author.Password))
                        {
                            FormsAuthentication.SetAuthCookie(loginVM.Name, false);
                            Session["CurrentAuthorId"] = author.Id;
                            return RedirectToAction("Index");
                        }

                    }
                    ModelState.AddModelError("", "Username and Password don't match");
                    return View();

                }
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();  
            return RedirectToAction("index");
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Author author)
        {
            if(ModelState.IsValid)
            {
                using(var session = NHibernateHelper.CreateSession())
                {
                    using(var txn =  session.BeginTransaction())
                    {
                        //author.Detail.Author = author;
                        author.Password = BC.BCrypt.HashPassword(author.Password);
                        session.Save(author);
                        txn.Commit();
                        return RedirectToAction("index");
                    }
                }
            }
            return View();
        }

        public ActionResult Edit()
        {
            var id = (Guid)Session["CurrentAuthorId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                var author = session.Query<Author>().FirstOrDefault(a => a.Id == id);
                return View(author);
            }
        }
        [HttpPost]
        public ActionResult Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                using (var session = NHibernateHelper.CreateSession())
                {
                    using (var txn = session.BeginTransaction())
                    {
                        author.Detail.Author = author; 
                        session.Update(author);
                        txn.Commit();
                        return RedirectToAction("index");
                    }
                }
            }
            return View();
        }

        public ActionResult Delete()
        {
            var id = (Guid)Session["CurrentAuthorId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                var author = session.Query<Author>().FirstOrDefault(a => a.Id == id);
                TempData["CurrentAuthor"] = author;
                return View(author);
            }
        }

        [HttpPost,ActionName("delete")]

        public ActionResult DeleteAuthor()
        {
            using(var session = NHibernateHelper.CreateSession())
            {
                using(var txn = session.BeginTransaction())
                {
                    var author = (Author)TempData["CurrentAuthor"];
                    session.Delete(author);
                    txn.Commit();
                    return RedirectToAction("index");
                }
            }
        }
    }
}