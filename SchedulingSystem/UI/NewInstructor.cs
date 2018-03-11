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
    public partial class NewInstructor : Form
    {
        SchedulingContext _context; 
        public NewInstructor()
        {
            InitializeComponent();
            _context = new SchedulingContext(); 
        }

        private void NewInstructor_Load(object sender, EventArgs e)
        {
            List<Department> departments = _context.Departments.ToList();

            departments.Insert(0, new Department());

            departmentDropdown.DataSource = departments;
            departmentDropdown.DisplayMember = "Name";
            departmentDropdown.ValueMember = "Id"; 
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Instructor instructor = new Instructor
            {
                FirstName = firstNameValue.Text,
                FatherName = fatherNameValue.Text,
                GrandFatherName = grandFatherNameValue.Text,
                DepartmentId = int.Parse(departmentDropdown.SelectedValue.ToString())
            };

            _context.Instructors.Add(instructor);
            _context.SaveChanges(); 
        }
    }
}
