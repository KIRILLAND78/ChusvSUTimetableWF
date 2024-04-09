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
                Settings.Instance.Group = int.Parse(textBox1.Text);
            }
            catch
            {
                textBox1.Text = "недопустимый формат";
            };
            Program.Quack();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            textBox1.Text = $"{Settings.Instance.Group}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Size = new Size(this.Size.Width, 843);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Settings.Instance.X = 5;
            Settings.Instance.Y = 5;
            Program.main.Location = new Point(5, 5);
            Settings.Instance.Save();
        }
    }
}
