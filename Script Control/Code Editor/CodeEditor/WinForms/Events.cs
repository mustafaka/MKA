using System;
namespace AIMS.Libraries.CodeEditor.WinForms
{
 	/// <summary>
	/// 
	/// </summary>
	public class ClickLinkEventArgs : EventArgs
	{
		/// <summary>
		/// 
		/// </summary>
		public string Link = "";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Link"></param>
		public ClickLinkEventArgs(string Link)
		{
			this.Link = Link;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public delegate void ClickLinkEventHandler(object sender, ClickLinkEventArgs e);
}