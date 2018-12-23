using CADKitCore.Contract;
using CADKitCore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CADKitCore.Extensions;
using CADKitCore.Contract.DTO;
using CADKitCore.Views.DTO;
using CADKitDALCAD;
using ZwSoft.ZwCAD.ApplicationServices;

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
        }
        public override void OnViewLoaded()
        {
            BindScaleList();
        }

        public void OnDocumentActivate(object sender, DocumentCollectionEventArgs arg)
        {
            BindScaleList();
        }

        public void OnDimUnitSelect(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnDrawUnitSelect(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnScaleSelect(object sender, EventArgs e)
        {
            AppSettings.Instance.DrawingScale = View.SelectedScale.Scale;
        }

        void BindScaleList()
        {
            IScaleDTO currentScale = null;
            var occ = CADProxy.Database.ObjectContextManager.GetContextCollection("ACDB_ANNOTATIONSCALES");
            IList<IScaleDTO> scales = new List<IScaleDTO>();
            var aaa = AppSettings.Instance.DrawingScale;

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
                if (AppSettings.Instance.DrawingScale == item.Scale)
                {
                    var i = scales.Count;
                    currentScale = scales[i - 1];
                }
            }
            View.BindingScale(scales);
            if (currentScale != null)
            {
                View.SelectedScale = currentScale;
            }
        }

        void BindDrawingUnit()
        {

        }
    }
}
