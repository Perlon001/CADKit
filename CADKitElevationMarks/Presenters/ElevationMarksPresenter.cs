using Autofac;
using CADKit;
using CADKit.Contracts;
using CADKit.Models;
using CADKit.Services;
using CADKit.UI;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Contracts.Presenters;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Contracts.Views;
using CADKitElevationMarks.Events;
using CADKitElevationMarks.Services;
using System;
using System.Drawing;

namespace CADKitElevationMarks.Presenters
{
    public class ElevationMarksPresenter : Presenter<IElevationMarksView>, IElevationMarksPresenter
    {
        private IMarkTypeService markTypeService;
        
        public ElevationMarksPresenter(IElevationMarksView _view)
        {
            View = _view;
            View.Presenter = this;
        }

        public void ChangeStandardDrawing(DrawingStandards _standard)
        {
            using(var scope = DI.Container.BeginLifetimeScope())
            {
                var factory = scope.Resolve<MarkTypeServiceFactory>();
                markTypeService = factory.GetMarkTypeService(_standard);
            }
        }

        public void CreateMark(object sender, BeginCreateMarkEventArgs e)
        {
            var markType = markTypeService.GetMarkType(e.ID);
            var mark = Activator.CreateInstance(markType) as IElevationMark;
            mark.Create(View.SetType);
        }

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
            base.OnViewLoaded();

            View.RegisterHandlers();
        }

        public void FillButtons()
        {
            View.BindDrawingStandard(DrawingStandards.PNB01025,new MarkService(new MarkIconService(InterfaceScheme.dark)));
        }

        public void _FillButtons()
        {
            View.ClearDrawingStandars();
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
    }
}
