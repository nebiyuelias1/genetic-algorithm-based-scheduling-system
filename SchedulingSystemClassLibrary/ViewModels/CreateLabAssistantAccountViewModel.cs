using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.ViewModels
{
    public class CreateLabAssistantAccountViewModel
    {
        public LabAssistant LabAssistant { get; set; }

        [EmailAddress]
        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        public int LabAssistantId { get; set; }

        public IEnumerable<string> Errors { get; set; }

    }
}
