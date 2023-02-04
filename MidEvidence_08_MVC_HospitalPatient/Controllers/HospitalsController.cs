using MidEvidence_08_MVC_HospitalPatient.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MidEvidence_08_MVC_HospitalPatient.Controllers
{
    public class HospitalsController : Controller
    {
        HospitalDbContext db = new HospitalDbContext();
        // GET: Hospitals
        public ActionResult Index()
        {
            return View(db.Hospitals.Include(h => h.Patients).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        public PartialViewResult CreateHospital()
        {
            return PartialView("_CreateHospital");
        }
        [HttpPost]
        public PartialViewResult CreateHospital(Hospital h)
        {
            if (ModelState.IsValid)
            {
                db.Hospitals.Add(h);
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public PartialViewResult EditHospital(int id)
        {
            var b = db.Hospitals.First(x => x.HospitalId == id);
            return PartialView("_EditHospital", b);
        }
        [HttpPost]
        public PartialViewResult EditHospital(Hospital h)
        {
            Thread.Sleep(2000);
            if (ModelState.IsValid)
            {
                db.Entry(h).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Delete(int id)
        {
            return View(db.Hospitals.First(x => x.HospitalId == id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DoDelete(int HospitalId)
        {
            var b = new Hospital { HospitalId = HospitalId };
            db.Entry(b).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}