using CADKit.ServiceCAD;
using CADKit.Contract;
using CADKit.Contract.DTO;
using CADKit.Model;
using CADKit.Util;
using CADKit.Views.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using CADKit.DIContainer;
using Autofac;
using ZwSoft.ZwCAD.ApplicationServices;
using CADKit.Services;

namespace CADKit.Presenters
{
    public class SettingsPresenter : Presenter<ISettingsView>, ISettingsPresenter
    {
        private IList<IComponent> leafTree;

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
            BindLeadTree();
            View.SelectedScale = ScaleDTO.GetCurrentScale();
        }

        public void OnDrawUnitSelect(object sender, EventArgs e)
        {
            DI.Container.Resolve<AppSettings>().DrawingUnit = View.SelectedDrawingUnit;
        }

        public void OnDimUnitSelect(object sender, EventArgs e)
        {
            DI.Container.Resolve<AppSettings>().DimensionUnit = View.SelectedDimensionUnit;
        }

        public void OnScaleSelect(object sender, EventArgs e)
        {
            CADProxy.SetSystemVariable("CANNOSCALE", View.SelectedScale.Name);
        }

        void OnCommandEnded(object sender, ZwSoft.ZwCAD.ApplicationServices.CommandEventArgs arg)
        {
            if(arg.GlobalCommandName == "SCALELISTEDIT")
            {
                BindScaleList();
            }
        }

        void OnDocumentActivate(object sender, ZwSoft.ZwCAD.ApplicationServices.DocumentCollectionEventArgs arg)
        {
            BindScaleList();
            var a = CADProxy.GetCustomProperty("CKDrawingScale");
            View.SelectedScale = new ScaleDTO()
            {
                UniqueIdentifier = CADProxy.Database.Cannoscale.UniqueIdentifier,
                Name = CADProxy.Database.Cannoscale.Name
            };
            View.SelectedDrawingUnit = EnumsUtil.GetEnum(CADProxy.GetCustomProperty("CKDrawingUnit"), Units.mm);
            View.SelectedDimensionUnit = EnumsUtil.GetEnum(CADProxy.GetCustomProperty("CKDimensionUnit"), Units.mm);
        }

        void OnSystemVariableChanged(object sender, ZwSoft.ZwCAD.ApplicationServices.SystemVariableChangedEventArgs arg)
        {
            if (arg.Name == "CANNOSCALE")
            {
                View.SelectedScale = ScaleDTO.GetCurrentScale();
            }
        }

        void BindScaleList()
        {
            var occ = CADProxy.Database.ObjectContextManager.GetContextCollection("ACDB_ANNOTATIONSCALES");
            IList<IScaleDTO> scales = new List<IScaleDTO>();
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

        void BindLeadTree()
        {
            var cc = new InternalCompositContainer(new LocalCompositService());
        }
    }
}
