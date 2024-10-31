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

namespace Clock
{
    public partial class ChooseFont : Form
    {
        public ChooseFont()
        {
            InitializeComponent();
            LoadFonts();
        }
        void LoadFonts()
        { 
            //Получаем список Всех файло в текущем каталоге и сохраняем в массив fonts
            string[] fonts= Directory.EnumerateFiles(Directory.GetCurrentDirectory()).ToArray();
            //2) поскольку в массиве хранятся полные пути к файлам, убираем пути и оставляем только имена файлов
           for(int i=0;i<fonts.Length; i++)
            {
                fonts[i] = fonts[i].Split('\\').Last();
            }
          // загружаем весь массив файлов в combobox
            comboBoxFont.Items.AddRange(fonts);
        }
    }
}
