using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using TRLibrary;

namespace NoHassleShortcuts
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
			// TODO - Load the shortcuts folder path from the registry!

			// Remove the "App" string from the un-merged application
			var source = Assembly.GetEntryAssembly().Location.Replace("App.exe", ".exe");
			var filename = Path.GetFileName(source);
			var shortcuts = Path.Combine(Environment.GetEnvironmentVariable("SystemDrive") + "\\", "Shortcuts", filename);

			var menu = "Make a new No Hassle Shortcut!";
			var folder = "TeamRalon";
			var command = "\"" + shortcuts + "\" \"%1\"";

			// Remove existing right click menus in case they are out of date
			Admin.RemoveRightClickMenu(folder, menu, Admin.ShortcutType.File);
			Admin.RemoveRightClickMenu(folder, menu, Admin.ShortcutType.Folder);

			// Add right click menu for files and folders
			Admin.NewRightClickMenu(folder, menu, command, Admin.ShortcutType.File);
			Admin.NewRightClickMenu(folder, menu, command, Admin.ShortcutType.Folder);

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

			// Run the application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(args));
        }
    }
}
