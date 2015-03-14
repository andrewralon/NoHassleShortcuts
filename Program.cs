using System;
using System.Collections.Generic;
using System.Linq;
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
			string folder = "TeamRalon";
			string menu = "Make a new No Hassle Shortcut!";
			string command = "\"C:\\Shortcuts\\NoHassleShortcuts.exe\" \"%1\"";

			Admin.RemoveRightClickMenu(folder, menu, Admin.ShortcutType.File);
			Admin.RemoveRightClickMenu(folder, menu, Admin.ShortcutType.Folder);

			Admin.NewRightClickMenu(folder, menu, command, Admin.ShortcutType.File);
			Admin.NewRightClickMenu(folder, menu, command, Admin.ShortcutType.Folder);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(args));
        }
    }
}
