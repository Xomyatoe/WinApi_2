using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LabelTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            if(cbShowDate.Checked ) 
            {
                LabelTime.Text += $"\n{DateTime.Today.ToString("yyyy.MM.dd")}";
            }
        }
    }
}
