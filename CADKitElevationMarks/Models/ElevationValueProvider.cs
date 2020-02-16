using System;
using CADKit.Proxy;
using System.Globalization;
using CADKit;
using CADKit.Contracts;

#if ZwCAD
using ZwSoft.ZwCAD.EditorInput;
#endif

#if AutoCAD
using Autodesk.AutoCAD.EditorInput;
#endif

namespace CADKitElevationMarks.Models
{
    public class ElevationValueProvider : ValueProvider
    {
        public override void Init()
        {
            CADProxy.MainWindow.Focus();
            var promptOptions = new PromptPointOptions("\nWskaż punkt wysokościowy:");
            var pointValue = CADProxy.Editor.GetPoint(promptOptions);
            switch (pointValue.Status)
            {
                case PromptStatus.OK:
                    BasePoint = pointValue.Value;
                    ElevationValue = new ElevationValue(GetElevationSign(), GetElevationValue()).Parse(new CultureInfo("pl-PL"));
                    break;
                case PromptStatus.Cancel:
                    throw new OperationCanceledException();
                default:
                    throw new Exception("Nie rozpoznany PromptStatus");
            }
        }

        private string GetElevationValue()
        {
            return Math.Round(Math.Abs(BasePoint.Y) * GetElevationFactor(), 3).ToString("0.000");
        }

        private string GetElevationSign()
        {
            if (Math.Round(Math.Abs(BasePoint.Y) * GetElevationFactor(), 3) == 0)
            {
                return "%%p";
            }
            else if (BasePoint.Y < 0)
            {
                return "-";
            }
            else
            {
                return "+";
            }
        }

        private double GetElevationFactor()
        {
            switch (AppSettings.Get.DrawingUnit)
            {
                case Units.m:
                    return 1;
                case Units.cm:
                    return 0.01;
                case Units.mm:
                    return 0.001;
                default:
                    throw new Exception("\nNie rozpoznana jednostka rysunkowa");
            }
        }

    }
}
