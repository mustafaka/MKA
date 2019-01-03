// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Daniel Grunwald" email="daniel@danielgrunwald.de"/>
//     <version>$Revision: 1965 $</version>
// </file>

using System;

using AIMS.Libraries.Scripting.Dom;
using AIMS.Libraries.CodeEditor;
using AIMS.Libraries.CodeEditor.WinForms;
using AIMS.Libraries.CodeEditor.WinForms.CompletionWindow;
using AIMS.Libraries.CodeEditor.Syntax;
using AIMS.Libraries.Scripting.Dom.CSharp;

namespace AIMS.Libraries.Scripting.ScriptControl.CodeCompletion
{
	/// <summary>
	/// Provides code completion for attribute names.
	/// </summary>
	public class AttributesDataProvider : CtrlSpaceCompletionDataProvider
	{
		public AttributesDataProvider(IProjectContent pc)
			: this(ExpressionContext.TypeDerivingFrom(pc.GetClass("System.Attribute"), true))
		{
		}
		
		public AttributesDataProvider(ExpressionContext context) : base(context)
		{
			this.ForceNewExpression = true;
		}
		
		bool removeAttributeSuffix = true;
		
		public bool RemoveAttributeSuffix {
			get {
				return removeAttributeSuffix;
			}
			set {
				removeAttributeSuffix = value;
			}
		}
		
		public override ICompletionData[] GenerateCompletionData(string fileName, EditViewControl textArea, char charTyped)
		{
			ICompletionData[] data = base.GenerateCompletionData(fileName, textArea, charTyped);
			if (removeAttributeSuffix) {
				foreach (ICompletionData d in data) {
					if (d.Text.EndsWith("Attribute")) {
						d.Text = d.Text.Substring(0, d.Text.Length - 9);
					}
				}
			}
			return data;
		}
	}
}
