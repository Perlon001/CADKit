using System;
using System.Reflection;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Runtime;
using CADKitElevationMarks.Model;
using CADKitCore.Contract;
using CADKitCore.Util;
using Autofac;
using CADKitCore.Model;
using CADKitCore.Settings;

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
            //using (var scope = DI.Container.BeginLifetimeScope())
            //{
            //    TextStyleCreator bbb = new TextStyleCreator();
            //    ITextStyleCreator ccc = scope.Resolve<ITextStyleCreator>();
            //    bbb.Create(TextStyles.elevmark);
            //    ccc.Create(TextStyles.elevmark);
                

            //    ITextStyleTableService aaa = scope.Resolve<ITextStyleTableService>();
            //    aaa.GetSymbolRecord(TextStyles.elevmark);
            //    aaa.CreateSymbolRecord(new TextStyleTableRecord());
            //    aaa.CreateSymbolRecord(bbb.Create(TextStyles.elevmark));
            //    aaa.CreateSymbolRecord(ccc.Create(TextStyles.elevmark));
            //}
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
