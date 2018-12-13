using System;
using System.Reflection;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Runtime;
using CADKitElevationMarks.Model;

namespace CADKitElevationMarks
{
    public class Autostart : IExtensionApplication
    {
        public void Initialize()
        {
            // Assembly.LoadFrom("D:\\dev\\CSharp\\Workspace\\CADKit\\CADKitCore\\bin\\Debug\\CADKitCore.dll");

            Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\nStart CADKitElevationMark");
            var start = new CADKitCore.Autostart();
            start.Initialize();
            // Commands.AA();
        }

        public void Terminate()
        {
        }

        public static ObjectId Generate()
        {
            TextStyleTableRecord style = new TextStyleTableRecord();
            style.Name = "ck_koty";
            style.FileName = "simplex.shx";
            style.XScale = 0.65;
            style.ObliquingAngle = 0;

            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acDatabase = acDoc.Database;
            ObjectId result;
            using (Transaction transaction = acDoc.TransactionManager.StartTransaction())
            {
                TextStyleTable symbolTable = (TextStyleTable)transaction.GetObject(GetObjectId(acDatabase, typeof(TextStyleTable)), mode: OpenMode.ForRead);

                if (symbolTable.Has(style.Name))
                {
                    result = symbolTable[style.Name];
                }
                else
                {
                    symbolTable.UpgradeOpen();
                    result = symbolTable.Add(style);
                    transaction.AddNewlyCreatedDBObject(style, true);
                    transaction.Commit();
                }
            }
            return result;
        }

        private static ObjectId GetObjectId(Database db, Type type)
        {
            switch (type.Name)
            {
                case "LayerTable":
                    return db.LayerTableId;
                case "TextStyleTable":
                    return db.TextStyleTableId;
                default:
                    throw new NotSupportedException();
            }
        }

    }
}
