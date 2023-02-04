using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MidEvidence_08_MVC_HospitalPatient.Models
{
    public enum Department { CARDIAC_SURGERY = 1, COVID_WING, DIETETICS_NUTRITION, ORTHOPEDICS, NEUROLOGY, UROLOGY }
    public class Hospital
    {
        [Display(Name = "Hospital Id")]
        public int HospitalId { get; set; }
        [Required, StringLength(25), Display(Name = "Hospital Name")]
        public string HospitalName { get; set; }
        [EnumDataType(typeof(Department))]
        public Department Department { get; set; }
        [Required, Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public Decimal Payment { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Admit Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AdmitDate { get; set; }
        [Column(TypeName = "date"), Display(Name = "End Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Service Continuing?")]
        public bool IsContinuing { get; set; }

        public ICollection<Patient> Patients { get; set; } = new List<Patient>();
    }
    public class Patient
    {
        public int PatientId { get; set; }
        [Required, StringLength(50), Display(Name = "Patient Name")]
        public string PatientName { get; set; }
        [Required, StringLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, StringLength(50), DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required, StringLength(150), Display(Name = "Picture")]
        public string Picture { get; set; }
        [Required]
        public int HospitalId { get; set; }
        [ForeignKey("HospitalId")]
        public Hospital Hospital { get; set; }
    }
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext()
        {
            Database.SetInitializer(new HospitalDbInitializer());
        }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Patient> Patients { get; set; }

    }
    public class HospitalDbInitializer : DropCreateDatabaseIfModelChanges<HospitalDbContext>
    {
        protected override void Seed(HospitalDbContext context)
        {
            Hospital h = new Hospital { HospitalName = "Popular Hospital", Department = Department.CARDIAC_SURGERY, Payment = 15000.00M, AdmitDate = DateTime.Now.AddDays(-5 * 30), IsContinuing = false };
            h.Patients.Add(new Patient { PatientName = "Imrul Kayes", Picture = "1.jpg", Email = "imrul@gamil.com", Phone = "01920-121412" });
            h.Patients.Add(new Patient { PatientName = "Al Hasan", Picture = "2.jpg", Email = "imrul@gamil.com", Phone = "01920-121413" });

            context.Hospitals.Add(h);
            context.SaveChanges();
        }
    }
}