namespace SchedulingSystem.UI
{
    partial class AssignInstructorToCourseOffering
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
            this.courseOfferingsListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.instructorsDropdown = new System.Windows.Forms.ComboBox();
            this.assignButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // courseOfferingsListBox
            // 
            this.courseOfferingsListBox.FormattingEnabled = true;
            this.courseOfferingsListBox.Location = new System.Drawing.Point(12, 12);
            this.courseOfferingsListBox.Name = "courseOfferingsListBox";
            this.courseOfferingsListBox.Size = new System.Drawing.Size(438, 108);
            this.courseOfferingsListBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Instructor";
            // 
            // instructorsDropdown
            // 
            this.instructorsDropdown.FormattingEnabled = true;
            this.instructorsDropdown.Location = new System.Drawing.Point(69, 144);
            this.instructorsDropdown.Name = "instructorsDropdown";
            this.instructorsDropdown.Size = new System.Drawing.Size(380, 21);
            this.instructorsDropdown.TabIndex = 2;
            // 
            // assignButton
            // 
            this.assignButton.Location = new System.Drawing.Point(69, 216);
            this.assignButton.Name = "assignButton";
            this.assignButton.Size = new System.Drawing.Size(75, 23);
            this.assignButton.TabIndex = 3;
            this.assignButton.Text = "Assign";
            this.assignButton.UseVisualStyleBackColor = true;
            this.assignButton.Click += new System.EventHandler(this.assignButton_Click);
            // 
            // AssignInstructorToCourseOffering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 308);
            this.Controls.Add(this.assignButton);
            this.Controls.Add(this.instructorsDropdown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.courseOfferingsListBox);
            this.Name = "AssignInstructorToCourseOffering";
            this.Text = "AssignInstructorToCourseOffering";
            this.Load += new System.EventHandler(this.AssignInstructorToCourseOffering_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox courseOfferingsListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox instructorsDropdown;
        private System.Windows.Forms.Button assignButton;
    }
}