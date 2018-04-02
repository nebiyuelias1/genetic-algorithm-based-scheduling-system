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
    public partial class NewRoom : Form
    {
        private SchedulingContext _context; 
        public NewRoom()
        {
            InitializeComponent();
            _context = new SchedulingContext(); 
        }

        private void registerSectionButton_Click(object sender, EventArgs e)
        {
            var room = new Room
            {
                Name = nameValue.Text, 
                //Building = buildingValue.Text, 
                Size = int.Parse(sizeValue.Text)
            };

            _context.Rooms.Add(room);
            _context.SaveChanges(); 
        }
    }
}
