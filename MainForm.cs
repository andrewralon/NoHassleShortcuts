using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TRLibrary;
// ReSharper disable LocalizableElement

namespace NoHassleShortcuts
{
	public partial class MainForm : Form
	{
		#region Fields

		private string shortcutsFolder = "Shortcuts"; // TODO - Get this from the registry!
		private string newShortcutPath = "";
		private string newShortcutName = "";
		private Admin.ShortcutType shortcutType = Admin.ShortcutType.Unknown;

		#endregion Fields

		#region Constructors

		public MainForm(string[] args)
		{
			InitializeComponent();

			// Set the title and icon
			Text += " " + Application.ProductVersion + " © " + DateTime.UtcNow.Year + " TeamRalon";
			Icon = Properties.Resources.TRfavicon;

			// Set the first argument to the path of the shortcut
			if (args.Length > 0)
			{
				if (args[0] != "")
				{
					newShortcutPath = args[0];
					txtPath.Text = args[0];
					txtPath.Focus();
					txtPath.Select(txtPath.Text.Length, 0);

					txtBatName.Select();
					txtBatName.Focus();
				}
			}

			// Set the second argument to the name of the shortcut
			if (args.Length > 1)
			{
				if (args[1] != "")
				{
					txtBatName.Text = args[1];
				}
			}

			// Set the path of the shortcuts folder
			shortcutsFolder = Path.Combine(Environment.GetEnvironmentVariable("SystemDrive") + "\\", shortcutsFolder);

			// Update the GUI with the shortcuts folder
			lblStep3.Tag = lblStep3.Text;
			lblStep3.Text += shortcutsFolder + ".";

			// Add the shortcuts folder to the system path if it's not already there
			Admin.AddToPath(shortcutsFolder, true);

			//ListCurrentShortcuts();
		}

		#endregion Constructors

		#region Private Methods

		private void Create()
		{
			newShortcutPath = txtPath.Text;
			newShortcutName = txtBatName.Text;

			if (!CheckValid())
			{
				return;
			}

			btnCreateShortcut.Enabled = false;

			// Create the shortcuts folder if it doesn't exist already
			if (!Directory.Exists(shortcutsFolder))
			{
				Directory.CreateDirectory(shortcutsFolder);
			}

			// Create the .bat file to shortcuts folder
			if (!CreateBatFile(newShortcutName, newShortcutPath, shortcutType))
			{
				MessageBox.Show("Unable to create shortcut file. Oops.");
				return;
			}

			// Make the user feel like something actually happened.... :P
			System.Threading.Thread.Sleep(300);
			System.Media.SystemSounds.Beep.Play();
			btnCreateShortcut.Enabled = true;
		}

		private bool CheckValid()
		{
			// Check fields for content
			if (newShortcutName == "")
			{
				MessageBox.Show("Please type the new shortcut name.");
				return false;
			}

			if (newShortcutPath == "")
			{
				MessageBox.Show("Please drag in a file or folder.");
				return false;
			}

			newShortcutName += ".bat";

			// Is dragged in content a file or a folder? Handle unknown type.
			shortcutType = Admin.GetShortcutType(newShortcutPath);

			if (shortcutType == Admin.ShortcutType.Unknown)
			{
				MessageBox.Show("Unable to detect a file or folder. Please try again.");
				return false;
			}

			return true;
		}

		private bool CreateBatFile(string shortcut, string target, Admin.ShortcutType shortcutType)
		{
			if (shortcutType == Admin.ShortcutType.Unknown)
			{
				return false;
			}

			string pathOnly = "";
			string filename = "";
			string savePath = Path.Combine(shortcutsFolder, shortcut);

			// Check if the shortcut file already exists
			if (File.Exists(savePath))
			{
				DialogResult dr = MessageBox.Show(this, "This shortcut file already exists: " +
					Environment.NewLine + Environment.NewLine +
					"    " + savePath + Environment.NewLine + Environment.NewLine +
					"Would you like to overwrite it with your new shortcut?",
					"Overwrite existing file?", MessageBoxButtons.YesNo);

				if (dr != DialogResult.Yes)
				{
					return false;
				}
			}

			if (shortcutType != Admin.ShortcutType.Url) // File or Folder
			{
				pathOnly = Path.GetDirectoryName(newShortcutPath);
				filename = Path.GetFileName(newShortcutPath);
			}

			// Create lines with comments and command based on type (file or folder)
			List<string> lines = new List<string>();

			lines.Add("@ECHO OFF");
			lines.Add("REM " + Text);

			if (shortcutType == Admin.ShortcutType.Url)
			{
				lines.Add("REM <url>\"" + savePath + "\"</url>");
				lines.Add("START " + SanitizeBatAndCmdEscapeCharacters(target));
			}
			else if (shortcutType == Admin.ShortcutType.File)
			{
				lines.Add("REM <file>\"" + savePath + "\"</file>");
				lines.Add("START \"\" /D \"" + pathOnly + "\" \"" + filename + "\"");
			}
			else if (shortcutType == Admin.ShortcutType.Folder)
			{
				lines.Add("REM <folder>\"" + savePath + "\"</folder>");
				lines.Add("\"%SystemRoot%\\explorer.exe\" \"" + newShortcutPath + "\"");
			}

			lines.Add("EXIT");

			// Write the file to the given save path
			File.WriteAllLines(savePath, lines.ToArray());

			return true;
		}

		private string SanitizeBatAndCmdEscapeCharacters(string target)
		{
			// TODO - USE REGEX INSTEAD!
			// TODO - Check for ^& and %% in existing string instead of blindly replacing

			// CMD uses & for commands, so replace it with ^&
			target = target.Replace("&", "^&");

			// Bat files use % for commands, so replace it with %%
			return target.Replace("%", "%%");
		}

		private string FollowLink(string linkPath)
		{
			// If it's a shortcut, follow it!
			var target = Shell.FollowShortcut(linkPath);

			if (File.Exists(target) || Directory.Exists(target))
			{
				return target;
			}

			return "";
		}

		private void ListCurrentShortcuts() // TODO - After MVP is working....
		{
			var bats = Directory.GetFiles(shortcutsFolder, "*.bat", SearchOption.TopDirectoryOnly);

			List<string> shortcuts = new List<string>();

			shortcuts.Add("Shortcut, File, Path");
			shortcuts.Add("--------------------");

			List<string> filenames = new List<string>();

			foreach (var bat in bats)
			{
				filenames.Add(GetFilename(bat));
				shortcuts.Add(Path.GetFileNameWithoutExtension(bat));
			}

			MessageBox.Show(string.Join(Environment.NewLine, shortcuts.ToArray()));

			// If this absolutely fails, just open the shortcuts folder
		}

		private string GetFilename(string path) // TODO - After MVP is working....
		{
			var lines = File.ReadAllLines(path);

			foreach (var line in lines)
			{

			}

			return "";
		}

		private void SetPath(string path)
		{
			// Check if it exists
			if (path != "" && (File.Exists(path) || Directory.Exists(path)))
			{
				// Follow the link (if it exists) and set the path textbox
				txtPath.Text = FollowLink(path);

				// Focus on the shortcut name textbox
				txtBatName.Focus();

				// Activate this window (normally keeps focus on whatever was previously active)
				Admin.ActivateThisWindow();
			}
			else
			{
				MessageBox.Show("Unable to set the path. Please drag in a file or a folder again.");
			}
		}

        #endregion Private Methods

        #region Public Methods

        #endregion Public Methods

        #region Handlers

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void txtBatName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				Create();
			}
		}

		private void btnCreateShortcut_Click(object sender, EventArgs e)
		{
			Create();
		}

		private void btnOpenShortcuts_Click(object sender, EventArgs e)
		{
			Process.Start("\"" + shortcutsFolder + "\"");
		}

		#region DragAndDrop Handlers

		private void HandleDragEnter(object sender, DragEventArgs e)
		{
			Dragging.DragAndEnter(sender, e);
		}

		private void HandleDragDrop(object sender, DragEventArgs e)
		{
			SetPath(Dragging.GetDroppedFiles(sender, e).FirstOrDefault());
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

        #endregion Handlers
    }
}
