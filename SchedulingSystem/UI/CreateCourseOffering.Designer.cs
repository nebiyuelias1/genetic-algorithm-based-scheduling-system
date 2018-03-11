namespace SchedulingSystem.UI
{
    partial class CreateCourseOffering
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
            this.courseDropdown = new System.Windows.Forms.ComboBox();
            this.sectionDropdown = new System.Windows.Forms.ComboBox();
            this.yearValue = new System.Windows.Forms.TextBox();
            this.semesterValue = new System.Windows.Forms.TextBox();
            this.createButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Course";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Section";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Year";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(206, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Semester";
            // 
            // courseDropdown
            // 
            this.courseDropdown.FormattingEnabled = true;
            this.courseDropdown.Location = new System.Drawing.Point(71, 43);
            this.courseDropdown.Name = "courseDropdown";
            this.courseDropdown.Size = new System.Drawing.Size(314, 21);
            this.courseDropdown.TabIndex = 4;
            // 
            // sectionDropdown
            // 
            this.sectionDropdown.FormattingEnabled = true;
            this.sectionDropdown.Location = new System.Drawing.Point(71, 91);
            this.sectionDropdown.Name = "sectionDropdown";
            this.sectionDropdown.Size = new System.Drawing.Size(314, 21);
            this.sectionDropdown.TabIndex = 5;
            // 
            // yearValue
            // 
            this.yearValue.Location = new System.Drawing.Point(71, 138);
            this.yearValue.Name = "yearValue";
            this.yearValue.Size = new System.Drawing.Size(122, 20);
            this.yearValue.TabIndex = 6;
            // 
            // semesterValue
            // 
            this.semesterValue.Location = new System.Drawing.Point(263, 139);
            this.semesterValue.Name = "semesterValue";
            this.semesterValue.Size = new System.Drawing.Size(122, 20);
            this.semesterValue.TabIndex = 7;
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(71, 185);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(75, 23);
            this.createButton.TabIndex = 8;
            this.createButton.Text = "Create";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // CreateCourseOffering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 221);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.semesterValue);
            this.Controls.Add(this.yearValue);
            this.Controls.Add(this.sectionDropdown);
            this.Controls.Add(this.courseDropdown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CreateCourseOffering";
            this.Text = "CreateCourseOffering";
            this.Load += new System.EventHandler(this.CreateCourseOffering_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox courseDropdown;
        private System.Windows.Forms.ComboBox sectionDropdown;
        private System.Windows.Forms.TextBox yearValue;
        private System.Windows.Forms.TextBox semesterValue;
        private System.Windows.Forms.Button createButton;
    }
}