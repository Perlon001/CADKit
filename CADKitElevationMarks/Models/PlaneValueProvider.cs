﻿using System;
using CADKit.Proxy;
using CADKitElevationMarks.Contracts;

#if ZwCAD
using ZwSoft.ZwCAD.Geometry;
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
    public class PlaneValueProvider : ValueProvider, IPlaneValueProvider
    {
        public override void PrepareValue()
        {
            Application.MainWindow.Focus();
            var promptOptions = new PromptStringOptions("\nRzędna wysokościowa obszaru:");
            var textValue = CADProxy.Editor.GetString(promptOptions);
            if (textValue.Status == PromptStatus.OK)
            {
                elevationValue = new ElevationValue(textValue.StringResult).Parse();
                basePoint = new Point3d(0, 0, 0);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
