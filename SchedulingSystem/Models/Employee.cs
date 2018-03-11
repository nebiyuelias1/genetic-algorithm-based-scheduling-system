using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string Sex { get; set; }
        public DateTime DOB { get; set; }
        public DateTime HiredDate { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
