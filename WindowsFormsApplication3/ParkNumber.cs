using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUB
{
    public partial class number : Form
    {
        public number()
        {
            InitializeComponent();
        }

        private void ParkNumber_Load(object sender, EventArgs e)
        {

        }

        private void parkNumberHolder(object sender, EventArgs e)
        {

        }

        private void enterParkNumber(object sender, EventArgs e)
        {
            int a = (int) parkNumber.Value;
        }
    }
}
