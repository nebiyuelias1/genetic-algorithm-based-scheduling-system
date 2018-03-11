namespace SchedulingSystem.UI
{
    partial class NewInstructor
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
            this.firstNameValue = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.fatherNameValue = new System.Windows.Forms.TextBox();
            this.grandFatherNameValue = new System.Windows.Forms.TextBox();
            this.departmentDropdown = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "First Name";
            // 
            // firstNameValue
            // 
            this.firstNameValue.Location = new System.Drawing.Point(118, 29);
            this.firstNameValue.Name = "firstNameValue";
            this.firstNameValue.Size = new System.Drawing.Size(281, 20);
            this.firstNameValue.TabIndex = 1;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(118, 214);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Father Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Grand Father Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Department";
            // 
            // fatherNameValue
            // 
            this.fatherNameValue.Location = new System.Drawing.Point(118, 70);
            this.fatherNameValue.Name = "fatherNameValue";
            this.fatherNameValue.Size = new System.Drawing.Size(281, 20);
            this.fatherNameValue.TabIndex = 6;
            // 
            // grandFatherNameValue
            // 
            this.grandFatherNameValue.Location = new System.Drawing.Point(118, 115);
            this.grandFatherNameValue.Name = "grandFatherNameValue";
            this.grandFatherNameValue.Size = new System.Drawing.Size(281, 20);
            this.grandFatherNameValue.TabIndex = 7;
            // 
            // departmentDropdown
            // 
            this.departmentDropdown.FormattingEnabled = true;
            this.departmentDropdown.Location = new System.Drawing.Point(118, 151);
            this.departmentDropdown.Name = "departmentDropdown";
            this.departmentDropdown.Size = new System.Drawing.Size(280, 21);
            this.departmentDropdown.TabIndex = 8;
            // 
            // NewInstructor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 285);
            this.Controls.Add(this.departmentDropdown);
            this.Controls.Add(this.grandFatherNameValue);
            this.Controls.Add(this.fatherNameValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.firstNameValue);
            this.Controls.Add(this.label1);
            this.Name = "NewInstructor";
            this.Text = "NewInstructor";
            this.Load += new System.EventHandler(this.NewInstructor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox firstNameValue;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox fatherNameValue;
        private System.Windows.Forms.TextBox grandFatherNameValue;
        private System.Windows.Forms.ComboBox departmentDropdown;
    }
}