﻿using CADKitElevationMarks.Contracts;
using System;
using CADKit.Proxy;
using System.Globalization;
using CADKit;
using CADKit.Contracts;

#if ZwCAD
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.ApplicationServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
#endif

namespace CADKitElevationMarks.Models
{
    public class ElevationValueProvider : ValueProvider, IElevationValueProvider
    {
        public override void PrepareValue()
        {
            Application.MainWindow.Focus();
            var promptOptions = new PromptPointOptions("\nWskaż punkt wysokościowy:");
            var pointValue = CADProxy.Editor.GetPoint(promptOptions);
            if (pointValue.Status == PromptStatus.OK)
            {
                basePoint = pointValue.Value;
                elevationValue = new ElevationValue(GetElevationSign(), GetElevationValue()).Parse(new CultureInfo("pl-PL"));
            }
            else
            {
                throw new Exception();
            }
        }

        private string GetElevationValue()
        {
            return Math.Round(Math.Abs(basePoint.Y) * GetElevationFactor(), 3).ToString("0.000");
        }

        private string GetElevationSign()
        {
            if (Math.Round(Math.Abs(basePoint.Y) * GetElevationFactor(), 3) == 0)
            {
                return "%%p";
            }
            else if (basePoint.Y < 0)
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