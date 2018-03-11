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
    public partial class NewSection : Form
    {
        private SchedulingContext _context; 
        public NewSection()
        {
            InitializeComponent();
            _context = new SchedulingContext();
        }

        private void NewSection_Load(object sender, EventArgs e)
        {
            List<Department> departments = _context.Departments.ToList();

            departmentDropdown.DataSource = departments;
            departmentDropdown.DisplayMember = "Name";
            departmentDropdown.ValueMember = "Id"; 

        }

        private void registerSectionButton_Click(object sender, EventArgs e)
        {
            // TODO - Validation must be implemented for this form 

            var section = new Section
            {
                Name = nameValue.Text,
                EntranceYear = int.Parse(entranceYearValue.Text),
                StudentCount = int.Parse(studentCountValue.Text),
                DepartmentId = int.Parse(departmentDropdown.SelectedValue.ToString())
            };

            _context.Sections.Add(section);
            _context.SaveChanges();
            
        }
    }
}
