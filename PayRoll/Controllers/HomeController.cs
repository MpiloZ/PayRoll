using PayRoll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace PayRoll.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListAllCompanies()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult CompanyDetails(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                id = int.Parse( Request.Url.AbsolutePath.Substring(Request.Url.AbsolutePath.LastIndexOf('/')+1));

                ApplicationDbContext db = new ApplicationDbContext();

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Company c = db.Companies.Find(id);

                if (c == null)
                {
                    return HttpNotFound();
                }
                return View(c);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult ListAllEmployees()
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationDbContext db = new ApplicationDbContext();

                return View(db.Employees.ToList());
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult ListSouthAfricanEmployees()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<Employee> l = new List<Employee>();

                ApplicationDbContext db = new ApplicationDbContext();

                for(int i = 0; i < db.Employees.ToList().Count; i++)
                {
                    var e = db.Employees.ToList()[i];

                    if (e.HomeAddress?.Country == "South Africa")
                    {
                        l.Add(e);
                    }
                }

                return View(l);
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult LoadZAEmployees()
        {
            var allValues = Request.Form.AllKeys;
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;



            var Name = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            var LastName = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
            var Position = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
            var Country = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var c = (from a in db.Employees where a.HomeAddress.Country == "South Africa" select new { a.Name, a.Lastname, a.Position, a.HomeAddress.Country });

                if (!string.IsNullOrEmpty(Name))
                {
                    c = c.Where(x => x.Name.ToLower().Contains(Name.ToLower()));
                }

                recordsTotal = c.Count();
                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
                {
                    c = c.OrderBy(sortColumn + " " + sortColumnDir);
                }
                else
                {
                    c = c.OrderByDescending(x => x.Name);
                }

                var data = c.Skip(skip).Take(pageSize).ToList();


                return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult LoadEmployees()
        {
            var allValues = Request.Form.AllKeys;
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            

            var Name = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            var LastName = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
            var Position = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
            var Country = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var c = (from a in db.Employees select new { a.Name, a.Lastname, a.Position, a.HomeAddress.Country });

                if (!string.IsNullOrEmpty(Name))
                {
                    c = c.Where(x => x.Name.ToLower().Contains(Name.ToLower()));
                }

                recordsTotal = c.Count();
                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
                {
                    c = c.OrderBy(sortColumn + " " + sortColumnDir);
                }
                else
                {
                    c = c.OrderByDescending(x => x.Name);
                }

                var data = c.Skip(skip).Take(pageSize).ToList();


                return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult LoadCompanies()
        {
            var allValues = Request.Form.AllKeys;
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var companyName = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();

            var Name = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault(); ;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var c = (from a in db.Companies select a);

                if (!string.IsNullOrEmpty(Name))
                {
                    c = c.Where(x => x.Name.ToLower().Contains(Name.ToLower()));
                }

                recordsTotal = c.Count();
                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
                {
                    c = c.OrderBy(sortColumn + " " + sortColumnDir);
                }
                else
                {
                    c = c.OrderByDescending(x => x.Name);
                }

                var data = c.Skip(skip).Take(pageSize).ToList();


                return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}