namespace SchedulingSystem.UI
{
    partial class NewSection
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nameValue = new System.Windows.Forms.TextBox();
            this.entranceYearValue = new System.Windows.Forms.TextBox();
            this.studentCountValue = new System.Windows.Forms.TextBox();
            this.departmentDropdown = new System.Windows.Forms.ComboBox();
            this.registerSectionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Entrace Year";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Student Count";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Department";
            // 
            // nameValue
            // 
            this.nameValue.Location = new System.Drawing.Point(94, 21);
            this.nameValue.Name = "nameValue";
            this.nameValue.Size = new System.Drawing.Size(292, 20);
            this.nameValue.TabIndex = 4;
            // 
            // entranceYearValue
            // 
            this.entranceYearValue.Location = new System.Drawing.Point(93, 57);
            this.entranceYearValue.Name = "entranceYearValue";
            this.entranceYearValue.Size = new System.Drawing.Size(292, 20);
            this.entranceYearValue.TabIndex = 5;
            // 
            // studentCountValue
            // 
            this.studentCountValue.Location = new System.Drawing.Point(94, 98);
            this.studentCountValue.Name = "studentCountValue";
            this.studentCountValue.Size = new System.Drawing.Size(292, 20);
            this.studentCountValue.TabIndex = 6;
            // 
            // departmentDropdown
            // 
            this.departmentDropdown.FormattingEnabled = true;
            this.departmentDropdown.Location = new System.Drawing.Point(93, 140);
            this.departmentDropdown.Name = "departmentDropdown";
            this.departmentDropdown.Size = new System.Drawing.Size(293, 21);
            this.departmentDropdown.TabIndex = 7;
            // 
            // registerSectionButton
            // 
            this.registerSectionButton.Location = new System.Drawing.Point(93, 212);
            this.registerSectionButton.Name = "registerSectionButton";
            this.registerSectionButton.Size = new System.Drawing.Size(124, 23);
            this.registerSectionButton.TabIndex = 8;
            this.registerSectionButton.Text = "Register Section";
            this.registerSectionButton.UseVisualStyleBackColor = true;
            this.registerSectionButton.Click += new System.EventHandler(this.registerSectionButton_Click);
            // 
            // NewSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 281);
            this.Controls.Add(this.registerSectionButton);
            this.Controls.Add(this.departmentDropdown);
            this.Controls.Add(this.studentCountValue);
            this.Controls.Add(this.entranceYearValue);
            this.Controls.Add(this.nameValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "NewSection";
            this.Text = "NewSection";
            this.Load += new System.EventHandler(this.NewSection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox nameValue;
        private System.Windows.Forms.TextBox entranceYearValue;
        private System.Windows.Forms.TextBox studentCountValue;
        private System.Windows.Forms.ComboBox departmentDropdown;
        private System.Windows.Forms.Button registerSectionButton;
    }
}