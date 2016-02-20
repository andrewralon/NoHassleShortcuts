namespace NoHassleShortcuts
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
			this.txtBatName = new System.Windows.Forms.TextBox();
			this.lblStep2 = new System.Windows.Forms.Label();
			this.lblBatExtension = new System.Windows.Forms.Label();
			this.btnCreateShortcut = new System.Windows.Forms.Button();
			this.lblStep3 = new System.Windows.Forms.Label();
			this.lblStep4 = new System.Windows.Forms.Label();
			this.btnOpenShortcuts = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblStep1
			// 
			this.lblStep1.AutoSize = true;
			this.lblStep1.Location = new System.Drawing.Point(28, 21);
			this.lblStep1.Name = "lblStep1";
			this.lblStep1.Size = new System.Drawing.Size(287, 20);
			this.lblStep1.TabIndex = 0;
			this.lblStep1.Text = "1. Drag in or paste a file, folder, or URL.";
			this.lblStep1.DragDrop += new System.Windows.Forms.DragEventHandler(this.lblStep1_DragDrop);
			this.lblStep1.DragEnter += new System.Windows.Forms.DragEventHandler(this.lblStep1_DragEnter);
			// 
			// txtPath
			// 
			this.txtPath.Location = new System.Drawing.Point(32, 63);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(332, 26);
			this.txtPath.TabIndex = 5;
			this.txtPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtPath_DragDrop);
			this.txtPath.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtPath_DragEnter);
			// 
			// txtBatName
			// 
			this.txtBatName.Location = new System.Drawing.Point(115, 162);
			this.txtBatName.Name = "txtBatName";
			this.txtBatName.Size = new System.Drawing.Size(127, 26);
			this.txtBatName.TabIndex = 15;
			this.txtBatName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtBatName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatName_KeyDown);
			// 
			// lblStep2
			// 
			this.lblStep2.AutoSize = true;
			this.lblStep2.Location = new System.Drawing.Point(28, 120);
			this.lblStep2.Name = "lblStep2";
			this.lblStep2.Size = new System.Drawing.Size(254, 20);
			this.lblStep2.TabIndex = 3;
			this.lblStep2.Text = "2. Choose a name for the shortcut.";
			this.lblStep2.DragDrop += new System.Windows.Forms.DragEventHandler(this.lblStep2_DragDrop);
			this.lblStep2.DragEnter += new System.Windows.Forms.DragEventHandler(this.lblStep2_DragEnter);
			// 
			// lblBatExtension
			// 
			this.lblBatExtension.AutoSize = true;
			this.lblBatExtension.Location = new System.Drawing.Point(248, 165);
			this.lblBatExtension.Name = "lblBatExtension";
			this.lblBatExtension.Size = new System.Drawing.Size(36, 20);
			this.lblBatExtension.TabIndex = 6;
			this.lblBatExtension.Text = ".bat";
			// 
			// btnCreateShortcut
			// 
			this.btnCreateShortcut.AutoSize = true;
			this.btnCreateShortcut.Location = new System.Drawing.Point(115, 261);
			this.btnCreateShortcut.Name = "btnCreateShortcut";
			this.btnCreateShortcut.Size = new System.Drawing.Size(169, 30);
			this.btnCreateShortcut.TabIndex = 25;
			this.btnCreateShortcut.Text = "Create Shortcut";
			this.btnCreateShortcut.UseVisualStyleBackColor = true;
			this.btnCreateShortcut.Click += new System.EventHandler(this.btnCreateShortcut_Click);
			// 
			// lblStep3
			// 
			this.lblStep3.AutoSize = true;
			this.lblStep3.Location = new System.Drawing.Point(28, 221);
			this.lblStep3.Name = "lblStep3";
			this.lblStep3.Size = new System.Drawing.Size(183, 20);
			this.lblStep3.TabIndex = 8;
			this.lblStep3.Text = "3. Create the shortcut in ";
			// 
			// lblStep4
			// 
			this.lblStep4.AutoSize = true;
			this.lblStep4.Location = new System.Drawing.Point(28, 323);
			this.lblStep4.Name = "lblStep4";
			this.lblStep4.Size = new System.Drawing.Size(325, 40);
			this.lblStep4.TabIndex = 12;
			this.lblStep4.Text = "Press Windows + R, type the shortcut name, \r\nand press Enter to use. Enjoy!";
			// 
			// btnOpenShortcuts
			// 
			this.btnOpenShortcuts.AutoSize = true;
			this.btnOpenShortcuts.Location = new System.Drawing.Point(115, 390);
			this.btnOpenShortcuts.Name = "btnOpenShortcuts";
			this.btnOpenShortcuts.Size = new System.Drawing.Size(169, 30);
			this.btnOpenShortcuts.TabIndex = 26;
			this.btnOpenShortcuts.Text = "Open Shortcuts";
			this.btnOpenShortcuts.UseVisualStyleBackColor = true;
			this.btnOpenShortcuts.Click += new System.EventHandler(this.btnOpenShortcuts_Click);
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(399, 449);
			this.Controls.Add(this.btnOpenShortcuts);
			this.Controls.Add(this.lblStep4);
			this.Controls.Add(this.btnCreateShortcut);
			this.Controls.Add(this.lblStep3);
			this.Controls.Add(this.lblBatExtension);
			this.Controls.Add(this.txtBatName);
			this.Controls.Add(this.lblStep2);
			this.Controls.Add(this.txtPath);
			this.Controls.Add(this.lblStep1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "No Hassle Shortcuts";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStep1;
		private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.TextBox txtBatName;
        private System.Windows.Forms.Label lblStep2;
		private System.Windows.Forms.Label lblBatExtension;
        private System.Windows.Forms.Button btnCreateShortcut;
        private System.Windows.Forms.Label lblStep3;
		private System.Windows.Forms.Label lblStep4;
		private System.Windows.Forms.Button btnOpenShortcuts;
    }
}

