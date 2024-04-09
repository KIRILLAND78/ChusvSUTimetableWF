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
            SuspendLayout();
            // 
            // loginButton
            // 
            loginButton.Location = new Point(12, 176);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(426, 29);
            loginButton.TabIndex = 0;
            loginButton.Text = "Войти";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += button1_Click;
            loginButton.MouseEnter += button1_MouseEnter;
            loginButton.MouseLeave += button1_MouseHover;
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
            loginTextBox.Location = new Point(237, 16);
            loginTextBox.Name = "loginTextBox";
            loginTextBox.Size = new Size(205, 27);
            loginTextBox.TabIndex = 5;
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(237, 49);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Size = new Size(205, 27);
            passwordTextBox.TabIndex = 6;
            // 
            // groupLabel
            // 
            groupLabel.AutoSize = true;
            groupLabel.Location = new Point(12, 85);
            groupLabel.Name = "groupLabel";
            groupLabel.Size = new Size(215, 20);
            groupLabel.TabIndex = 7;
            groupLabel.Text = "Группа (не вводите, если нет)";
            // 
            // groupTextBox
            // 
            groupTextBox.Location = new Point(237, 82);
            groupTextBox.Name = "groupTextBox";
            groupTextBox.Size = new Size(205, 27);
            groupTextBox.TabIndex = 8;
            // 
            // wrongLoginPasswordNotification
            // 
            wrongLoginPasswordNotification.AutoSize = true;
            wrongLoginPasswordNotification.ForeColor = Color.Red;
            wrongLoginPasswordNotification.Location = new Point(12, 123);
            wrongLoginPasswordNotification.Name = "wrongLoginPasswordNotification";
            wrongLoginPasswordNotification.Size = new Size(301, 20);
            wrongLoginPasswordNotification.TabIndex = 9;
            wrongLoginPasswordNotification.Text = "Был введен неправильный логин/пароль";
            wrongLoginPasswordNotification.Visible = false;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(454, 217);
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
    }
}