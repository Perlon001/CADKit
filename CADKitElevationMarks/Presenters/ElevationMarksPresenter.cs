using Autofac;
using CADKit;
using CADKit.Contracts;
using CADKit.Events;
using CADKit.Models;
using CADKit.Services;
using CADKit.UI;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Contracts.Presenters;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Contracts.Views;
using CADKitElevationMarks.Events;
using CADKitElevationMarks.Models;
using CADKitElevationMarks.Services;
using System;
using System.Drawing;

#if ZwCAD
using CADApplicationServices = ZwSoft.ZwCAD.ApplicationServices;
#endif

#if AutoCAD
using CADApplicationServices = Autodesk.AutoCAD.ApplicationServices;
#endif

namespace CADKitElevationMarks.Presenters
{
    public class ElevationMarksPresenter : Presenter<IElevationMarksView>, IElevationMarksPresenter
    {
        private IMarkTypeService markTypeService;
        
        public ElevationMarksPresenter(IElevationMarksView _view)
        {
            View = _view;
            View.Presenter = this;
            //AppSettings.Get.ChangeInterfaceScheme -= OnChangeColorScheme;
            //AppSettings.Get.ChangeInterfaceScheme += OnChangeColorScheme;
        }

        public void ChangeStandardDrawing(DrawingStandards _standard)
        {
            using (var factory = new MarkTypeServiceFactory())
            {
                markTypeService = factory.GetMarkTypeService(_standard);
            }
        }

        public void CreateMark(object sender, BeginCreateMarkEventArgs e)
        {
            var markType = markTypeService.GetMarkType(e.ID);
            var mark = Activator.CreateInstance(markType) as IElevationMark;
            mark.Create(View.GetSetSelection());
        }

        // TODO: GetOptionIcon() move to another service
        public Bitmap GetOptionIcon()
        {
            switch (InterfaceSchemeService.ColorScheme)
            {
                case InterfaceScheme.dark:
                    return Properties.Resources.options_dark;
                default:
                    return Properties.Resources.options;
            }
        }

        public override void OnViewLoaded()
        {
            View.ClearDrawingStandars();
            //View.SetColorScheme(GetColorSchemeService());
            FillTabs();
            View.RegisterHandlers();
        }

        public void FillTabs()
        {
            using (var scope = DI.Container.BeginLifetimeScope())
            {
                var factory = scope.Resolve<MarkTypeServiceFactory>();
                foreach (DrawingStandards st in Enum.GetValues(typeof(DrawingStandards)))
                {
                    View.BindDrawingStandard(st, factory.GetMarkTypeService(st).GetMarks(), scope.Resolve<IInterfaceSchemeService>());
                }
                markTypeService = factory.GetMarkTypeService(View.GetDrawingStandard());
            }
        }

        //private void OnChangeColorScheme(object sender, ChangeInterfaceSchemeEventArgs arg)
        //{
        //    View.ClearDrawingStandars();
        //    View.SetColorScheme(GetColorSchemeService());
        //    FillTabs();
        //}

        //private IInterfaceSchemeService GetColorSchemeService()
        //{
        //    using (var scope = DI.Container.BeginLifetimeScope())
        //    {
        //        IInterfaceSchemeService service = DI.Container.Resolve<IInterfaceSchemeService>();
        //        return service;
        //    }
        //}
    }
}
