using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

		private enum ShortcutType { File, Folder, Unknown };

        #endregion Fields

        #region Constructor

        public MainForm()
        {
            InitializeComponent();

			this.Tag = this.Text;
			this.Text += " " + Application.ProductVersion + " © Team Ralon";

			string cDrive = Environment.GetEnvironmentVariable("SystemDrive");
			saveFolder = cDrive + "\\" + saveFolder;

			lblStep3.Tag = lblStep3.Text;
			lblStep3.Text += saveFolder + ".";

            ofd.Title = "Choose a file or folder to shortcut";

			Admin.AddToPath(saveFolder, true);

			//ListCurrentShortcuts();
		}

        #endregion Constructor

        #region Private Methods

        private void Create(string newShortcut)
        {
			if (txtBatName.Text == "" || shortcutPath == "")
			{
				MessageBox.Show("Please drag in a file or folder and name your shortcut.");
				return;
			}

			btnCreateShortcut.Enabled = false;

			ShortcutType type = GetShortcutType();
			if (type == ShortcutType.Unknown)
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
				sw.WriteLine("REM No Hassle Shortcuts © Team Ralon");

				if (type == ShortcutType.File)
				{
					sw.WriteLine("REM File: " + savePath);
					sw.WriteLine("START \"\" /D \"" + pathOnly + "\" \"" + filename + "\"");
				}
				else if (type == ShortcutType.Folder)
				{
					sw.WriteLine("REM Folder: " + savePath);
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

			// if this absolutely fails, just open the shortcuts folder!
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

		private ShortcutType GetShortcutType()
		{
			FileAttributes attr = File.GetAttributes(shortcutPath);
			if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
			{
				return ShortcutType.Folder;
			}
			else if (File.Exists(shortcutPath))
			{
				return ShortcutType.File;
			}
			else
			{
				return ShortcutType.Unknown;
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
				Create(txtBatName.Text + ".bat");
			}
		}

        private void btnCreateShortcut_Click(object sender, EventArgs e)
        {
			Create(txtBatName.Text + ".bat");
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
