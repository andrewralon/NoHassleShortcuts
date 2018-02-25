using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using TRLibrary;

namespace NoHassleShortcuts
{
    public class Main
    {
        #region Fields

        //private MainForm _mainForm;

        // TODO - Get this from the registry!
        public string ShortcutsFolder { get; private set; } = "Shortcuts";

        public string NewShortcutPath { get; private set; } = "";

        public string NewShortcutName { get; private set; }

        public ShortcutType ShortcutType { get; private set; } = ShortcutType.Unknown;

        #endregion Fields

        #region Constructors

        public Main(string[] args)
        {
            GetArgs(args);

            LoadRegistryValues();

            AddShortcutsFolderToSystemPath();

            CreateContextMenuEntries();

            //_mainForm = new MainForm();
            //Run(_mainForm);
            Run();
        }

        #endregion Constructors

        #region Private Methods

        #region Private Methods - Main

        private void GetArgs(string[] args)
        {
            // Arg 1 is the shortcut's path
            if (args.Length > 0 && args[0] != "")
            {
                NewShortcutPath = args[0];
            }

            // Arg 2 is shortcut's name
            if (args.Length > 1 && args[1] != "")
            {
                NewShortcutName = args[1];
            }
        }

        private void LoadRegistryValues()
        {
            // TODO - Load the shortcuts folder path from the registry!
        }

        private void AddShortcutsFolderToSystemPath()
        {
            // Set the path of the shortcuts folder
            ShortcutsFolder = Environment.GetEnvironmentVariable("SystemDrive") + "\\" + ShortcutsFolder;

            // Update the GUI with the shortcuts folder
            lblStep3.Tag = lblStep3.Text;
            lblStep3.Text += ShortcutsFolder + ".";

            // Add the shortcuts folder to the system path if it's not already there
            Admin.AddToPath(ShortcutsFolder, true);
        }

        private void CreateContextMenuEntries()
        {
            // Remove the "App" string from the un-merged application
            var source = Assembly.GetEntryAssembly().Location.Replace("App.exe", ".exe");
            var filename = Path.GetFileName(source);
            var shortcuts = Environment.GetEnvironmentVariable("SystemDrive") + "\\" + "Shortcuts" + "\\" + filename;

            var menu = "Make a new No Hassle Shortcut!";
            var folder = "TeamRalon";
            var command = "\"" + shortcuts + "\" \"%1\"";

            // Remove existing right click menus in case they are out of date
            Admin.RemoveRightClickMenu(folder, menu, ShortcutType.File);
            Admin.RemoveRightClickMenu(folder, menu, ShortcutType.Folder);

            // Add right click menu for files and folders
            Admin.NewRightClickMenu(folder, menu, command, ShortcutType.File);
            Admin.NewRightClickMenu(folder, menu, command, ShortcutType.Folder);

            try
            {
                // Copy the app to the shortcuts folder (and overwrite if it already exists)
                File.Copy(source, shortcuts, true);
            }
            catch //(Exception ex)
            {
                // Ignore the possibility it can't overwrite itself (for now) and just move on
                //MessageBox.Show("Unable to copy the file to this location: \"" + shortcuts + "\"" +
                //	Environment.NewLine + Environment.NewLine +
                //	ex.ToString());
            }
        }

        private void Run() //MainForm mainForm)
        {
            // Run the application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(this));
        }

        #endregion Private Methods - Main

        private bool CheckValid()
        {
            // Check fields for content
            if (NewShortcutName == "")
            {
                MessageBox.Show("Please type the new shortcut name.");
                return false;
            }

            if (NewShortcutPath == "")
            {
                MessageBox.Show("Please drag in a file or folder.");
                return false;
            }

            NewShortcutName += ".bat";

            // Is dragged in content a file or a folder? Handle unknown type.
            ShortcutType = Admin.GetShortcutType(NewShortcutPath);

            if (ShortcutType == ShortcutType.Unknown)
            {
                MessageBox.Show("Unable to detect a file or folder. Please try again.");
                return false;
            }

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



        #endregion Private Methods

        #region Public Methods

        public void Create()
        {
            NewShortcutPath = txtPath.Text;
            NewShortcutName = txtBatName.Text;

            if (!CheckValid())
            {
                return;
            }

            btnCreateShortcut.Enabled = false;

            TRLibrary.Shortcut.CreateShortcutsFolder(ShortcutsFolder);

            TRLibrary.Shortcut.Create(NewShortcutPath, NewShortcutName, ShortcutType);

            // Create the .bat file to shortcuts folder
            if (!CreateBatFile(NewShortcutName, NewShortcutPath, ShortcutType))
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

        public string SetShortcutPath(string path)
        {
            string newPath;

            // Check if it exists
            if (path != "" &&
                (File.Exists(path) || Directory.Exists(path) || Admin.IsValidUrl(path)))
            {
                // Follow the link (if it exists) and set the path textbox
                newPath = FollowLink(path);

                // Focus on the shortcut name textbox
                txtBatName.Focus();

                // Activate this window (normally keeps focus on whatever was previously active)
                Admin.ActivateThisWindow();
            }
            else
            {
                MessageBox.Show("Unable to set the path. Please drag in or paste a file, folder, or URL again.");
            }

            return newPath;
        }

        public void OpenShortcutsFolder()
        {
            Process.Start("\"" + ShortcutsFolder + "\"");
        }

        public bool CreateBatFile(string shortcut, string target, ShortcutType shortcutType)
        {
            if (shortcutType == ShortcutType.Unknown)
            {
                return false;
            }

            string pathOnly = "";
            string filename = "";
            string savePath = Path.Combine(ShortcutsFolder, shortcut);

            // Check if the shortcut file already exists
            if (File.Exists(savePath))
            {
                DialogResult dr = MessageBox.Show(_mainForm, "This shortcut file already exists: " +
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
                pathOnly = Path.GetDirectoryName(NewShortcutPath);
                filename = Path.GetFileName(NewShortcutPath);
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
                lines.Add("\"%SystemRoot%\\explorer.exe\" \"" + NewShortcutPath + "\"");
            }

            lines.Add("EXIT");

            // Write the file to the given save path
            File.WriteAllLines(savePath, lines.ToArray()); //, Encoding.UTF8);

            return true;
        }

        #endregion Public Methods
    }
}
