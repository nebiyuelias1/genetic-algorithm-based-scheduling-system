using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.ViewModels
{
    public class DepartmentSectionsViewModel
    {
        public List<Section> Sections { get; set; }
        public string DepartmentName { get; set; }

    }
}
