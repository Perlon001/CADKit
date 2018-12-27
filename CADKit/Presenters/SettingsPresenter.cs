using CADKitCore.Contract;
using CADKitCore.Contract.DTO;
using CADKitCore.Settings;
using CADKitCore.Util;
using CADKitCore.Views.DTO;
using CADKitDALCAD;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CADKitCore.Presenters
{
    public class SettingsPresenter : Presenter<ISettingsView>, ISettingsPresenter
    {
        public SettingsPresenter(ISettingsView view)
        {
            View = view;
            View.Presenter = this;
            View.RegisterHandlers();
            CADProxy.DocumentManager.DocumentActivated -= OnDocumentActivate;
            CADProxy.DocumentManager.DocumentActivated += OnDocumentActivate;
            //CADProxy.Document.CommandEnded -= OnCommandEnded;
            //CADProxy.Document.CommandEnded += OnCommandEnded;
            CADProxy.SystemVariableChanged -= OnSystemVariableChanged;
            CADProxy.SystemVariableChanged += OnSystemVariableChanged;
        }

        public override void OnViewLoaded()
        {
            BindDrawingUnit();
            BindDimensionUnit();
            BindScaleList();
        }

        public void OnDocumentActivate(object sender, ZwSoft.ZwCAD.ApplicationServices.DocumentCollectionEventArgs arg)
        {
            CADProxy.ShowAlertDialog(CADProxy.Document.Name + ":" + CADProxy.Database.Cannoscale.Name);
            BindScaleList();
        }

        private void OnCommandEnded(object sender, ZwSoft.ZwCAD.ApplicationServices.CommandEventArgs e)
        {
            // jak na przechwycenie zmiany skali to słabe jest 
            if(e.GlobalCommandName == "SETVAR")
            {
                BindScaleList();
            }
        }

        private void OnSystemVariableChanged(object sender, ZwSoft.ZwCAD.ApplicationServices.SystemVariableChangedEventArgs arg)
        {
            CADProxy.ShowAlertDialog("arg000:" + arg.Name);
            if(arg.Name == "CANNOSCALE")
            {
                IScaleDTO currentScale = scales.FirstOrDefault(a => a.Name == CADProxy.Database.Cannoscale.Name);
                if (currentScale != null)
                {
                    View.SelectedScale = currentScale;
                }
            }
        }

        public void OnDimUnitSelect(object sender, EventArgs e)
        {
            AppSettings.Instance.DimensionUnit = View.SelectedDimensionUnit;
        }

        public void OnDrawUnitSelect(object sender, EventArgs e)
        {
            AppSettings.Instance.DrawingUnit = View.SelectedDrawingUnit;
        }

        public void OnScaleSelect(object sender, EventArgs e)
        {
            var occ = CADProxy.Database.ObjectContextManager.GetContextCollection("ACDB_ANNOTATIONSCALES");
            foreach (ZwSoft.ZwCAD.DatabaseServices.AnnotationScale item in occ)
            {
                if(item.Name == View.SelectedScale.Name)
                {
                    CADProxy.Database.Cannoscale = item;
                    AppSettings.Instance.DrawingScale = View.SelectedScale.Scale;
                    break;
                }
            }
        }

        void BindScaleList()
        {
            var occ = CADProxy.Database.ObjectContextManager.GetContextCollection("ACDB_ANNOTATIONSCALES");
            IList<IScaleDTO> scales = new List<IScaleDTO>();

            foreach (ZwSoft.ZwCAD.DatabaseServices.AnnotationScale item in occ )
            {
                scales.Add(new ScaleDTO(){
                    CollectionName = item.CollectionName,
                    Name = item.Name,
                    Scale = item.Scale,
                    DrawingUnits = item.DrawingUnits,
                    PaperUnits = item.PaperUnits,
                    UniqueIdentifier = item.UniqueIdentifier
                });
            }
            View.BindingScale(scales);
            IScaleDTO currentScale = scales.FirstOrDefault(a => a.Name == CADProxy.Database.Cannoscale.Name);
            if (currentScale != null)
            {
                View.SelectedScale = currentScale;
            }
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
