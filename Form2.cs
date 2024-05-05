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
    public partial class Form2 : Form
    {
        private System.Windows.Forms.Timer hideWrongPasswordLabelTimer;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TTApiManager.Instance.Login(loginTextBox.Text, passwordTextBox.Text);
            if (TTApiManager.Instance.State == "Nominal")
            {
                this.Close();
                return;
            }
            if (hideWrongPasswordLabelTimer != null)
            {
                hideWrongPasswordLabelTimer.Stop();
                hideWrongPasswordLabelTimer.Dispose();
            }
            hideWrongPasswordLabelTimer = new System.Windows.Forms.Timer();
            hideWrongPasswordLabelTimer.Interval = 4000;
            hideWrongPasswordLabelTimer.Tick += HideWrongPasswordLabelTimer_Tick;
            hideWrongPasswordLabelTimer.Start();
            if (TTApiManager.Instance.State == "Wrong credentials")
            {
                wrongLoginPasswordNotification.Text = "Неверный логин/пароль";
                wrongLoginPasswordNotification.Visible = true;
                return;
            }
            wrongLoginPasswordNotification.Text = "Неизвестная ошибка.\r\nПожалуйста, проверьте соединение с интернетом.";
            wrongLoginPasswordNotification.Visible = true;
            //show неизвестная ошибка
        }

        private void HideWrongPasswordLabelTimer_Tick(object? sender, EventArgs e)
        {
            wrongLoginPasswordNotification.Visible = false;
            hideWrongPasswordLabelTimer.Stop();
            hideWrongPasswordLabelTimer.Dispose();
        }

        private void AgreementLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {UseShellExecute = true , FileName = "https://online.chuvsu.ru/doc/user_agreement" }
            );
        }

        private void AgreementLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            { UseShellExecute = true, FileName = "https://online.chuvsu.ru/doc/privacy_policy" }
            );
        }
    }
}
