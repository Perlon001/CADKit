using CADKitCore.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitCore.Presenters
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
