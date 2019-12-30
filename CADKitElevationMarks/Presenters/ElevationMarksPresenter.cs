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
        private readonly MarkTypeServiceFactory factory;
        public ElevationMarksPresenter(IElevationMarksView _view)
        {
            View = _view;
            View.Presenter = this;
            factory = new MarkTypeServiceFactory();
        }

        public void CreateMark(int id)
        {
            using (var scope = DI.Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IMarkTypeService>();
                var markType = service.GetMarkType(id);
                var mark = Activator.CreateInstance(markType) as IElevationMark;
                mark.Create();
            }
        }

        public override void OnViewLoaded()
        {
            base.OnViewLoaded();
            foreach (DrawingStandards st in Enum.GetValues(typeof(DrawingStandards)))
            {
                var service = factory.GetMarkTypeService(st);
                View.BindDrawingStandard(st, service.GetMarks());
            }
        }
    }
}
