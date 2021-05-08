
namespace ImageConverter
{
    partial class About
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
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(1, 46);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(396, 16);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "1- Add Images by Drag Images or using the add button";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(1, 75);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(396, 16);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "2- Select an output folder";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(1, 104);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(396, 16);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "3- Select a format";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label4
            // 
            this.Label4.Location = new System.Drawing.Point(1, 133);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(396, 16);
            this.Label4.TabIndex = 3;
            this.Label4.Text = "4- Click Convert";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(1, 12);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(396, 16);
            this.Label5.TabIndex = 4;
            this.Label5.Text = "How to use:";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label6
            // 
            this.Label6.Location = new System.Drawing.Point(1, 190);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(396, 16);
            this.Label6.TabIndex = 5;
            this.Label6.Text = "2021 - Ala Chebbi";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 222);
            this.ControlBox = false;
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Deactivate += new System.EventHandler(this.About_Click);
            this.Click += new System.EventHandler(this.About_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label6;
    }
}