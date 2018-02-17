using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TRLibrary;
using ShortcutType = TRLibrary.Admin.ShortcutType;
// ReSharper disable LocalizableElement

namespace NoHassleShortcuts
{
	public partial class MainForm : Form
	{
		#region Fields

		private string _shortcutsFolder = "Shortcuts"; // TODO - Get this from the registry!
		private string _newShortcutPath = "";
		private string _newShortcutName = "";
		private ShortcutType _shortcutType = ShortcutType.Unknown;

		#endregion Fields

		#region Constructors

		public MainForm(string[] args)
		{
			InitializeComponent();

			// Set the first argument to the path of the shortcut
			if (args.Length > 0)
			{
				if (args[0] != "")
				{
					_newShortcutPath = args[0];
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
			_shortcutsFolder = Environment.GetEnvironmentVariable("SystemDrive") + "\\" + _shortcutsFolder;

			// Update the GUI with the shortcuts folder
			lblStep3.Tag = lblStep3.Text;
			lblStep3.Text += _shortcutsFolder + ".";

			// Add the shortcuts folder to the system path if it's not already there
			Admin.AddToPath(_shortcutsFolder, true);
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			// Set the title and icon
			Text += " " + Application.ProductVersion + " © " + DateTime.UtcNow.Year + " TeamRalon";
			Icon = Properties.Resources.TRfavicon;
		}

		#endregion Constructors

		#region Private Methods

		private void Create()
		{
			_newShortcutPath = txtPath.Text;
			_newShortcutName = txtBatName.Text;

			if (!CheckValid())
			{
				return;
			}

			btnCreateShortcut.Enabled = false;

			// Create the shortcuts folder if it doesn't exist already
			if (!Directory.Exists(_shortcutsFolder))
			{
				Directory.CreateDirectory(_shortcutsFolder);
			}

			// Create the .bat file to shortcuts folder
			if (!CreateBatFile(_newShortcutName, _newShortcutPath, _shortcutType))
			{
				MessageBox.Show("Unable to create shortcut file. Oops.");
                btnCreateShortcut.Enabled = true;
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
			if (_newShortcutName == "")
			{
				MessageBox.Show("Please type the new shortcut name.");
				return false;
			}

			if (_newShortcutPath == "")
			{
				MessageBox.Show("Please drag in a file or folder.");
				return false;
			}

			_newShortcutName += ".bat";

			// Is dragged in content a file or a folder? Handle unknown type.
			_shortcutType = Admin.GetShortcutType(_newShortcutPath);

			if (_shortcutType == ShortcutType.Unknown)
			{
				MessageBox.Show("Unable to detect a file or folder. Please try again.");
				return false;
			}

			return true;
		}

		private bool CreateBatFile(string shortcut, string target, ShortcutType shortcutType)
		{
			if (shortcutType == ShortcutType.Unknown)
			{
				return false;
			}

			string pathOnly = "";
			string filename = "";
			string savePath = Path.Combine(_shortcutsFolder, shortcut);

			// Check if the shortcut file already exists
			if (File.Exists(savePath))
			{
				DialogResult dr = MessageBox.Show(this, "This shortcut file already exists: " +
					Environment.NewLine + Environment.NewLine +
					"    " + savePath + 
					Environment.NewLine + Environment.NewLine +
					"Would you like to overwrite it with your new shortcut?",
					"Overwrite existing file?", MessageBoxButtons.YesNo);

				if (dr != DialogResult.Yes)
				{
					return false;
				}
			}

			if (shortcutType != ShortcutType.Url) // File or Folder
			{
				pathOnly = Path.GetDirectoryName(_newShortcutPath);
				filename = Path.GetFileName(_newShortcutPath);
			}

            // Create lines with comments and command based on type (file or folder)
            string shortcutTypeLower = shortcutType.ToString().ToLower();
            List<string> lines = new List<string>
            {
                "@ECHO OFF",
                "REM " + Text,
                "REM <" + shortcutTypeLower + ">" + savePath + "</" + shortcutTypeLower + ">"
            };
            //lines.Add("CHCP 65001>NUL");

            if (shortcutType == ShortcutType.Url)
			{
				lines.Add("START " + SanitizeBatAndCmdEscapeCharacters(target));
			}
			else if (shortcutType == ShortcutType.File)
			{
				lines.Add("START \"\" /D \"" + pathOnly + "\" \"" + filename + "\"");
			}
			else if (shortcutType == ShortcutType.Folder)
			{
				lines.Add("\"%SystemRoot%\\explorer.exe\" \"" + _newShortcutPath + "\"");
			}

			lines.Add("EXIT");

            // Write the file to the given save path
            File.WriteAllLines(savePath, lines.ToArray()); //, Encoding.UTF8);

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

			if (File.Exists(target) || Directory.Exists(target) || Admin.IsValidUrl(target))
			{
				return target;
			}

			return "";
		}

		private void SetPath(string path)
		{
			// Check if it exists
			if (path != "" && 
				(File.Exists(path) || Directory.Exists(path) || Admin.IsValidUrl(path)))
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
				MessageBox.Show("Unable to set the path. Please drag in or paste a file, folder, or URL again.");
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

		private void TxtBatName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				Create();
			}
		}

		private void BtnCreateShortcut_Click(object sender, EventArgs e)
		{
			Create();
		}

		private void BtnOpenShortcuts_Click(object sender, EventArgs e)
		{
			Process.Start("\"" + _shortcutsFolder + "\"");
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

		private void TxtPath_DragDrop(object sender, DragEventArgs e)
		{
			HandleDragDrop(sender, e);
		}

		private void TxtPath_DragEnter(object sender, DragEventArgs e)
		{
			HandleDragEnter(sender, e);
		}

		private void LblStep1_DragDrop(object sender, DragEventArgs e)
		{
			HandleDragDrop(sender, e);
		}

		private void LblStep1_DragEnter(object sender, DragEventArgs e)
		{
			HandleDragEnter(sender, e);
		}

		private void LblStep2_DragDrop(object sender, DragEventArgs e)
		{
			HandleDragDrop(sender, e);
		}

		private void LblStep2_DragEnter(object sender, DragEventArgs e)
		{
			HandleDragEnter(sender, e);
		}

		#endregion DragAndDrop Handlers

		#endregion Handlers
	}
}
