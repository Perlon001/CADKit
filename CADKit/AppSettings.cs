using Autofac;
using CADKit.Contracts;
using CADKit.Events;
using CADKit.Extensions;
using CADKit.Models;
using CADKit.Proxy;
using CADKit.Services;
using CADKit.UI;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;

#if ZwCAD
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.Windows;
#endif

#if AutoCAD
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Windows;
#endif

namespace CADKit
{
    public sealed class AppSettings
    {
        private static readonly AppSettings instance = new AppSettings();

        private CADKitPaletteSet palette;
        private readonly EntityPropertytService propertyService;
        private Units drawingUnit;
        private Units dimensionUnit;
        private string drawingScale;


        static AppSettings() { }

        private AppSettings() 
        {
            AppPath = Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location));

            propertyService = new EntityPropertytService();

            CADProxy.DocumentCreated -= OnDocumentCreated;
            CADProxy.DocumentCreated += OnDocumentCreated;
            CADProxy.DocumentDestroyed -= OnDocumentDestroyed;
            CADProxy.DocumentDestroyed += OnDocumentDestroyed;
            CADProxy.SystemVariableChanged -= OnSystemVariableChanged;
            CADProxy.SystemVariableChanged += OnSystemVariableChanged;

            GetSettingsFromDocument();
            SetSettingsToDocument();
        }

        public event EventHandler<ChangeInterfaceSchemeEventArgs> ChangeInterfaceScheme;

        public static AppSettings Get { get { return instance; } }

        public string AppPath { get; private set; }

        public CADKitPaletteSet CADKitPalette 
        {
            get
            {
                if (palette == null)
                {
                    palette = new CADKitPaletteSet("CADKit", new Guid("53607c72-90e4-4bf8-b83d-c3da5a19c845"))
                    {
                        // Visible must set to true before Dock setting!
                        Visible = true,
                        Dock = DockSides.Left,
                        KeepFocus = true,
                    };
                    
                }
                return palette;
            }
        }

        public Units DrawingUnit
        {
            get
            {
                return drawingUnit;
            }
            set
            {
                drawingUnit = value;
                CADProxy.SetCustomProperty("CKDrawingUnit", drawingUnit.ToString());
            }
        }

        public Units DimensionUnit
        {
            get
            {
                return dimensionUnit;
            }
            set
            {
                dimensionUnit = value;
                CADProxy.SetCustomProperty("CKDimensionUnit", dimensionUnit.ToString());
            }
        }

        public string DrawingScale
        {
            get
            {
                return drawingScale; 
            }
            set
            {
                drawingScale = value;
                CADProxy.SetCustomProperty("CKDrawingScale", drawingScale);
            }
        }

        public double ScaleFactor
        {
            get
            {
                double scale = CADProxy.Database.Cannoscale.Scale;
                switch (DrawingUnit)
                {
                    case Units.cm:
                        return 10 / scale;
                    case Units.m:
                        return 1000 / scale;
                    case Units.mm:
                        return 1 / scale;
                    default:
                        throw new Exception("Nie rozpoznana jednostka rysunkowa");
                }
            }
        }

        public EntityPropertytService PropertyService { get { return propertyService; } }

        private void SetSettingsToDocument()
        {
            CADProxy.SetCustomProperty("CKDrawingUnit", drawingUnit.ToString());
            CADProxy.SetCustomProperty("CKDimensionUnit", dimensionUnit.ToString());
            CADProxy.SetCustomProperty("CKDrawingScale", drawingScale);
        }

        private void GetSettingsFromDocument()
        {
            drawingUnit = EnumsUtil.GetEnum(CADProxy.GetCustomProperty("CKDrawingUnit"), Units.mm);
            dimensionUnit = EnumsUtil.GetEnum(CADProxy.GetCustomProperty("CKDimensionUnit"), Units.mm);
            drawingScale = CADProxy.GetCustomProperty("CKDrawingScale");
            if (drawingScale == "")
            {
                DrawingScale = CADProxy.Database.Cannoscale.Name;
            }
        }

        private void OnResize(object sender, EventArgs e)
        {
            CADKitPalette.Name = "CADKit " + CADKitPalette.Size.Width;
        }

        private void OnDocumentCreated(object sender, DocumentCollectionEventArgs e)
        {
            GetSettingsFromDocument();
            SetSettingsToDocument();
            if (CADProxy.DocumentManager.Count == 1 && Get.CADKitPalette.PaletteState)
            {
                Get.CADKitPalette.Visible = true;
            }
        }

        private void OnDocumentDestroyed(object sender, DocumentDestroyedEventArgs e)
        {
            if (CADProxy.Document == null)
            {
                CADKitPalette.Visible = false;
            }
            drawingScale = "";
        }

        private void OnSystemVariableChanged(object _sender, SystemVariableChangedEventArgs _arg)
        {
            if (_arg.Name == "COLORSCHEME")
            {
                using(var scope = DI.Container.BeginLifetimeScope())
                {
                    var service = DI.Container.Resolve<IInterfaceSchemeService>();
                    ChangeInterfaceScheme?.Invoke(_sender, new ChangeInterfaceSchemeEventArgs(service));
                }
            }
        }
    }
}
