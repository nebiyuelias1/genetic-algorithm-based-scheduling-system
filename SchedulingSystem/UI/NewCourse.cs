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
    public partial class NewCourse : Form
    {
        SchedulingContext _context; 
        public NewCourse()
        {
            InitializeComponent();
            _context = new SchedulingContext();
        }

        private void registerCourseButton_Click(object sender, EventArgs e)
        {
            // TODO - You must validate the form before sending it to the database

            

            var course = new Course
            {
                CourseCode = courseCodeValue.Text,
                Title = titleValue.Text, 
                Credit = byte.Parse(creditValue.Text), 
                Lecture = byte.Parse(lectureValue.Text), 
                Laboratory = byte.Parse(laboratoryValue.Text), 
                Tutor = byte.Parse(tutorValue.Text), 
                DeliveryYear = int.Parse(deliveryYearValue.Text), 
                DeliverySemester = int.Parse(deliverySemesterValue.Text), 
                CurriculumId = int.Parse(curriculumDropdown.SelectedValue.ToString())

            };

            _context.Courses.Add(course);
            _context.SaveChanges();
            
        }

        private void NewCourse_Load(object sender, EventArgs e)
        {
            List<Curriculum> curriculums = _context.Curriculums.ToList();

            curriculumDropdown.DataSource = curriculums;
            curriculumDropdown.DisplayMember = "Nomenclature";
            curriculumDropdown.ValueMember = "Id";
             
        }
    }
}
