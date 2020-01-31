using CADKitBasic.Models;
using System;
using System.Collections.Generic;
using CADKitBasic.Utils;
using CADKit;
using CADKit.Runtime;
using CADKit.Extensions;
using CADKit.Proxy;
using CADKit.Models;
using Autofac;

#if ZwCAD
using ZwSoft.ZwCAD.EditorInput;
#endif

#if AutoCAD
using Autodesk.AutoCAD.EditorInput;
#endif

namespace CADKitBasic
{
    public class Commands
    {
        //TODO: metody powinne byc zamienione na klasy z mozliwoscia wyboru trybu Linia komend/Okno dialogowe
        // na podstawie zmiennej systemowej(na razie nie mam pomysłu jakiej) lub na podstawie zdefiniowanej zmiennej globalnej lub AppSettings
        // na razie jest wersja prosta czyli linia komend
        // w przyszlosci do refaktoryzacji
        // [CommandMethod("CKUNR")]
        // public void SetDrawingStandard()
        // {
        // TODO: Lepiej gdyby slownik byl budowany na zewnatrz i gotowy dostarczony metodzie
        // w przyszlosci do refaktoryzacji

        //Dictionary<DrawingStandards, string> drawingStandards = new Dictionary<DrawingStandards, string>();
        //     foreach (var item in Enum.GetValues(typeof(DrawingStandards)))
        //     {
        //         drawingStandards.Add((DrawingStandards)item, item.ToString().Replace('_', '-'));
        //     }
        //     PromptKeywordOptions keyOptions = new PromptKeywordOptions("\nNorma rysunkowa:")
        //     {
        //         AllowNone = true
        //     };
        //     foreach (var item in Enum.GetValues(typeof(DrawingStandards)))
        //     {
        //         keyOptions.Keywords.Add(item.ToString().Replace('_', '-'));
        //     }
        //     keyOptions.Keywords.Default = AppSettings.Instance.DrawingStandard.ToString().Replace('_', '-');
        //     PromptResult keyResult = CADProxy.Editor.GetKeywords(keyOptions);
        //     if (keyResult.Status == PromptStatus.OK)
        //     {
        //         AppSettings.Instance.DrawingStandard = EnumsUtil.GetEnum<DrawingStandards>(keyResult.StringResult.Replace('-', '_'), AppSettings.Instance.DrawingStandard);
        //     }
        //     CADProxy.Editor.WriteMessage($"\nBieżąca norma rysunkowa : {drawingStandards[AppSettings.Instance.DrawingStandard]}\n");
        // }



        [CommandMethod("CK_UJR")]
        public void SetDrawingUnits()
        {
            PromptKeywordOptions keyOptions = new PromptKeywordOptions("\nJednostka rysunkowa:")
            {
                AllowNone = true
            };
            keyOptions.Keywords.Default = AppSettings.Get.DrawingUnit.ToString();
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
                        AppSettings.Get.DrawingUnit = Units.mm;
                        break;
                    case "cm":
                        AppSettings.Get.DrawingUnit = Units.cm;
                        break;
                    case "m":
                        AppSettings.Get.DrawingUnit = Units.m;
                        break;
                }
            }
            CADProxy.Editor.WriteMessage($"\nBieżąca jednostka rysunkowa : {AppSettings.Get.DrawingUnit}\n");
        }

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


    }
}
