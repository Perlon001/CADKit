using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if ZwCAD
using ZwSoft.ZwCAD.EditorInput;
using CADEditorInput = ZwSoft.ZwCAD.EditorInput;
#endif

#if AutoCAD
using Autodesk.AutoCAD.EditorInput;
using CADEditorInput = Autodesk.AutoCAD.EditorInput;
#endif

namespace CADKit.EditorInput
{
    public abstract class PromptEditorOptions : CADEditorInput.PromptOptions
    {
        protected internal PromptEditorOptions(string message) : base(message) { }
        protected internal PromptEditorOptions(string messageAndKeywords, string globalKeywords) : base(messageAndKeywords, globalKeywords) { }
    }
}
