using CADKit.Contracts;
using System;

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

        public virtual void OnExceptionOccurrence(Exception ex)
        {
            View.ShowException(ex);
        }

        public virtual void OnViewLoaded()
        {

        }

        public virtual void Dispose()
        {

        }
    }
}
