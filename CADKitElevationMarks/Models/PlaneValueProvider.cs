using System;
using CADKit.Proxy;

#if ZwCAD
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.EditorInput;
#endif

#if AutoCAD
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
#endif


namespace CADKitElevationMarks.Models
{
    public class PlaneValueProvider : ValueProvider
    {
        public override void Init()
        {
            CADProxy.MainWindow.Focus();
            var promptOptions = new PromptStringOptions("\nRzędna wysokościowa obszaru:");
            var textValue = CADProxy.Editor.GetString(promptOptions);
            switch (textValue.Status)
            {
                case PromptStatus.OK:
                    ElevationValue = new ElevationValue("", textValue.StringResult).Parse();
                    BasePoint = new Point3d(0, 0, 0);
                    break;
                case PromptStatus.Cancel:
                    throw new OperationCanceledException();
                default:
                    throw new Exception("Nie rozpoznany PromptStatus");
            }
        }
    }
}
