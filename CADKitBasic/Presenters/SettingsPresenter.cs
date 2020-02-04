using CADKitBasic.Contracts;
using CADKitBasic.Contracts.Presenters;
using CADKitBasic.Views.DTO;
using CADKit;
using System;
using System.Collections.Generic;
using System.Linq;
using CADKit.Models;
using CADKit.Extensions;
using CADKit.UI;
using CADKit.Proxy;
using Autofac;
using CADKit.Contracts;

#if ZwCAD
using CADApplicationServices = ZwSoft.ZwCAD.ApplicationServices;
#endif

#if AutoCAD
using CADApplicationServices = Autodesk.AutoCAD.ApplicationServices;
#endif

namespace CADKitBasic.Presenters
{
    public class SettingsPresenter : Presenter<ISettingsView>, ISettingsPresenter
    {
        public SettingsPresenter(ISettingsView _view) : base()
        {
            View = _view;
            View.Presenter = this;
            CADProxy.DocumentActivated -= OnDocumentActivate;
            CADProxy.DocumentActivated += OnDocumentActivate;
            CADProxy.CommandEnded -= OnCommandEnded;
            CADProxy.CommandEnded += OnCommandEnded;
            CADProxy.SystemVariableChanged -= OnSystemVariableChanged;
            CADProxy.SystemVariableChanged += OnSystemVariableChanged;
        }

        public override void OnViewLoaded()
        {

            BindDrawingUnit();
            BindDimensionUnit();
            BindScaleList();

            base.OnViewLoaded();
            
            View.RegisterHandlers();
            View.SelectedScale = ScaleDTO.GetCurrentScale();
        }

        public void OnDrawUnitSelect(object sender, EventArgs e)
        {
            AppSettings.Get.DrawingUnit = View.SelectedDrawingUnit;
        }

        public void OnDimUnitSelect(object sender, EventArgs e)
        {
            AppSettings.Get.DimensionUnit = View.SelectedDimensionUnit;
        }

        public void OnScaleSelect(object sender, EventArgs e)
        {
            CADProxy.SetSystemVariable("CANNOSCALE", View.SelectedScale.Name);
        }

        private void OnCommandEnded(object sender, CADApplicationServices.CommandEventArgs arg)
        {
            if (arg.GlobalCommandName == "SCALELISTEDIT")
            {
                BindScaleList();
            }
        }

        private void OnDocumentActivate(object sender, CADApplicationServices.DocumentCollectionEventArgs arg)
        {
            BindScaleList();
            View.SelectedScale = new ScaleDTO()
            {
                UniqueIdentifier = CADProxy.Database.Cannoscale.UniqueIdentifier,
                Name = CADProxy.Database.Cannoscale.Name
            };
            View.SelectedDrawingUnit = EnumsUtil.GetEnum(CADProxy.GetCustomProperty("CKDrawingUnit"), Units.mm);
            View.SelectedDimensionUnit = EnumsUtil.GetEnum(CADProxy.GetCustomProperty("CKDimensionUnit"), Units.mm);
        }

        private void OnSystemVariableChanged(object sender, CADApplicationServices.SystemVariableChangedEventArgs arg)
        {
            if (arg.Name == "CANNOSCALE")
            {
                View.SelectedScale = ScaleDTO.GetCurrentScale();
            }
        }

        private void BindScaleList()
        {
            var occ = CADProxy.Database.ObjectContextManager.GetContextCollection("ACDB_ANNOTATIONSCALES");
            IList<ScaleDTO> scales = new List<ScaleDTO>();
            foreach (ZwSoft.ZwCAD.DatabaseServices.AnnotationScale item in occ)
            {
                scales.Add(new ScaleDTO()
                {
                    UniqueIdentifier = item.UniqueIdentifier,
                    Name = item.Name
                });
            }
            View.BindingScale(scales);
        }

        private void BindDrawingUnit()
        {
            View.BindingDrawingUnits(EnumsUtil.GetEnumDictionary<Units>().ToList());
        }

        private void BindDimensionUnit()
        {
            View.BindingDimensionUnits(EnumsUtil.GetEnumDictionary<Units>().ToList());
        }

        //private void OnChangeColorScheme(object sender, ChangeInterfaceSchemeEventArgs arg)
        //{
        //    using (var scope = DI.Container.BeginLifetimeScope())
        //    {
        //        IInterfaceSchemeService service = DI.Container.Resolve<IInterfaceSchemeService>();
        //        View.SetColorScheme(service);
        //    }
        //}
    }
}
