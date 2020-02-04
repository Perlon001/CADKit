using Autofac;
using CADKit.Contracts;

namespace CADKit.UI
{
    public abstract class Presenter<TView> : IPresenter where TView : IView
    {
        private TView view;

        public TView View
        {
            get
            {
                return view;
            }
            set
            {
                view = value;
                view.Load += (s, e) => OnViewLoaded();
            }
        }

        public virtual void Dispose()
        {
        }

        public virtual void OnViewLoaded()
        {
            using (var scope = DI.Container.BeginLifetimeScope())
            {
                View.SetColorScheme(scope.Resolve<IInterfaceSchemeService>());
            }
        }
    }
}
