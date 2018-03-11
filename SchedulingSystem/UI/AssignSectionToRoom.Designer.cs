namespace SchedulingSystem.UI
{
    partial class AssignSectionToRoom
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
            this.sectionDropdown = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.roomDropdown = new System.Windows.Forms.ComboBox();
            this.assignSectionToRoomButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Section";
            // 
            // sectionDropdown
            // 
            this.sectionDropdown.FormattingEnabled = true;
            this.sectionDropdown.Location = new System.Drawing.Point(62, 36);
            this.sectionDropdown.Name = "sectionDropdown";
            this.sectionDropdown.Size = new System.Drawing.Size(242, 21);
            this.sectionDropdown.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Room";
            // 
            // roomDropdown
            // 
            this.roomDropdown.FormattingEnabled = true;
            this.roomDropdown.Location = new System.Drawing.Point(62, 94);
            this.roomDropdown.Name = "roomDropdown";
            this.roomDropdown.Size = new System.Drawing.Size(242, 21);
            this.roomDropdown.TabIndex = 3;
            // 
            // assignSectionToRoomButton
            // 
            this.assignSectionToRoomButton.Location = new System.Drawing.Point(62, 172);
            this.assignSectionToRoomButton.Name = "assignSectionToRoomButton";
            this.assignSectionToRoomButton.Size = new System.Drawing.Size(75, 23);
            this.assignSectionToRoomButton.TabIndex = 4;
            this.assignSectionToRoomButton.Text = "Assign";
            this.assignSectionToRoomButton.UseVisualStyleBackColor = true;
            this.assignSectionToRoomButton.Click += new System.EventHandler(this.assignSectionToRoomButton_Click);
            // 
            // AssignSectionToRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 371);
            this.Controls.Add(this.assignSectionToRoomButton);
            this.Controls.Add(this.roomDropdown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sectionDropdown);
            this.Controls.Add(this.label1);
            this.Name = "AssignSectionToRoom";
            this.Text = "AssignSectionToRoom";
            this.Load += new System.EventHandler(this.AssignSectionToRoom_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox sectionDropdown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox roomDropdown;
        private System.Windows.Forms.Button assignSectionToRoomButton;
    }
}