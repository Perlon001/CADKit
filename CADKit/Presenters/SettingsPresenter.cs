using CADKit.ServiceCAD;
using CADKit.Contract;
using CADKit.Contract.DTO;
using CADKit.Model;
using CADKit.Settings;
using CADKit.Util;
using CADKit.Views.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using ZwSoft.ZwCAD.ApplicationServices;

namespace CADKit.Presenters
{
    public class SettingsPresenter : Presenter<ISettingsView>, ISettingsPresenter
    {
        public SettingsPresenter(ISettingsView view)
        {
            View = view;
            View.Presenter = this;
            View.RegisterHandlers();
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
                if (item.Name == View.SelectedScale.Name)
                {
                    CADProxy.Database.Cannoscale = item;
                    AppSettings.Instance.DrawingScale = item.Scale;
                    break;
                }
            }
        }

        private void OnCommandEnded(object sender, ZwSoft.ZwCAD.ApplicationServices.CommandEventArgs arg)
        {
            if(arg.GlobalCommandName == "SCALELISTEDIT")
            {
                BindScaleList();
            }
        }

        private void OnDocumentActivate(object sender, ZwSoft.ZwCAD.ApplicationServices.DocumentCollectionEventArgs arg)
        {
            BindScaleList();
        }

        private void OnSystemVariableChanged(object sender, ZwSoft.ZwCAD.ApplicationServices.SystemVariableChangedEventArgs arg)
        {
            if (arg.Name == "CANNOSCALE")
            {
                View.SelectedScale = new ScaleDTO() { UniqueIdentifier = CADProxy.Database.Cannoscale.UniqueIdentifier };
            }
        }

        void BindScaleList()
        {
            var occ = CADProxy.Database.ObjectContextManager.GetContextCollection("ACDB_ANNOTATIONSCALES");
            IList<IScaleDTO> scales = new List<IScaleDTO>();
            IScaleDTO currentScale = null;
            foreach (ZwSoft.ZwCAD.DatabaseServices.AnnotationScale item in occ )
            {
                scales.Add(new ScaleDTO(){
                    UniqueIdentifier = item.UniqueIdentifier,
                    Name = item.Name
                });
                if( item.Name == CADProxy.Database.Cannoscale.Name)
                {
                    currentScale = new ScaleDTO() {UniqueIdentifier = item.UniqueIdentifier };
                }
            }
            View.BindingScale(scales);
            if( !currentScale.Equals(null))
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
