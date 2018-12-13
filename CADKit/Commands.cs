using CADKitCore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Runtime;

namespace CADKitCore
{
    public class Commands
    {
        private readonly AppSettings settings = AppSettings.Instance;

        // metody powinne byc zamienione na klasy z mozliwoscia wyboru trybu Linia komend/Okno dialogowe
        // na podstawie zmiennej systemowej (na razie nie mam pomysłu jakiej) lub na podstawie zdefiniowanej zmiennej globalnej lub AppSettings
        // na razie jest wersja prosta czyli linia komend
        // w przyszlosci do refaktoryzacji
        [CommandMethod("CK_UNR")]
        public void SetDrawingStandard()
        {
            // Lepiej gdyby slownik byl budowany na zewnatrz i gotowy dostarczony metodzie
            // w przyszlosci do refaktoryzacji
            Dictionary<DrawingStandards, string> drawingStandards = new Dictionary<DrawingStandards, string>();
            foreach (var item in Enum.GetValues(typeof(DrawingStandards)))
            {
                drawingStandards.Add((DrawingStandards)item, item.ToString().Replace('_', '-'));
            }
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            PromptKeywordOptions keyOptions = new PromptKeywordOptions("\nNorma rysunkowa:");
            foreach (var item in Enum.GetValues(typeof(DrawingStandards)))
            {
                keyOptions.Keywords.Add(item.ToString().Replace('_', '-'));
            }
            keyOptions.Keywords.Default = settings.DrawingStandard.ToString().Replace('_', '-');
            keyOptions.AllowNone = true;
            PromptResult keyResult = acDoc.Editor.GetKeywords(keyOptions);
            if (keyResult.Status == PromptStatus.OK)
            {
                switch (keyResult.StringResult)
                {
                    case "PN-B-01025":
                        settings.DrawingStandard = DrawingStandards.PN_B_01025;
                        break;
                    case "BN-PARIKON":
                        settings.DrawingStandard = DrawingStandards.CADKIT;
                        break;
                }
            }
            acDoc.Editor.WriteMessage($"\nBieżąca norma rysunkowa : {drawingStandards[settings.DrawingStandard]}\n");
        }

        [CommandMethod("CK_UJR")]
        public void SetDrawingUnits()
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            PromptKeywordOptions keyOptions = new PromptKeywordOptions("\nJednostka rysunkowa:");
            foreach (var item in Enum.GetValues(typeof(DrawingUnits)))
            {
                keyOptions.Keywords.Add(item.ToString());
            }
            keyOptions.Keywords.Default = settings.DrawingUnit.ToString();
            keyOptions.AllowNone = true;
            PromptResult keyResult = acDoc.Editor.GetKeywords(keyOptions);
            if (keyResult.Status == PromptStatus.OK)
            {
                switch (keyResult.StringResult)
                {
                    case "mm":
                        settings.DrawingUnit = DrawingUnits.mm;
                        break;
                    case "cm":
                        settings.DrawingUnit = DrawingUnits.cm;
                        break;
                    case "m":
                        settings.DrawingUnit = DrawingUnits.m;
                        break;
                }
            }
            acDoc.Editor.WriteMessage($"\nBieżąca jednostka rysunkowa : {settings.DrawingUnit}\n");
        }

        [CommandMethod("CK_USR")]
        public void SetDrawingScale()
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            PromptIntegerOptions keyOptions = new PromptIntegerOptions($"\nSkala rysunku 1:<{((int)(1 / settings.DrawingScale)).ToString()}>");
            keyOptions.AllowNone = true;
            keyOptions.AllowNegative = false;
            keyOptions.AllowZero = false;
            PromptIntegerResult keyResult = acDoc.Editor.GetInteger(keyOptions);
            if (keyResult.Status == PromptStatus.OK)
            {
                settings.DrawingScale = 1 / (double)keyResult.Value;
            }
            acDoc.Editor.WriteMessage($"\nBieżąca skala rysunkowa 1:{((int)(1 / settings.DrawingScale)).ToString()}\n");
        }
    }
}
