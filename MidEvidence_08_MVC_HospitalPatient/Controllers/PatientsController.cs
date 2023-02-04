using MidEvidence_08_MVC_HospitalPatient.Models;
using MidEvidence_08_MVC_HospitalPatient.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MidEvidence_08_MVC_HospitalPatient.Controllers
{
    public class PatientsController : Controller
    {
        HospitalDbContext db = new HospitalDbContext();
        // GET: Patients
        public ActionResult Index()
        {
            return View(db.Patients.Include(x => x.Hospital).ToList());
        }
        public ActionResult Create()
        {
            ViewBag.Hospitals = db.Hospitals.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(PatientInputModel c)
        {
            if (ModelState.IsValid)
            {
                var Patient = new Patient
                {
                    PatientName = c.PatientName,
                    Email = c.Email,
                    Phone = c.Phone,
                    HospitalId = c.HospitalId
                };
                string ext = Path.GetExtension(c.Picture.FileName);
                string f = Guid.NewGuid() + ext;
                c.Picture.SaveAs(Server.MapPath("~/Assets/") + f);
                Patient.Picture = f;
                db.Patients.Add(Patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Hospitals = db.Hospitals.ToList();
            return View(c);
        }
        public ActionResult Edit(int id)
        {
            var t = db.Patients.First(x => x.PatientId == id);
            ViewBag.Hospitals = db.Hospitals.ToList();
            ViewBag.CurrentPic = t.Picture;
            return View(new PatientEditModel { PatientId = t.PatientId, PatientName = t.PatientName, Email = t.Email, Phone=t.Phone, HospitalId = t.HospitalId });
        }
        [HttpPost]
        public ActionResult Edit(PatientEditModel t)
        {
            var Patient = db.Patients.First(x => x.PatientId == t.PatientId);
            if (ModelState.IsValid)
            {

                Patient.PatientName = t.PatientName;
                Patient.Email = t.Email;
                Patient.Phone = t.Phone;
                if (t.Picture != null)
                {
                    string ext = Path.GetExtension(t.Picture.FileName);
                    string f = Guid.NewGuid() + ext;
                    t.Picture.SaveAs(Server.MapPath("~/Assets/") + f);
                    Patient.Picture = f;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurrentPic = Patient.Picture;
            ViewBag.Hospitals = db.Hospitals.ToList();
            return View(t);
        }
        public ActionResult Delete(int id)
        {
            return View(db.Patients.Include(x => x.Hospital).First(x => x.PatientId == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Patient t = new Patient { PatientId = id };
            db.Entry(t).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}