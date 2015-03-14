using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TRLibrary;

namespace NoHassleShortcuts
{
    public partial class MainForm : Form
    {
        #region Fields

        private OpenFileDialog ofd = new OpenFileDialog();
		private string saveFolder = "Shortcuts";
        private string shortcutPath = "";

        #endregion Fields

        #region Constructors

        public MainForm(string[] args)
        {
            InitializeComponent();

			this.Tag = this.Text;
			this.Text += " " + Application.ProductVersion + " © " + DateTime.UtcNow.Year + " TeamRalon";
			Icon = Properties.Resources.TR_64x64_icon;

			if (args.Length > 0)
			{
				if (args[0] != "")
				{
					shortcutPath = args[0];
					txtPath.Text = args[0];
					txtPath.Focus();
					txtPath.Select(txtPath.Text.Length, 0);

					txtBatName.Select();
					txtBatName.Focus();
				}
			}

			if (args.Length > 1)
			{
				if (args[1] != "")
				{
					txtBatName.Text = args[1];
				}
			}

			string cDrive = Environment.GetEnvironmentVariable("SystemDrive");
			saveFolder = cDrive + "\\" + saveFolder;

			lblStep3.Tag = lblStep3.Text;
			lblStep3.Text += saveFolder + ".";

            ofd.Title = "Choose a file or folder to shortcut";

			Admin.AddToPath(saveFolder, true);

			//ListCurrentShortcuts();
		}

        #endregion Constructors

        #region Private Methods

        private void Create(string newShortcut)
        {
			if (newShortcut == "")
			{
				MessageBox.Show("Unable to parse the new shortcut name.");
				return;
			}

			if (shortcutPath == "")
			{
				MessageBox.Show("Please drag in a file or folder to shortcut.");
				return;
			}

			if (txtBatName.Text == "")
			{
				MessageBox.Show("Type the name of the shortcut.");
				return;
			}

			newShortcut += ".bat";

			btnCreateShortcut.Enabled = false;

			Admin.ShortcutType type = Admin.GetShortcutType(shortcutPath);
			if (type == Admin.ShortcutType.Unknown)
			{
				MessageBox.Show("Unable to detect a file or folder. Please try again.");
				return;
			}

			string pathOnly = Path.GetDirectoryName(shortcutPath);
			string filename = Path.GetFileName(shortcutPath);
			string savePath = Path.Combine(saveFolder, newShortcut);

			if (!Directory.Exists(saveFolder))
			{
				Directory.CreateDirectory(saveFolder);
			}

			using (StreamWriter sw = new StreamWriter(savePath, false))
            {
				sw.WriteLine("@ECHO OFF");
				sw.WriteLine("REM " + this.Text);

				if (type == Admin.ShortcutType.File)
				{
					sw.WriteLine("REM <file>\"" + savePath + "\"</file>");
					sw.WriteLine("START \"\" /D \"" + pathOnly + "\" \"" + filename + "\"");
				}
				else if (type == Admin.ShortcutType.Folder)
				{
					sw.WriteLine("REM <folder>\"" + savePath + "\"</folder>");
					sw.WriteLine("\"%SystemRoot%\\explorer.exe\" \"" + shortcutPath + "\"");
				}

				sw.WriteLine("EXIT");
				sw.Close();
            }

			System.Threading.Thread.Sleep(300);
			System.Media.SystemSounds.Beep.Play();
			btnCreateShortcut.Enabled = true;
        }

		private string FollowLink(string linkPath)
		{
			// If it's a shortcut, follow it!
			string target = Shell.FollowShortcut(linkPath);
			if (File.Exists(target) || Directory.Exists(target))
			{
				return target;
			}

			return "";
			//return Link.ResolveLink(linkPath);
		}

		private void ListCurrentShortcuts() // LATER!! 
		{
			string[] bats = Directory.GetFiles(saveFolder, "*.bat", SearchOption.TopDirectoryOnly);

			List<string> shortcuts = new List<string>();
			shortcuts.Add("Shortcut, File, Path");
			shortcuts.Add("--------------------");

			List<string> filenames = new List<string>();

			foreach (string bat in bats)
			{
				filenames.Add(GetFilename(bat));
				shortcuts.Add(Path.GetFileNameWithoutExtension(bat));
			}

			MessageBox.Show(String.Join(Environment.NewLine, shortcuts.ToArray()));

			// If this absolutely fails, just open the shortcuts folder!
		}

		private string GetFilename(string path)
		{
			string[] lines = File.ReadAllLines(path);

			foreach (string line in lines)
			{

			}

			return "";
		}

		private void SetPath(string path)
		{
			// Check if it exists
			if (path != "" && (File.Exists(path) || Directory.Exists(path))) 
			{
				string target = FollowLink(path);

				shortcutPath = target;// path;
				txtPath.Text = target;// path;
				txtPath.Focus();
				txtPath.Select(txtPath.Text.Length, 0);
			}
			else
			{
				MessageBox.Show("Unable to set the path."); // REMOVE ME LATER!
			}
		}

        #endregion Private Methods

        #region Public Methods

        #endregion Public Methods

        #region Handlers

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ofd.CheckFileExists)
                {
                    SetPath(ofd.FileName);
                }
            }
        }

		private void txtBatName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				Create(txtBatName.Text);
			}
		}

        private void btnCreateShortcut_Click(object sender, EventArgs e)
        {
			Create(txtBatName.Text);
        }

		private void btnOpenShortcuts_Click(object sender, EventArgs e)
		{
			Process.Start("\"" + saveFolder + "\"");
		}

        #endregion Handlers

        #region DragAndDrop Handlers

        private void HandleDragEnter(object sender, DragEventArgs e)
        {
            Dragging.DragAndEnter(sender, e);
        }

        private void HandleDragDrop(object sender, DragEventArgs e)
        {
            string path = Dragging.GetDroppedFiles(sender, e).First();
            SetPath(path);
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            HandleDragDrop(sender, e);
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            HandleDragEnter(sender, e);
        }

        private void txtPath_DragDrop(object sender, DragEventArgs e)
        {
            HandleDragDrop(sender, e);
        }

        private void txtPath_DragEnter(object sender, DragEventArgs e)
        {
            HandleDragEnter(sender, e);
        }

        private void lblStep1_DragDrop(object sender, DragEventArgs e)
        {
            HandleDragDrop(sender, e);
        }

        private void lblStep1_DragEnter(object sender, DragEventArgs e)
        {
            HandleDragEnter(sender, e);
        }

        private void btnBrowse_DragDrop(object sender, DragEventArgs e)
        {
            HandleDragDrop(sender, e);
        }

        private void btnBrowse_DragEnter(object sender, DragEventArgs e)
        {
            HandleDragEnter(sender, e);
        }

        private void lblStep2_DragDrop(object sender, DragEventArgs e)
        {
            HandleDragDrop(sender, e);
        }

        private void lblStep2_DragEnter(object sender, DragEventArgs e)
        {
            HandleDragEnter(sender, e);
        }

        #endregion DragAndDrop Handlers
    }
}
