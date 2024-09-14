using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using AuthorAndBooks.Data;
using AuthorAndBooks.Models;

namespace AuthorAndBooks.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult ViewBooks(Guid id)
        {
            
            using (var sesh = NHibernateHelper.CreateSession())
            {
                var books = sesh.Query<Book>().Where(x => x.Author.Id == id).ToList();
                return View(books);
            }

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                using (var s = NHibernateHelper.CreateSession())
                {
                    using (var txn = s.BeginTransaction())
                    {
                        var id = (Guid)(Session["CurrentAuthorId"]);
                        book.Author = s.Query<Author>().FirstOrDefault(x => x.Id == id);
                        s.Save(book);
                        txn.Commit();
                        return RedirectToAction("viewbooks", new { id });
                    }
                }
            }
            return View();

        }

        public ActionResult Edit(int id)
        {
            using (var s = NHibernateHelper.CreateSession())
            {
                var book = s.Query<Book>().FirstOrDefault(b => b.Id == id);
                return View(book);
            }
        }
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            var id = (Guid)(Session["CurrentAuthorId"]);
            
            if (ModelState.IsValid)
            {
                using(var s =  NHibernateHelper.CreateSession())
                {
                    using (var txn = s.BeginTransaction())
                    {
                        var author = s.Query<Author>().FirstOrDefault(a => a.Id == id);
                        var existingBook = s.Query<Book>().FirstOrDefault(b=>b.Author.Id == id);
                        existingBook.Name = book.Name;
                        existingBook.Description = book.Description;
                        existingBook.Genre = book.Genre;
                        existingBook.Author = author;
                        s.Update(existingBook);
                        txn.Commit();
                        return RedirectToAction("viewbooks", new {id});
                    }
                }
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            using(var s = NHibernateHelper.CreateSession())
            {
                var book = s.Query<Book>().FirstOrDefault(b=>b.Id==id);
                return View(book);
            }

        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteBook(int id)
        {
            using (var s = NHibernateHelper.CreateSession())
            {
                using(var txn = s.BeginTransaction())
                {
                    var book = s.Query<Book>().FirstOrDefault(b => b.Id == id);
                    s.Delete(book);
                    txn.Commit();
                    return RedirectToAction("viewbooks", new { id = (Guid)(Session["CurrentAuthorId"])});
                }
            }
        }
    }
}