using Autofac;
using CADKit.Contracts;
using CADKit.Contracts.DTO;
using CADKit.Contracts.Presenters;
using CADKit.Contracts.Services;
using CADKit.Models;
using CADKit.Utils;
using CADKit.Views.DTO;
using CADProxy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CADKit.Presenters
{
    public class SettingsPresenter : Presenter<ISettingsView>, ISettingsPresenter
    {
        //private readonly IList<IComponent> leafTree;

        private readonly ICompositeService compositeService;

        public SettingsPresenter(ISettingsView view, ICompositeService compositeService)
        {
            View = view;
            View.Presenter = this;
            View.RegisterHandlers();
            ProxyCAD.DocumentActivated -= OnDocumentActivate;
            ProxyCAD.DocumentActivated += OnDocumentActivate;
            ProxyCAD.CommandEnded -= OnCommandEnded;
            ProxyCAD.CommandEnded += OnCommandEnded;
            ProxyCAD.SystemVariableChanged -= OnSystemVariableChanged;
            ProxyCAD.SystemVariableChanged += OnSystemVariableChanged;
            this.compositeService = compositeService; // DI.Container.Resolve<ICompositeService>();
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
            ProxyCAD.SetSystemVariable("CANNOSCALE", View.SelectedScale.Name);
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
            View.SelectedScale = new ScaleDTO()
            {
                UniqueIdentifier = ProxyCAD.Database.Cannoscale.UniqueIdentifier,
                Name = ProxyCAD.Database.Cannoscale.Name
            };
            View.SelectedDrawingUnit = EnumsUtil.GetEnum(ProxyCAD.GetCustomProperty("CKDrawingUnit"), Units.mm);
            View.SelectedDimensionUnit = EnumsUtil.GetEnum(ProxyCAD.GetCustomProperty("CKDimensionUnit"), Units.mm);
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
            var occ = ProxyCAD.Database.ObjectContextManager.GetContextCollection("ACDB_ANNOTATIONSCALES");
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
            View.BindingComposites(compositeService.GetComposites());
        }
    }
}
