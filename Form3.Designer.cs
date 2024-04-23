namespace ChusvSUTimetableWF
{
    partial class Form3
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
            label1 = new Label();
            subgroupLabel = new Label();
            button1 = new Button();
            subgroupTextBox = new TextBox();
            button3 = new Button();
            transparencySlider = new TrackBar();
            transparencySliderLabel = new Label();
            label3 = new Label();
            checkBox1 = new CheckBox();
            Credits = new Button();
            ((System.ComponentModel.ISupportInitialize)transparencySlider).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(343, 20);
            label1.TabIndex = 0;
            label1.Text = "Прототип виджета расписания ЧГУ, Итерация 4";
            // 
            // subgroupLabel
            // 
            subgroupLabel.AutoSize = true;
            subgroupLabel.Location = new Point(12, 51);
            subgroupLabel.Name = "subgroupLabel";
            subgroupLabel.Size = new Size(85, 20);
            subgroupLabel.TabIndex = 2;
            subgroupLabel.Text = "Подгруппа";
            // 
            // button1
            // 
            button1.Location = new Point(472, 195);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 4;
            button1.Text = "Сохранить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // subgroupTextBox
            // 
            subgroupTextBox.Location = new Point(134, 48);
            subgroupTextBox.Name = "subgroupTextBox";
            subgroupTextBox.Size = new Size(209, 27);
            subgroupTextBox.TabIndex = 5;
            // 
            // button3
            // 
            button3.Location = new Point(208, 195);
            button3.Name = "button3";
            button3.Size = new Size(258, 29);
            button3.TabIndex = 8;
            button3.Text = "Сбросить положение окна";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // transparencySlider
            // 
            transparencySlider.Location = new Point(134, 93);
            transparencySlider.Minimum = 1;
            transparencySlider.Name = "transparencySlider";
            transparencySlider.Size = new Size(209, 56);
            transparencySlider.TabIndex = 9;
            transparencySlider.Value = 10;
            transparencySlider.Scroll += transparencySlider_Scroll;
            // 
            // transparencySliderLabel
            // 
            transparencySliderLabel.AutoSize = true;
            transparencySliderLabel.Location = new Point(12, 93);
            transparencySliderLabel.Name = "transparencySliderLabel";
            transparencySliderLabel.Size = new Size(109, 20);
            transparencySliderLabel.TabIndex = 10;
            transparencySliderLabel.Text = "Прозрачность";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 163);
            label3.Name = "label3";
            label3.Size = new Size(124, 20);
            label3.TabIndex = 11;
            label3.Text = "Перетаскивание";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(146, 164);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(18, 17);
            checkBox1.TabIndex = 12;
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // Credits
            // 
            Credits.Location = new Point(473, 16);
            Credits.Name = "Credits";
            Credits.Size = new Size(94, 29);
            Credits.TabIndex = 13;
            Credits.Text = "Создатели";
            Credits.UseVisualStyleBackColor = true;
            Credits.Click += Credits_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(579, 234);
            Controls.Add(Credits);
            Controls.Add(checkBox1);
            Controls.Add(label3);
            Controls.Add(transparencySliderLabel);
            Controls.Add(transparencySlider);
            Controls.Add(button3);
            Controls.Add(subgroupTextBox);
            Controls.Add(button1);
            Controls.Add(subgroupLabel);
            Controls.Add(label1);
            Name = "Form3";
            Text = "Настройки";
            Load += Form3_Load;
            ((System.ComponentModel.ISupportInitialize)transparencySlider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label subgroupLabel;
        private Button button1;
        private TextBox subgroupTextBox;
        private Button button3;
        private TrackBar transparencySlider;
        private Label transparencySliderLabel;
        private Label label3;
        private CheckBox checkBox1;
        private Button Credits;
    }
}