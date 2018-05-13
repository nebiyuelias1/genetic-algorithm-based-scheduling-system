using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchedulingSystemClassLibrary.Models;

namespace SchedulingSystemClassLibrary.ViewModels
{
    public class LabAssistantsFormViewModel
    {
        public LabAssistantsFormViewModel()
        {

        }
        public LabAssistantsFormViewModel(LabAssistant labAssistant)
        {
            this.Id = labAssistant.Id;
            this.FirstName = labAssistant.FirstName;
            this.FatherName = labAssistant.FatherName;
            this.GrandFatherName = labAssistant.GrandFatherName;
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }

        [Required]
        [Display(Name = "Grand Father Name")]
        public string GrandFatherName { get; set; }
    }
}
