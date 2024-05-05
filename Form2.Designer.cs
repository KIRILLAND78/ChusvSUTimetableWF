namespace ChusvSUTimetableWF
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            loginButton = new Button();
            loginLabel = new Label();
            passwordLabel = new Label();
            loginTextBox = new TextBox();
            passwordTextBox = new TextBox();
            groupLabel = new Label();
            groupTextBox = new TextBox();
            wrongLoginPasswordNotification = new Label();
            AgreementLabel1 = new Label();
            AgreementLabel2 = new LinkLabel();
            AgreementLabel3 = new Label();
            AgreementLabel4 = new LinkLabel();
            AgreementLabel5 = new Label();
            AgreementLabel6 = new Label();
            SuspendLayout();
            // 
            // loginButton
            // 
            loginButton.Location = new Point(12, 229);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(457, 29);
            loginButton.TabIndex = 0;
            loginButton.Text = "Войти";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += button1_Click;
            // 
            // loginLabel
            // 
            loginLabel.AutoSize = true;
            loginLabel.Location = new Point(12, 19);
            loginLabel.Name = "loginLabel";
            loginLabel.Size = new Size(106, 20);
            loginLabel.TabIndex = 3;
            loginLabel.Text = "Логин (почта)";
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(12, 52);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(62, 20);
            passwordLabel.TabIndex = 4;
            passwordLabel.Text = "Пароль";
            // 
            // loginTextBox
            // 
            loginTextBox.Location = new Point(260, 16);
            loginTextBox.Name = "loginTextBox";
            loginTextBox.Size = new Size(209, 27);
            loginTextBox.TabIndex = 5;
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(260, 49);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Size = new Size(209, 27);
            passwordTextBox.TabIndex = 6;
            // 
            // groupLabel
            // 
            groupLabel.AutoSize = true;
            groupLabel.Location = new Point(12, 85);
            groupLabel.Name = "groupLabel";
            groupLabel.Size = new Size(242, 20);
            groupLabel.TabIndex = 7;
            groupLabel.Text = "Подгруппа (не вводите, если нет)";
            // 
            // groupTextBox
            // 
            groupTextBox.Location = new Point(260, 82);
            groupTextBox.Name = "groupTextBox";
            groupTextBox.Size = new Size(209, 27);
            groupTextBox.TabIndex = 8;
            // 
            // wrongLoginPasswordNotification
            // 
            wrongLoginPasswordNotification.AutoSize = true;
            wrongLoginPasswordNotification.ForeColor = Color.Red;
            wrongLoginPasswordNotification.Location = new Point(12, 192);
            wrongLoginPasswordNotification.Name = "wrongLoginPasswordNotification";
            wrongLoginPasswordNotification.Size = new Size(301, 20);
            wrongLoginPasswordNotification.TabIndex = 9;
            wrongLoginPasswordNotification.Text = "Был введен неправильный логин/пароль";
            wrongLoginPasswordNotification.Visible = false;
            // 
            // AgreementLabel1
            // 
            AgreementLabel1.AutoSize = true;
            AgreementLabel1.Location = new Point(12, 112);
            AgreementLabel1.Name = "AgreementLabel1";
            AgreementLabel1.Size = new Size(266, 20);
            AgreementLabel1.TabIndex = 10;
            AgreementLabel1.Text = "Нажимая \"Войти\" вы соглашаетесь с";
            // 
            // AgreementLabel2
            // 
            AgreementLabel2.AutoSize = true;
            AgreementLabel2.Location = new Point(12, 132);
            AgreementLabel2.Name = "AgreementLabel2";
            AgreementLabel2.Size = new Size(238, 20);
            AgreementLabel2.TabIndex = 11;
            AgreementLabel2.TabStop = true;
            AgreementLabel2.Text = "пользовательским соглашением";
            AgreementLabel2.LinkClicked += AgreementLabel2_LinkClicked;
            // 
            // AgreementLabel3
            // 
            AgreementLabel3.AutoSize = true;
            AgreementLabel3.Location = new Point(256, 132);
            AgreementLabel3.Name = "AgreementLabel3";
            AgreementLabel3.Size = new Size(18, 20);
            AgreementLabel3.TabIndex = 12;
            AgreementLabel3.Text = "и";
            // 
            // AgreementLabel4
            // 
            AgreementLabel4.AutoSize = true;
            AgreementLabel4.Location = new Point(12, 152);
            AgreementLabel4.Name = "AgreementLabel4";
            AgreementLabel4.Size = new Size(325, 20);
            AgreementLabel4.TabIndex = 13;
            AgreementLabel4.TabStop = true;
            AgreementLabel4.Text = "политикой обработки персональных данных";
            AgreementLabel4.LinkClicked += AgreementLabel4_LinkClicked;
            // 
            // AgreementLabel5
            // 
            AgreementLabel5.AutoSize = true;
            AgreementLabel5.Location = new Point(343, 152);
            AgreementLabel5.Name = "AgreementLabel5";
            AgreementLabel5.Size = new Size(98, 20);
            AgreementLabel5.TabIndex = 14;
            AgreementLabel5.Text = "приложения";
            // 
            // AgreementLabel6
            // 
            AgreementLabel6.AutoSize = true;
            AgreementLabel6.Location = new Point(12, 172);
            AgreementLabel6.Name = "AgreementLabel6";
            AgreementLabel6.Size = new Size(97, 20);
            AgreementLabel6.TabIndex = 15;
            AgreementLabel6.Text = "\"Мой ЧувГУ\"";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(481, 270);
            Controls.Add(AgreementLabel6);
            Controls.Add(AgreementLabel5);
            Controls.Add(AgreementLabel4);
            Controls.Add(AgreementLabel3);
            Controls.Add(AgreementLabel2);
            Controls.Add(AgreementLabel1);
            Controls.Add(wrongLoginPasswordNotification);
            Controls.Add(groupTextBox);
            Controls.Add(groupLabel);
            Controls.Add(passwordTextBox);
            Controls.Add(loginTextBox);
            Controls.Add(passwordLabel);
            Controls.Add(loginLabel);
            Controls.Add(loginButton);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form2";
            Text = "Вход";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button loginButton;
        private Label loginLabel;
        private Label passwordLabel;
        private TextBox loginTextBox;
        private TextBox passwordTextBox;
        private Label groupLabel;
        private TextBox groupTextBox;
        private Label wrongLoginPasswordNotification;
        private Label AgreementLabel1;
        private LinkLabel AgreementLabel2;
        private Label AgreementLabel3;
        private LinkLabel AgreementLabel4;
        private Label AgreementLabel5;
        private Label AgreementLabel6;
    }
}