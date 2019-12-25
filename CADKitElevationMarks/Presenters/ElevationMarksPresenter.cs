using Autofac;
using CADKit;
using CADKit.Models;
using CADKit.Presenters;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Contracts.Presenters;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Contracts.Views;
using System;

namespace CADKitElevationMarks.Presenters
{
    public class ElevationMarksPresenter : Presenter<IElevationMarksView>, IElevationMarksPresenter
    {
        public ElevationMarksPresenter(IElevationMarksView _view)
        {
            View = _view;
            View.Presenter = this;
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
            using (var scope = DI.Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IMarkTypeService>();
                foreach (DrawingStandards st in Enum.GetValues(typeof(DrawingStandards)))
                {
                    View.BindDrawingStandard(st, service.GetMarks(st));
                }
            }
        }
    }
}
