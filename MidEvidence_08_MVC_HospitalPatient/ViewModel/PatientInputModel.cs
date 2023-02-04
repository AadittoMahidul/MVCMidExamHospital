using MidEvidence_08_MVC_HospitalPatient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MidEvidence_08_MVC_HospitalPatient.ViewModel
{
    public class PatientInputModel
    {
        public int PatientId { get; set; }
        [Required, StringLength(50), Display(Name = "Patient Name")]
        public string PatientName { get; set; }
        [Required, StringLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, StringLength(50), DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required]
        public HttpPostedFileBase Picture { get; set; }
        [Required, Display(Name = "Hospital Id")]
        public int HospitalId { get; set; }
    }
}