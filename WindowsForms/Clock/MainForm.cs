﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using Microsoft.Win32;
using System.Runtime.InteropServices;


namespace Clock
{
    public partial class MainForm : Form
    {
        ColorDialog backgroundColorDialog;
        ColorDialog foregroundColorDialog;
        ChooseFont chooseFontDialog;
        AlarmList alarmList;
        Alarm alarm;
        string FontFile { get; set; }  
        //static readonly string DEFAULT_ALARM_SOUND= "..\\Sound\\gepard-murlyikaet-31139.mp3";

        public MainForm()
        {
            InitializeComponent();
            AllocConsole();
            SetFontDirectory();

            this.TransparencyKey = Color.Empty;
            backgroundColorDialog = new ColorDialog();
            foregroundColorDialog = new ColorDialog();

            chooseFontDialog = new ChooseFont();
            LoadSettings();
            alarmList = new AlarmList();
           // backgroundColorDialog.Color = Color.Black;
            //foregroundColorDialog.Color=Color.Yellow;
           
            SetVisibility(false);
            this.Location = new Point
            (
                System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - this.Width,
            50
            );
            this.Text += $" Location: {this.Location.X} x{this.Location.Y}";
            alarm = new Alarm();
            GetNextAlarm();
            

        }
        void SetFontDirectory()
        {
            string location = Assembly.GetEntryAssembly().Location; //Получаем полный адрес исполняемого файла
            string path =Path.GetDirectoryName(location); //из адреса извлекаем путь к файлу
           // MessageBox.Show(path);
            Directory.SetCurrentDirectory($"{path}\\..\\..\\Fonts");//переходим в каталог со шрифтами
            //MessageBox.Show(Directory.GetCurrentDirectory());
        }
        void LoadSettings()
        {
            StreamReader sr = new StreamReader("settings.txt");
            List<string> settings = new List<string>();
            while(!sr.EndOfStream)
            {
                settings.Add(sr.ReadLine());
            }
            sr.Close();
            backgroundColorDialog.Color = Color.FromArgb(Convert.ToInt32(settings.ToArray()[0]));
            foregroundColorDialog.Color = Color.FromArgb(Convert.ToInt32(settings.ToArray()[1]));
            FontFile = settings.ToArray()[2];
            topmostToolStripMenuItem.Checked = bool.Parse(settings.ToArray()[3]);
            showDateToolStripMenuItem.Checked = bool.Parse(settings.ToArray()[4]);
            labelTime.Font= chooseFontDialog.SetFontFile(FontFile);
            labelTime.ForeColor = foregroundColorDialog.Color;
            labelTime.BackColor = backgroundColorDialog.Color;
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            object run=rk.GetValue("Clock318");
            if (run != null) loadOnWindowsStartupToolStripMenuItem.Checked = true;
            rk.Dispose();
        }
        void SaveSettings()
        {
            StreamWriter sw = new StreamWriter("settings.txt");
            sw.WriteLine(backgroundColorDialog.Color.ToArgb()); //ToArgb возвращает числовой код цвета
            sw.WriteLine(foregroundColorDialog.Color.ToArgb() );
            sw.WriteLine(chooseFontDialog.FontFile.Split('\\').Last());
            sw.WriteLine(topmostToolStripMenuItem.Checked);
            sw.WriteLine(showDateToolStripMenuItem.Checked);
            sw.Close();
            Process.Start("notepad", "settings.txt");
        }
        void GetNextAlarm()
        {
                
                List<Alarm> alarms = new List<Alarm>();
                foreach (Alarm item in alarmList.ListBoxAlarms.Items)
                {
                   if(item.Time.TimeOfDay>DateTime.Now.TimeOfDay) 
                    alarms.Add(item);
                }
            if(alarms.Min()!=null)alarm = alarms.Min();
            //List<TimeSpan> intervals=new List<TimeSpan>();
            //foreach (Alarm item in alarmList.ListBoxAlarms.Items)
            //{
            //    TimeSpan min = new TimeSpan(24, 0, 0);
            //    if (DateTime.Now - item.Time < min) alarm = item; 
            //}            
           Console.WriteLine(alarm);
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            labelTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            if(cbShowDate.Checked ) 
            {
                labelTime.Text += $"\n{DateTime.Today.ToString("yyyy.MM.dd")}";
            }
            if(showWeekdayToolStripMenuItem.Checked)
            {
                labelTime.Text += $"\n{DateTime.Now.DayOfWeek}";
            }
            //notifyIconSystemTray.Text = "Current time " + labelTime.Text;
            //Console.WriteLine(DateTime.Now.DayOfWeek - 1 < 0 & 6:DateTime.Now.Day - 1);
           // int weekday = (int)DateTime.Now.DayOfWeek;
           // weekday = weekday == 0 ? 7 : weekday - 1;

            if (
            alarm!=null&&
                alarm.Weekdays[((int)DateTime.Now.DayOfWeek==0?6:(int)DateTime.Now.DayOfWeek-1)] == true &&
                DateTime.Now.Hour==alarm.Time.Hour&&
                DateTime.Now.Minute==alarm.Time.Minute&&
                DateTime.Now.Second==alarm.Time.Second)
            {
               // MessageBox.Show(alarm.Filename,"Alarm",MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("ALARM:---" + alarm.ToString());
                PlayAlarm();
                GetNextAlarm();
            }
            if(DateTime.Now.Second==0)
            {
                GetNextAlarm();
                Console.WriteLine("Minute");
            }
        }
        void PlayAlarm()
        {
            axWindowsMediaPlayer.URL = 
                File.Exists(alarm.Filename) ?
                alarm.Filename: 
                Path.GetFullPath("..\\Sound\\gepard-murlyikaet-31139.mp3");
            axWindowsMediaPlayer.settings.volume = 100;
            axWindowsMediaPlayer.Visible = true;
            axWindowsMediaPlayer.Ctlcontrols.play();
            Console.WriteLine($"Play ALARM: {alarm}");
        }
        private void SetVisibility(bool visible) 
        {
            this.TransparencyKey = visible? Color.Empty: this.BackColor;
            this.FormBorderStyle = visible? FormBorderStyle.Sizable:FormBorderStyle.None;
            this.ShowInTaskbar = visible;
            cbShowDate.Visible = visible;
            btnHideControls.Visible = visible;
            labelTime.BackColor = visible?Color.Empty:backgroundColorDialog.Color;
            axWindowsMediaPlayer.Visible = false;

        }
        private void btnHideControls_Click(object sender, EventArgs e)
        {
            //SetVisibility(false);
            showControlsToolStripMenuItem.Checked = false; 
        }

        private void labelTime_DoubleClick(object sender, EventArgs e)
        {
            //SetVisibility(true);
            showControlsToolStripMenuItem.Checked = true;
        }

        private void notifyIconSystemTray_MouseMove(object sender, MouseEventArgs e)
        {
            notifyIconSystemTray.Text = "Current time\n" + labelTime.Text;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void topmostToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = topmostToolStripMenuItem.Checked;
        }

        private void showControlsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            SetVisibility(((ToolStripMenuItem)sender).Checked);
        }

        private void showDateToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            cbShowDate.Checked = ((ToolStripMenuItem)sender).Checked;
        }

        private void cbShowDate_CheckedChanged(object sender, EventArgs e)
        {
            showDateToolStripMenuItem.Checked = ((CheckBox)sender).Checked;
        }

        private void notifyIconSystemTray_DoubleClick(object sender, EventArgs e)
        {
            if(!this.TopMost) 
            {
                this.TopMost = true;
                this.TopMost= false;
            }
        }

        private void foregroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(foregroundColorDialog.ShowDialog(this) == DialogResult.OK) 
            {
                labelTime.ForeColor = foregroundColorDialog.Color;

            }
        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (backgroundColorDialog.ShowDialog(this) == DialogResult.OK)
            {
                labelTime.BackColor = backgroundColorDialog.Color;

            }
        }

        private void fontsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(chooseFontDialog.ShowDialog(this)==DialogResult.OK)  
            {
                labelTime.Font = chooseFontDialog.ChosenFont; 
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
            alarmList.SaveAlarmsToFile("alarms.csv");
        }

        private void loadOnWindowsStartupToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey rk =
                Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run",true);
            if (loadOnWindowsStartupToolStripMenuItem.Checked)
                rk.SetValue("clock318", Application.ExecutablePath);
            else rk.DeleteValue("Clock318", false);//false- не бросать исключения если указанная запись отсутствует
            rk.Dispose();
        }

        private void alarmsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alarmList.ShowDialog(this);
            GetNextAlarm();
        }
        void SetPlayerInvisible(object sender, AxWMPLib._WMPOCXEvents_EndOfStreamEvent e)
        {
            axWindowsMediaPlayer.Visible = false;
        }
        void SetPlayerInvisible(object sender,AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e )
        {
            if(axWindowsMediaPlayer.playState==WMPLib.WMPPlayState.wmppsMediaEnded)
            axWindowsMediaPlayer.Visible = false;
        }
        [DllImport("kernel32.dll")]
        static extern bool AllocConsole();
    }
}
