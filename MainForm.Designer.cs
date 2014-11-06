﻿namespace NoHassleShortcuts
{
    partial class MainForm
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
			this.lblStep1 = new System.Windows.Forms.Label();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.txtBatName = new System.Windows.Forms.TextBox();
			this.lblStep2 = new System.Windows.Forms.Label();
			this.lblBatExtension = new System.Windows.Forms.Label();
			this.btnCreateShortcut = new System.Windows.Forms.Button();
			this.lblStep3 = new System.Windows.Forms.Label();
			this.lblStep4 = new System.Windows.Forms.Label();
			this.lblStep5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblStep1
			// 
			this.lblStep1.AutoSize = true;
			this.lblStep1.Location = new System.Drawing.Point(28, 21);
			this.lblStep1.Name = "lblStep1";
			this.lblStep1.Size = new System.Drawing.Size(260, 20);
			this.lblStep1.TabIndex = 0;
			this.lblStep1.Text = "1. Drag in a file or folder to shortcut.";
			this.lblStep1.DragDrop += new System.Windows.Forms.DragEventHandler(this.lblStep1_DragDrop);
			this.lblStep1.DragEnter += new System.Windows.Forms.DragEventHandler(this.lblStep1_DragEnter);
			// 
			// txtPath
			// 
			this.txtPath.Location = new System.Drawing.Point(32, 63);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(329, 26);
			this.txtPath.TabIndex = 5;
			this.txtPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtPath_DragDrop);
			this.txtPath.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtPath_DragEnter);
			// 
			// btnBrowse
			// 
			this.btnBrowse.AutoSize = true;
			this.btnBrowse.Location = new System.Drawing.Point(378, 61);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(90, 30);
			this.btnBrowse.TabIndex = 10;
			this.btnBrowse.Text = "Browse";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			this.btnBrowse.DragDrop += new System.Windows.Forms.DragEventHandler(this.btnBrowse_DragDrop);
			this.btnBrowse.DragEnter += new System.Windows.Forms.DragEventHandler(this.btnBrowse_DragEnter);
			// 
			// txtBatName
			// 
			this.txtBatName.Location = new System.Drawing.Point(171, 165);
			this.txtBatName.Name = "txtBatName";
			this.txtBatName.Size = new System.Drawing.Size(127, 26);
			this.txtBatName.TabIndex = 15;
			this.txtBatName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtBatName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatName_KeyDown);
			// 
			// lblStep2
			// 
			this.lblStep2.AutoSize = true;
			this.lblStep2.Location = new System.Drawing.Point(28, 123);
			this.lblStep2.Name = "lblStep2";
			this.lblStep2.Size = new System.Drawing.Size(360, 20);
			this.lblStep2.TabIndex = 3;
			this.lblStep2.Text = "2. Type the name you want to use as the shortcut.";
			this.lblStep2.DragDrop += new System.Windows.Forms.DragEventHandler(this.lblStep2_DragDrop);
			this.lblStep2.DragEnter += new System.Windows.Forms.DragEventHandler(this.lblStep2_DragEnter);
			// 
			// lblBatExtension
			// 
			this.lblBatExtension.AutoSize = true;
			this.lblBatExtension.Location = new System.Drawing.Point(304, 168);
			this.lblBatExtension.Name = "lblBatExtension";
			this.lblBatExtension.Size = new System.Drawing.Size(36, 20);
			this.lblBatExtension.TabIndex = 6;
			this.lblBatExtension.Text = ".bat";
			// 
			// btnCreateShortcut
			// 
			this.btnCreateShortcut.AutoSize = true;
			this.btnCreateShortcut.Location = new System.Drawing.Point(175, 267);
			this.btnCreateShortcut.Name = "btnCreateShortcut";
			this.btnCreateShortcut.Size = new System.Drawing.Size(151, 30);
			this.btnCreateShortcut.TabIndex = 25;
			this.btnCreateShortcut.Text = "Create Shortcut";
			this.btnCreateShortcut.UseVisualStyleBackColor = true;
			this.btnCreateShortcut.Click += new System.EventHandler(this.btnCreateShortcut_Click);
			// 
			// lblStep3
			// 
			this.lblStep3.AutoSize = true;
			this.lblStep3.Location = new System.Drawing.Point(28, 227);
			this.lblStep3.Name = "lblStep3";
			this.lblStep3.Size = new System.Drawing.Size(183, 20);
			this.lblStep3.TabIndex = 8;
			this.lblStep3.Text = "3. Create the shortcut in ";
			// 
			// lblStep4
			// 
			this.lblStep4.AutoSize = true;
			this.lblStep4.Location = new System.Drawing.Point(28, 329);
			this.lblStep4.Name = "lblStep4";
			this.lblStep4.Size = new System.Drawing.Size(459, 20);
			this.lblStep4.TabIndex = 12;
			this.lblStep4.Text = "4. Press Windows + R, type the shortcut name, and press Enter.";
			// 
			// lblStep5
			// 
			this.lblStep5.AutoSize = true;
			this.lblStep5.Location = new System.Drawing.Point(26, 386);
			this.lblStep5.Name = "lblStep5";
			this.lblStep5.Size = new System.Drawing.Size(69, 20);
			this.lblStep5.TabIndex = 13;
			this.lblStep5.Text = "5. Enjoy!";
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(510, 438);
			this.Controls.Add(this.lblStep5);
			this.Controls.Add(this.lblStep4);
			this.Controls.Add(this.btnCreateShortcut);
			this.Controls.Add(this.lblStep3);
			this.Controls.Add(this.lblBatExtension);
			this.Controls.Add(this.txtBatName);
			this.Controls.Add(this.lblStep2);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.txtPath);
			this.Controls.Add(this.lblStep1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "No Hassle Shortcuts";
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStep1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtBatName;
        private System.Windows.Forms.Label lblStep2;
		private System.Windows.Forms.Label lblBatExtension;
        private System.Windows.Forms.Button btnCreateShortcut;
        private System.Windows.Forms.Label lblStep3;
        private System.Windows.Forms.Label lblStep4;
        private System.Windows.Forms.Label lblStep5;
    }
}
