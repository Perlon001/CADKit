using Autofac;
using CADKit;
using CADKit.Models;
using CADKit.Presenters;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Contracts.Presenters;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Contracts.Views;
using CADKitElevationMarks.Services;
using System;

namespace CADKitElevationMarks.Presenters
{
    public class ElevationMarksPresenter : Presenter<IElevationMarksView>, IElevationMarksPresenter
    {
        IMarkTypeService markTypeService;
        
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
                this.markTypeService = factory.GetMarkTypeService(_standard);
            }
        }

        public void CreateMark(int _id)
        {
            var markType = this.markTypeService.GetMarkType(_id);
            var mark = Activator.CreateInstance(markType) as IElevationMark;
            mark.Create(View.GetEntitiesSet());
        }

        public override void OnViewLoaded()
        {
            try
            {
                base.OnViewLoaded();
                using (var scope = DI.Container.BeginLifetimeScope())
                {
                    var factory = scope.Resolve<MarkTypeServiceFactory>();
                    foreach (DrawingStandards st in Enum.GetValues(typeof(DrawingStandards)))
                    {
                        View.BindDrawingStandard(st, factory.GetMarkTypeService(st).GetMarks());
                    }
                    this.markTypeService = factory.GetMarkTypeService(View.GetDrawingStandard());
                }
                View.RegisterHandlers();
            }
            catch (Exception ex)
            {
                View.ShowException(ex, "Błąd ładowania widokou");
            }
        }
    }
}
