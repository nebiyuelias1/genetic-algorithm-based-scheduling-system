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
    public partial class NewCurriculum : Form
    {
        SchedulingContext _context;
        public NewCurriculum()
        {
            _context = new SchedulingContext(); 
            InitializeComponent();
        }

        private void NewCurriculum_Load(object sender, EventArgs e)
        {
            List<Department> departments = _context.Departments.ToList();

            departmentDropdown.DataSource = departments;
            departmentDropdown.DisplayMember = "Name";
            departmentDropdown.ValueMember = "Id";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // TODO - Validate 

            var curriculum = new Curriculum
            {
                FieldOfStudy = fieldOfStudyValue.Text,
                Nomenclature = nomenclatureValue.Text,
                AdmissionClassification = admissionClassificationValue.Text,
                Program = programValue.Text,
                StayYear = byte.Parse(stayYearValue.Text),
                StaySemester = byte.Parse(staySemesterValue.Text),
                MinimumCredit = int.Parse(minimumCreditValue.Text),
                DepartmentId = int.Parse(departmentDropdown.SelectedValue.ToString())
            };

            _context.Curriculums.Add(curriculum);
            _context.SaveChanges(); 
        }
    }
}
