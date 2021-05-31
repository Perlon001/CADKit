using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CADKit.Proxy;
using CADKit.Runtime;

#if ZwCAD
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.Colors;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Windows;
#endif

#if AutoCAD
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Windows;
#endif

namespace LayerColor
{
    public class GetRGBValuesClass
    {
        [CommandMethod("CDL")]
        public void ShowColorDialog()
        {
            ColorDialog cd = new ColorDialog();
            System.Windows.Forms.DialogResult dr = cd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                CADProxy.Editor.WriteMessage("\nColor selected: " + cd.Color.ToString());
            }

        }
        [CommandMethod("GetRGB")]
        public void GetRGB()
        {
            var dBase = CADProxy.Database; //Application.DocumentManager.MdiActiveDocument.Database;
            var tm = dBase.TransactionManager;
            var ed = CADProxy.Editor;
            using (var myT = tm.StartTransaction())
            {
                try
                {
                    var lt = (LayerTable)tm.GetObject(dBase.LayerTableId, OpenMode.ForRead);
                    //Iterate all records in table
                    foreach (ObjectId ltrId in lt)
                    {
                        LayerTableRecord ltr = (LayerTableRecord)tm.GetObject(ltrId, OpenMode.ForRead);
                        Color colour = ltr.Color;
                        ed.WriteMessage("\nThe name of the layer is: " + ltr.Name.ToString());
                        ed.WriteMessage("\nRed: " + colour.ColorValue.R.ToString());
                        ed.WriteMessage("\nGreen: " + colour.ColorValue.G.ToString());
                        ed.WriteMessage("\nBlue: " + colour.ColorValue.B.ToString());
                        ed.WriteMessage("\nColor Index value of the layer is:" + colour.ColorIndex.ToString() + "\n");
                    }
                    myT.Commit();
                }
                catch (CADKit.Proxy.Runtime.Exception e1)
                {
                    //Handle errors
                }
            }
        }
    }
}
