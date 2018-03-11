using SchedulingSystem.Models;
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
    public partial class CreateCourseOffering : Form
    {
        SchedulingContext _context;
        public CreateCourseOffering()
        {
            InitializeComponent();
            _context = new SchedulingContext(); 
        }

        private void CreateCourseOffering_Load(object sender, EventArgs e)
        {
            List<Course> courses = _context.Courses.ToList();
            List<Section> sections = _context.Sections.ToList();

            courseDropdown.DataSource = courses;
            courseDropdown.DisplayMember = "Title";
            courseDropdown.ValueMember = "Id";

            sectionDropdown.DataSource = sections;
            sectionDropdown.DisplayMember = "Name";
            sectionDropdown.ValueMember = "Id"; 
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            // TODO: Validation 

            var courseOffering = new CourseOffering
            {
                CourseId = int.Parse(courseDropdown.SelectedValue.ToString()), 
                SectionId = int.Parse(sectionDropdown.SelectedValue.ToString()), 
                Name = $"{((Course)courseDropdown.SelectedItem).Title}-{((Section)sectionDropdown.SelectedItem).Name}-{yearValue.Text}-{semesterValue.Text}", 
                Year = int.Parse(yearValue.Text), 
                Semester = int.Parse(semesterValue.Text)
            };

            _context.CourseOfferings.Add(courseOffering);
            _context.SaveChanges(); 
        }
    }
}
