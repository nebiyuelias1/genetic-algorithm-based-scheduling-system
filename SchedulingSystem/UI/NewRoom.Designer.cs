namespace SchedulingSystem.UI
{
    partial class NewRoom
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
            this.nameValue = new System.Windows.Forms.TextBox();
            this.buildingValue = new System.Windows.Forms.TextBox();
            this.sizeValue = new System.Windows.Forms.TextBox();
            this.registerRoomButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Building";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Size";
            // 
            // nameValue
            // 
            this.nameValue.Location = new System.Drawing.Point(73, 17);
            this.nameValue.Name = "nameValue";
            this.nameValue.Size = new System.Drawing.Size(268, 20);
            this.nameValue.TabIndex = 3;
            // 
            // buildingValue
            // 
            this.buildingValue.Location = new System.Drawing.Point(73, 61);
            this.buildingValue.Name = "buildingValue";
            this.buildingValue.Size = new System.Drawing.Size(268, 20);
            this.buildingValue.TabIndex = 4;
            // 
            // sizeValue
            // 
            this.sizeValue.Location = new System.Drawing.Point(73, 108);
            this.sizeValue.Name = "sizeValue";
            this.sizeValue.Size = new System.Drawing.Size(268, 20);
            this.sizeValue.TabIndex = 5;
            // 
            // registerRoomButton
            // 
            this.registerRoomButton.Location = new System.Drawing.Point(73, 167);
            this.registerRoomButton.Name = "registerRoomButton";
            this.registerRoomButton.Size = new System.Drawing.Size(103, 23);
            this.registerRoomButton.TabIndex = 6;
            this.registerRoomButton.Text = "Register Room";
            this.registerRoomButton.UseVisualStyleBackColor = true;
            this.registerRoomButton.Click += new System.EventHandler(this.registerSectionButton_Click);
            // 
            // NewRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 220);
            this.Controls.Add(this.registerRoomButton);
            this.Controls.Add(this.sizeValue);
            this.Controls.Add(this.buildingValue);
            this.Controls.Add(this.nameValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "NewRoom";
            this.Text = "NewRoom";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nameValue;
        private System.Windows.Forms.TextBox buildingValue;
        private System.Windows.Forms.TextBox sizeValue;
        private System.Windows.Forms.Button registerRoomButton;
    }
}