using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.EditorInput;
using CADEditorInput = ZwSoft.ZwCAD.EditorInput;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using CADEditorInput = Autodesk.AutoCAD.EditorInput;
#endif

namespace CADKit.EditorInput
{
    public sealed class PromptKeywordOptions 
    {
        private readonly CADEditorInput.PromptKeywordOptions proxy;

        public PromptKeywordOptions(string message)
        {
            proxy = new CADEditorInput.PromptKeywordOptions(message);
        }
        public PromptKeywordOptions(string messageAndKeywords, string globalKeywords)
        {
            proxy = new CADEditorInput.PromptKeywordOptions(messageAndKeywords, globalKeywords);

        }

        public bool AllowNone { get { return proxy.AllowNone; } set { proxy.AllowNone = value; } }
        public bool AllowArbitraryInput { get { return proxy.AllowArbitraryInput; } set { proxy.AllowArbitraryInput = value; } }
        public string Message { get { return proxy.Message; } set { proxy.Message = value; } }
        public KeywordCollection Keywords { get { return proxy.Keywords; } }
        public bool IsReadOnly { get { return proxy.IsReadOnly; } }
        public bool AppendKeywordsToMessage { get { return proxy.AppendKeywordsToMessage; } set { proxy.AppendKeywordsToMessage = value; } }

        public void SetMessageAndKeywords(string messageAndKeywords, string globalKeywords)
        {
            proxy.SetMessageAndKeywords(messageAndKeywords, globalKeywords);
        }


    }
}
