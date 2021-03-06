using System;
using System.Collections.Generic;
using System.IO;
using AIMS.Libraries.Forms.Docking;
using AIMS.Libraries.CodeEditor.SyntaxFiles;
using AIMS.Libraries.Scripting.NRefactory;
using AIMS.Libraries.CodeEditor.Syntax;

namespace AIMS.Libraries.Scripting.ScriptControl.Converter
{
    static class CodeConverter
    {
        public static string ConvertCode(string SourceCode, ScriptLanguage SourceLanguage, ScriptLanguage TargetLanguage)
        {
            if (SourceLanguage == TargetLanguage || SourceCode.Length == 0) return SourceCode;

            if (SourceLanguage == ScriptLanguage.CSharp)
                return ConvertCSharpCodeToVb(SourceCode);
            else
                return ConvertVBCodeToCSharp(SourceCode);
        }

        private static string ConvertVBCodeToCSharp(string Code)
        {
            IParser p = ParserFactory.CreateParser(SupportedLanguage.VBNet, new StringReader(Code));
            p.Parse();
            if (p.Errors.Count > 0)
            {
                return Code;
            }
            NRefactory.PrettyPrinter.CSharpOutputVisitor output = new NRefactory.PrettyPrinter.CSharpOutputVisitor();
            List<ISpecial> specials = p.Lexer.SpecialTracker.CurrentSpecials;
            PreprocessingDirective.VBToCSharp(specials);
            p.CompilationUnit.AcceptVisitor(new NRefactory.Visitors.VBNetConstructsConvertVisitor(), null);
            p.CompilationUnit.AcceptVisitor(new NRefactory.Visitors.ToCSharpConvertVisitor(), null);

            using (NRefactory.PrettyPrinter.SpecialNodesInserter.Install(specials, output))
            {
                output.VisitCompilationUnit(p.CompilationUnit, null);
            }
            return output.Text;
        }

        private static string ConvertCSharpCodeToVb(string Code)
        {
            IParser p = ParserFactory.CreateParser(SupportedLanguage.CSharp, new StringReader(Code));
            p.Parse();

            if (p.Errors.Count > 0)
            {
                return Code;
            }
            NRefactory.PrettyPrinter.VBNetOutputVisitor output = new NRefactory.PrettyPrinter.VBNetOutputVisitor();
            List<ISpecial> specials = p.Lexer.SpecialTracker.CurrentSpecials;
            PreprocessingDirective.CSharpToVB(specials);
            p.CompilationUnit.AcceptVisitor(new NRefactory.Visitors.CSharpConstructsVisitor(), null);
            p.CompilationUnit.AcceptVisitor(new NRefactory.Visitors.ToVBNetConvertVisitor(), null);

            using (NRefactory.PrettyPrinter.SpecialNodesInserter.Install(specials, output))
            {
                output.VisitCompilationUnit(p.CompilationUnit, null);
            }
            return output.Text;
        }
    }
}
