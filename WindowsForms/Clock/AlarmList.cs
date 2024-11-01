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
    public partial class AlarmList : Form
    {
        public ListBox ListBoxAlarms 
        {
            get => listBoxAlarms;
            private set=>listBoxAlarms=value; 
        }
        public AlarmList()
        {
            InitializeComponent();
        }

        private void buttonAddAlarm_Click(object sender, EventArgs e)
        {
            AddAlarm addAlarm = new AddAlarm(); 
            if(addAlarm.ShowDialog(this)==DialogResult.OK)
            {
                listBoxAlarms.Items.Add(addAlarm.Alarm);
            }
        }

        private void listBoxAlarms_DoubleClick(object sender, EventArgs e)
        {
            AddAlarm addAlarm= new AddAlarm((sender as ListBox).SelectedItem as Alarm);
            if(addAlarm.ShowDialog(this)==DialogResult.OK)
            {
                listBoxAlarms.SelectedItem = addAlarm.Alarm;
                listBoxAlarms.Items[listBoxAlarms.SelectedIndex] = listBoxAlarms.Items[listBoxAlarms.SelectedIndex]; 
            }

            
        }
    }
}
