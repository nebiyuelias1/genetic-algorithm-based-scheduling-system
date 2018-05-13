using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string AccountId { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {FatherName} {GrandFatherName}";
            }
        }
    }
}
