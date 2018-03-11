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
    public partial class AssignSectionToRoom : Form
    {
        private SchedulingContext _context; 
        public AssignSectionToRoom()
        {
            InitializeComponent();
            _context = new SchedulingContext();


            List<Section> sections = _context.Sections.ToList();
            List<Room> rooms = _context.Rooms.ToList();

            sectionDropdown.DataSource = sections;
            sectionDropdown.DisplayMember = "Name";
            sectionDropdown.ValueMember = "Id";

            roomDropdown.DataSource = rooms;
            roomDropdown.DisplayMember = "Name";
            roomDropdown.ValueMember = "Id";
        }

        private void AssignSectionToRoom_Load(object sender, EventArgs e)
        {

        }

        private void assignSectionToRoomButton_Click(object sender, EventArgs e)
        {
            int sectionId = int.Parse(sectionDropdown.SelectedValue.ToString());
            int roomId = int.Parse(roomDropdown.SelectedValue.ToString()); 

            var section = _context.Sections.SingleOrDefault(s => s.Id == sectionId); 
            var room = _context.Rooms.SingleOrDefault(r => r.Id == roomId);

            section.AssignedRooms.Add(room);
            _context.SaveChanges();
        }
    }
}
