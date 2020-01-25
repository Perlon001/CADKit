using CADKitBasic.Contracts;
using CADKitBasic.Contracts.Presenters;
using CADKitBasic.Contracts.Services;
using CADKitBasic.Views.DTO;
using CADKit;
using System;
using System.Collections.Generic;
using System.Linq;
using CADKit.Models;
using CADKit.Extensions;
using CADKit.UI;
using CADKit.Proxy;

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
        private readonly ICompositeService compositeService;

        public SettingsPresenter(ISettingsView _view, ICompositeService _compositeService)
        {
            View = _view;
            View.Presenter = this;
            View.RegisterHandlers();
            CADProxy.DocumentActivated -= OnDocumentActivate;
            CADProxy.DocumentActivated += OnDocumentActivate;
            CADProxy.CommandEnded -= OnCommandEnded;
            CADProxy.CommandEnded += OnCommandEnded;
            CADProxy.SystemVariableChanged -= OnSystemVariableChanged;
            CADProxy.SystemVariableChanged += OnSystemVariableChanged;
            compositeService = _compositeService; // DI.Container.Resolve<ICompositeService>();
        }

        public override void OnViewLoaded()
        {
            BindDrawingUnit();
            BindDimensionUnit();
            BindScaleList();
            View.SelectedScale = ScaleDTO.GetCurrentScale();
        }

        public void OnDrawUnitSelect(object sender, EventArgs e)
        {
            AppSettings.Instance.DrawingUnit = View.SelectedDrawingUnit;
        }

        public void OnDimUnitSelect(object sender, EventArgs e)
        {
            AppSettings.Instance.DimensionUnit = View.SelectedDimensionUnit;
        }

        public void OnScaleSelect(object sender, EventArgs e)
        {
            CADProxy.SetSystemVariable("CANNOSCALE", View.SelectedScale.Name);
        }

        void OnCommandEnded(object sender, CADApplicationServices.CommandEventArgs arg)
        {
            if(arg.GlobalCommandName == "SCALELISTEDIT")
            {
                BindScaleList();
            }
        }

        void OnDocumentActivate(object sender, CADApplicationServices.DocumentCollectionEventArgs arg)
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

        void OnSystemVariableChanged(object sender, CADApplicationServices.SystemVariableChangedEventArgs arg)
        {
            if (arg.Name == "CANNOSCALE")
            {
                View.SelectedScale = ScaleDTO.GetCurrentScale();
            }
        }

        void BindScaleList()
        {
            var occ = CADProxy.Database.ObjectContextManager.GetContextCollection("ACDB_ANNOTATIONSCALES");
            IList<ScaleDTO> scales = new List<ScaleDTO>();
            foreach (ZwSoft.ZwCAD.DatabaseServices.AnnotationScale item in occ )
            {
                scales.Add(new ScaleDTO(){
                    UniqueIdentifier = item.UniqueIdentifier,
                    Name = item.Name
                });
            }
            View.BindingScale(scales);
        }

        void BindDrawingUnit()
        {
            View.BindingDrawingUnits(EnumsUtil.GetEnumDictionary<Units>().ToList());
        }

        void BindDimensionUnit()
        {
            View.BindingDimensionUnits(EnumsUtil.GetEnumDictionary<Units>().ToList());
        }

    }
}
