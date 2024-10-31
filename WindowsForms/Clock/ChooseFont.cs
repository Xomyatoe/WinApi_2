using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Text;

namespace Clock
{
    public partial class ChooseFont : Form
    {
        public Font ChosenFont { get; private set; }
        public string FontFile {  get; private set; }  
        public ChooseFont()
        {
            InitializeComponent();
            LoadFonts();
        }
        public ChooseFont(string fontFile):this()
        {
            SetFontFile(fontFile);
        }
        public Font SetFontFile(string fontFile)
        {
            FontFile = fontFile;
            comboBoxFont.SelectedIndex = comboBoxFont.Items.IndexOf(FontFile);
            PrivateFontCollection pfc=new PrivateFontCollection();
            pfc.AddFontFile(FontFile);
            return new Font(pfc.Families[0],36);
        }
        void LoadFonts()
        { 
            //Получаем список Всех файло в текущем каталоге и сохраняем в массив fonts
            string[] fonts= Directory.EnumerateFiles(Directory.GetCurrentDirectory(),"*.ttf").ToArray();
            //2) поскольку в массиве хранятся полные пути к файлам, убираем пути и оставляем только имена файлов
           for(int i=0;i<fonts.Length; i++)
            {
                fonts[i] = fonts[i].Split('\\').Last();
            }
          // загружаем весь массив файлов в combobox
            comboBoxFont.Items.AddRange(fonts);
            comboBoxFont.SelectedIndex = 0;
        }

        private void comboBoxFont_SelectedValueChanged(object sender, EventArgs e)
        {
            FontFile= $"{Directory.GetCurrentDirectory()}\\{comboBoxFont.SelectedItem.ToString()}";
            //MessageBox.Show(fontFile);
            PrivateFontCollection pfc=new PrivateFontCollection();
            pfc.AddFontFile(FontFile);  
            Font font = new Font(pfc.Families[0], 36);
            labelExample.Font = font;
            
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ChosenFont = new Font(labelExample.Font.FontFamily, labelExample.Font.Size); 
        }
    }
}
