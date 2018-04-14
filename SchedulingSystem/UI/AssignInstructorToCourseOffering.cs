using SchedulingSystemClassLibrary.GeneticAlgorithm;
using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedulingSystem.UI
{
    public partial class AssignInstructorToCourseOffering : Form
    {
        private SchedulingSystemClassLibrary.SchedulingContext _context; 
        public AssignInstructorToCourseOffering()
        {
            InitializeComponent();
            _context = new SchedulingSystemClassLibrary.SchedulingContext();
            new GeneticAlgorithm(); 
        }

        private void AssignInstructorToCourseOffering_Load(object sender, EventArgs e)
        {
            List<CourseOffering> courseOfferings = _context.CourseOfferings.ToList();

            courseOfferingsListBox.DataSource = courseOfferings;
            courseOfferingsListBox.DisplayMember = "Name";
            courseOfferingsListBox.ValueMember = "Id";

            List<Instructor> instructors = _context.Instructors.ToList();

            instructorsDropdown.DataSource = instructors;
            instructorsDropdown.DisplayMember = "FirstName";
            instructorsDropdown.ValueMember = "Id";
        }

        private void assignButton_Click(object sender, EventArgs e)
        {
            // TODO: Validation 

            int selectedCourseOfferingid = int.Parse(courseOfferingsListBox.SelectedValue.ToString());
            var courseOffering = _context.CourseOfferings.SingleOrDefault(c => c.Id == selectedCourseOfferingid);

            courseOffering.InstructorId = int.Parse(instructorsDropdown.SelectedValue.ToString());

            _context.SaveChanges(); 
        }
    }
}
