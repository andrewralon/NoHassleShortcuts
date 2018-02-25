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

        private Main _main;

        #endregion Fields

        #region Constructors

        public MainForm(Main main)
		{
            _main = main;

			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			// Set the title and icon
			Text += " " + Application.ProductVersion + " by TeamRalon";
			Icon = Properties.Resources.TRfavicon;

            txtPath.Text = _main.NewShortcutPath;
            txtPath.Focus();
            txtPath.Select(txtPath.Text.Length, 0);

            txtBatName.Select();
            txtBatName.Focus();
        }

		#endregion Constructors

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
				_main.Create();
			}
		}

		private void BtnCreateShortcut_Click(object sender, EventArgs e)
		{
			_main.Create();
		}

		private void BtnOpenShortcuts_Click(object sender, EventArgs e)
		{
            _main.OpenShortcutsFolder();
		}

		#region DragAndDrop Handlers

		private void HandleDragEnter(object sender, DragEventArgs e)
		{
			Dragging.DragAndEnter(sender, e);
		}

		private void HandleDragDrop(object sender, DragEventArgs e)
		{
			_main.SetShortcutPath(Dragging.GetDroppedFiles(sender, e).FirstOrDefault());
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
