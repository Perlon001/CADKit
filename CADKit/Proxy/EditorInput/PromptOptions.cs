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
    public abstract class PromptOptions : CADEditorInput.PromptOptions
    {

        protected internal PromptOptions(string message) : base(message) { }
        protected internal PromptOptions(string messageAndKeywords, string globalKeywords) : base(messageAndKeywords, globalKeywords) { }

        public new string Message { get { return base.Message; } set { base.Message = value; } }
        public new KeywordCollection Keywords { get { return base.Keywords; } }
        public new bool IsReadOnly { get { return base.IsReadOnly; } }
        public new bool AppendKeywordsToMessage { get { return base.AppendKeywordsToMessage; } set { base.AppendKeywordsToMessage = value; } }

        public new void SetMessageAndKeywords(string messageAndKeywords, string globalKeywords)
        {
            base.SetMessageAndKeywords(messageAndKeywords, globalKeywords);
        }

        protected override CADEditorInput.PromptResult DoIt()
        {
            return base.DoIt();
        }
        internal protected new string FormatPrompt()
        {
            return base.FormatPrompt();
        }
        protected override string GetDefaultValueString()
        {
            return base.GetDefaultValueString();
        }
    }
}
