using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
			//string RightClickMenuName = "Make a shortcut to this!";
			//string RightClickCommandPath = "\"C:\\Shortcuts\\NoHassleShortcuts.exe\" \"%1\"";

			//TRLibrary.Admin.NewRightClickMenu(RightClickMenuName, RightClickCommandPath, TRLibrary.Admin.ShortcutType.File);
			//TRLibrary.Admin.NewRightClickMenu(RightClickMenuName, RightClickCommandPath, TRLibrary.Admin.ShortcutType.Folder);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(args));
			//Application.Run(new MainForm(new string[] { @"" }));
        }
    }
}
