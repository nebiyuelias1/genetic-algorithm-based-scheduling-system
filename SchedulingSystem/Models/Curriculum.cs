using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystem.Models
{
    public class Curriculum
    {
        public int Id { get; set; }
        public string FieldOfStudy { get; set; }
        public string Nomenclature { get; set; }
        public string AdmissionClassification { get; set; }
        public string Program { get; set; }
        public byte StayYear { get; set; }
        public byte StaySemester { get; set; }
        public int MinimumCredit { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
