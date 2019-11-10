using Autofac;
using CADKit.Models;
using System;
using System.Collections.Generic;
using CADKit.Utils;
using CADProxy;

#if ZwCAD
using ZwSoft.ZwCAD.Runtime;
using ZwSoft.ZwCAD.EditorInput;
#endif

#if AutoCAD
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
#endif

[assembly: CommandClass(typeof(CADKit.Commands))]

namespace CADKit
{
    public class Commands
    {
        // metody powinne byc zamienione na klasy z mozliwoscia wyboru trybu Linia komend/Okno dialogowe
        // na podstawie zmiennej systemowej (na razie nie mam pomysłu jakiej) lub na podstawie zdefiniowanej zmiennej globalnej lub AppSettings
        // na razie jest wersja prosta czyli linia komend
        // w przyszlosci do refaktoryzacji
        [CommandMethod("CKUNR")]
        public void SetDrawingStandard()
        {
            var settings = DI.Container.Resolve<AppSettings>();
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
            foreach (var item in Enum.GetValues(typeof(DrawingStandards)))
            {
                keyOptions.Keywords.Add(item.ToString().Replace('_', '-'));
            }
            keyOptions.Keywords.Default = settings.DrawingStandard.ToString().Replace('_', '-');
            PromptResult keyResult = ProxyCAD.Editor.GetKeywords(keyOptions);
            if (keyResult.Status == PromptStatus.OK)
            {
                settings.DrawingStandard = EnumsUtil.GetEnum<DrawingStandards>(keyResult.StringResult.Replace('-', '_'), settings.DrawingStandard);
            }
            ProxyCAD.Editor.WriteMessage($"\nBieżąca norma rysunkowa : {drawingStandards[settings.DrawingStandard]}\n");
        }

        //[CommandMethod("CK_UJR")]
        //public void SetDrawingUnits()
        //{
        //    var settings = DI.Container.Resolve<AppSettings>();

        //    PromptKeywordOptions keyOptions = new PromptKeywordOptions("\nJednostka rysunkowa:")
        //    {
        //        AllowNone = true
        //    };
        //    keyOptions.Keywords.Default = settings.DrawingUnit.ToString();
        //    foreach (var item in Enum.GetValues(typeof(Units)))
        //    {
        //        keyOptions.Keywords.Add(item.ToString());
        //    }
        //    PromptResult keyResult = CADProxy.Editor.GetKeywords(keyOptions);
        //    if (keyResult.Status == PromptStatus.OK)
        //    {
        //        switch (keyResult.StringResult)
        //        {
        //            case "mm":
        //                settings.DrawingUnit = Units.mm;
        //                break;
        //            case "cm":
        //                settings.DrawingUnit = Units.cm;
        //                break;
        //            case "m":
        //                settings.DrawingUnit = Units.m;
        //                break;
        //        }
        //    }
        //    CADProxy.Editor.WriteMessage($"\nBieżąca jednostka rysunkowa : {settings.DrawingUnit}\n");
        //}

        //[CommandMethod("CK_USR")]
        //public void SetDrawingScale()
        //{
        //    var settings = DI.Container.Resolve<AppSettings>();

        //    PromptStringOptions keyOptions = new PromptStringOptions($"\nSkala rysunku <{settings.DrawingScale}>:");
        //    //{
        //    //    AllowNone = true,
        //    //    AllowNegative = false,
        //    //    AllowZero = false
        //    //};
        //    PromptResult keyResult = CADProxy.Editor.GetString(keyOptions);
        //    if (keyResult.Status == PromptStatus.OK)
        //    {
        //        settings.DrawingScale = keyResult.StringResult;
        //    }
        //    CADProxy.Editor.WriteMessage($"\nBieżąca skala rysunkowa: {settings.DrawingScale}\n");
        //}

        [CommandMethod("CKPALETE")]
        public void ShowPalette()
        {
            var settings = DI.Container.Resolve<AppSettings>();
            settings.CADKitPalette.Visible = !settings.CADKitPalette.Visible;
        }
    }
}
