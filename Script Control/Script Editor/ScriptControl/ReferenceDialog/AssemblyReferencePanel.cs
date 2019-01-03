﻿
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

//using ICSharpCode.Core;

namespace AIMS.Libraries.Scripting.ScriptControl.ReferenceDialog
{
	public class AssemblyReferencePanel : Panel, IReferencePanel
	{
		ISelectReferenceDialog selectDialog;
		
		public AssemblyReferencePanel(ISelectReferenceDialog selectDialog)
		{
			this.selectDialog = selectDialog;
			
			Button browseButton   = new Button();
			browseButton.Location = new Point(10, 10);
			
			browseButton.Text     = "Browse";
			browseButton.Click   += new EventHandler(SelectReferenceDialog);
			browseButton.FlatStyle = FlatStyle.System;
			Controls.Add(browseButton);
		}
		
		void SelectReferenceDialog(object sender, EventArgs e)
		{
			using (OpenFileDialog fdiag  = new OpenFileDialog()) {
				fdiag.AddExtension    = true;
				
				fdiag.Filter = "AssemblyFiles|*.dll;*.exe|AllFiles}|*.*";
				fdiag.Multiselect     = true;
				fdiag.CheckFileExists = true;
				
				if (fdiag.ShowDialog() == DialogResult.OK) {
					foreach (string file in fdiag.FileNames) {
						selectDialog.AddReference(ReferenceType.Assembly,
						                          Path.GetFileName(file),
						                          file,
						                          null);
						
					}
				}
			}
		}
		
		public void AddReference()
		{
			SelectReferenceDialog(null, null);
		}
	}
}
