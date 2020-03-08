using Autofac;
using CADKit.Contracts;
using CADKit.Events;
using CADKit.Proxy.Windows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media;

#if ZwCAD
using CADWindows = ZwSoft.ZwCAD.Windows;
#endif
#if AutoCAD
using CADWindows = Autodesk.AutoCAD.Windows;
#endif

namespace CADKit.UI
{
    public class CADKitPaletteSet : ICollection
    {
        private PaletteSet paletteSet;
        private static IDictionary<string, dynamic> Collection = new Dictionary<string, dynamic>();

        public CADKitPaletteSet(string name)
        {
            paletteSet = new PaletteSet(name)
            {
                MinimumSize = new Size(100, 100)
            };
            AppSettings.Get.ChangeInterfaceScheme += OnChangeInterfaceScheme;
        }

        public CADKitPaletteSet(string name, Guid toolID)
        {
            paletteSet = new PaletteSet(name, toolID)
            {
                MinimumSize = new Size(100, 100)
            };
            AppSettings.Get.ChangeInterfaceScheme += OnChangeInterfaceScheme;
        }

        public CADKitPaletteSet(string name, string cmd, Guid toolID)
        {
            paletteSet = new PaletteSet(name, cmd, toolID)
            {
                MinimumSize = new Size(100, 100)
            };
            AppSettings.Get.ChangeInterfaceScheme += OnChangeInterfaceScheme;
        }

        public string Name
        {
            get { return paletteSet.Name; }
            set { paletteSet.Name = value; }
        }

        public CADWindows.DockSides Dock
        {
            get { return paletteSet.Dock; }
            set { paletteSet.Dock = value; }
        }

        public bool KeepFocus
        {
            get { return paletteSet.KeepFocus; }
            set { paletteSet.KeepFocus = value; }
        }

        public Size Size
        {
            get { return paletteSet.Size; }
            set { paletteSet.Size = value; }
        }

        public Size MinimumSize
        {
            get { return paletteSet.MinimumSize; }
            set { paletteSet.MinimumSize = value; }
        }

        public int Count { get { return paletteSet.Count; } }

        public bool PaletteState { get; protected set; }

        public bool Visible
        {
            get { return paletteSet.Visible; }
            set { paletteSet.Visible = value; }
        }

        public Control GetPage(string _name)
        {
            return Collection[_name];
        }

        public CADWindows.Palette Add(string name, Control control)
        {
            Collection.Add(name, control);
            return paletteSet.Add(name, control);
        }

        public CADWindows.Palette AddVisual(string name, Visual visual)
        {
            Collection.Add(name, visual);
            return paletteSet.AddVisual(name, visual);
        }

        public void OnChangeInterfaceScheme(object _sender, ChangeInterfaceSchemeEventArgs _arg)
        {
            using(var scope = DI.Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IInterfaceSchemeService>();
                for(int i = 0; i < Count; i++)
                {
                    var view = Collection[this.paletteSet[i].Name] as IView;
                    view.SetColorScheme(service);
                }
            }
        }

        //TODO: ICollection not implemented
        #region ICollection interface implementation
        public object SyncRoot => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
