using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChusvSUTimetableWF
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Settings.Instance.Group = int.Parse(subgroupTextBox.Text);
            }
            catch
            {
                subgroupTextBox.Text = "Недопустимый формат";
            };
            Settings.Instance.Transparency = transparencySlider.Value * 10;
            Settings.Instance.Draggable = checkBox1.Checked;
            Settings.Instance.Save();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            transparencySlider.Value = Settings.Instance.Transparency / 10;
            subgroupTextBox.Text = $"{Settings.Instance.Group}";
            checkBox1.Checked = Settings.Instance.Draggable;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Settings.Instance.X = 5;
            Settings.Instance.Y = 5;
            Program.main.Location = new Point(5, 5);
            Settings.Instance.Save();
        }

        private void transparencySlider_Scroll(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Credits_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Код виджета: Казаков Кирилл Валерьевич, КТ-31-21\r\nAPI расписания (Мой ЧувГУ): Петрянкин Даниил Евгеньевич, КТ-31-21\r\nДизайн:Гаврилов Александр Сергеевич, КТ-42-20");
        }
    }
}
