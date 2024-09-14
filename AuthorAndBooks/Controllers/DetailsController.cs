using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthorAndBooks.Data;
using AuthorAndBooks.Models;


namespace AuthorAndBooks.Controllers
{
    public class DetailsController : Controller
    {
        // GET: Details
        public ActionResult ViewDetail(Guid id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var detail = session.Query<AuthorDetail>().FirstOrDefault(d => d.Author.Id == id);
                return View(detail);
            }

        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(AuthorDetail detail)
        {
            if (ModelState.IsValid)
            {
                using (var session = NHibernateHelper.CreateSession())
                {
                    using (var txn = session.BeginTransaction())
                    {
                        var authId = (Guid)(Session["CurrentAuthorId"]);
                        detail.Author = session.Query<Author>().FirstOrDefault(a => a.Id == authId);
                        session.Save(detail);
                        txn.Commit();
                        return RedirectToAction("viewdetail", new { id = authId });
                    }
                }
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var detail = session.Query<AuthorDetail>().FirstOrDefault(d => d.Id == id);
                return View(detail);
            }
        }

        [HttpPost]
        public ActionResult Edit(AuthorDetail detail)
        {
            if (ModelState.IsValid)
            {
                using (var session = NHibernateHelper.CreateSession())
                {
                    using (var txn = session.BeginTransaction())
                    {

                        var id = (Guid)(Session["CurrentAuthorId"]);
                        var existingDetail = session.Query<AuthorDetail>().FirstOrDefault(d => d.Author.Id == id);
                        existingDetail.Street = detail.Street;
                        existingDetail.City = detail.City;
                        existingDetail.State = detail.State;
                        existingDetail.Country = detail.Country;
                        session.Update(existingDetail);
                        txn.Commit();
                        return RedirectToAction("viewdetail", new { id = id });
                    }
                }
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var detail = session.Query<AuthorDetail>().FirstOrDefault(d => d.Id == id);
                return View(detail);
            }
        }
        [HttpPost, ActionName("delete")]
        public ActionResult DeleteDetail(int id)
        {
            var authId = (Guid)(Session["CurrentAuthorId"]);
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var detail = session.Query<AuthorDetail>().FirstOrDefault(d => d.Id == id);
                    session.Delete(detail);
                    txn.Commit();
                    return RedirectToAction("viewdetail", new { id = authId });
                }
            }
        }
    }
}