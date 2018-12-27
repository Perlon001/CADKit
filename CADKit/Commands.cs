using CADKit.ServiceCAD;
using CADKit.Model;
using CADKit.Settings;
using CADKit.Util;
using System;
using System.Collections.Generic;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Runtime;

[assembly: CommandClass(typeof(CADKit.Commands))]

namespace CADKit
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
            PromptKeywordOptions keyOptions = new PromptKeywordOptions("\nNorma rysunkowa:")
            {
                AllowNone = true
            };
            keyOptions.Keywords.Default = settings.DrawingStandard.ToString().Replace('_', '-');
            foreach (var item in Enum.GetValues(typeof(DrawingStandards)))
            {
                keyOptions.Keywords.Add(item.ToString().Replace('_', '-'));
            }
            PromptResult keyResult = CADProxy.Editor.GetKeywords(keyOptions);
            if (keyResult.Status == PromptStatus.OK)
            {
                switch (keyResult.StringResult)
                {
                    case "PN-B-01025":
                        settings.DrawingStandard = DrawingStandards.PN_B_01025;
                        break;
                    case "CADKIT":
                        settings.DrawingStandard = DrawingStandards.CADKIT;
                        break;
                }
            }
            CADProxy.Editor.WriteMessage($"\nBieżąca norma rysunkowa : {drawingStandards[settings.DrawingStandard]}\n");
        }

        [CommandMethod("CK_UJR")]
        public void SetDrawingUnits()
        {
            PromptKeywordOptions keyOptions = new PromptKeywordOptions("\nJednostka rysunkowa:")
            {
                AllowNone = true
            };
            keyOptions.Keywords.Default = settings.DrawingUnit.ToString();
            foreach (var item in Enum.GetValues(typeof(Units)))
            {
                keyOptions.Keywords.Add(item.ToString());
            }
            PromptResult keyResult = CADProxy.Editor.GetKeywords(keyOptions);
            if (keyResult.Status == PromptStatus.OK)
            {
                switch (keyResult.StringResult)
                {
                    case "mm":
                        settings.DrawingUnit = Units.mm;
                        break;
                    case "cm":
                        settings.DrawingUnit = Units.cm;
                        break;
                    case "m":
                        settings.DrawingUnit = Units.m;
                        break;
                }
            }
            CADProxy.Editor.WriteMessage($"\nBieżąca jednostka rysunkowa : {settings.DrawingUnit}\n");
        }

        [CommandMethod("CK_USR")]
        public void SetDrawingScale()
        {
            PromptIntegerOptions keyOptions = new PromptIntegerOptions($"\nSkala rysunku 1:<{((int)(1 / settings.DrawingScale)).ToString()}>")
            {
                AllowNone = true,
                AllowNegative = false,
                AllowZero = false
            };
            PromptIntegerResult keyResult = CADProxy.Editor.GetInteger(keyOptions);
            if (keyResult.Status == PromptStatus.OK)
            {
                settings.DrawingScale = 1 / (double)keyResult.Value;
            }
            CADProxy.Editor.WriteMessage($"\nBieżąca skala rysunkowa 1:{((int)(1 / settings.DrawingScale)).ToString()}\n");
        }

        [CommandMethod("CK_PALETE")]
        public void ShowPalette()
        {
            AppSettings.Instance.CADKitPalette.Visible = !AppSettings.Instance.CADKitPalette.Visible;
        }
    }
}
