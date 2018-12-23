using CADKitCore.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitCore.Contract
{
    public interface ISettingsView : IView
    {
        ISettingsPresenter Presenter { get; set; }
        IScaleDTO SelectedScale { get; set; }

        void BindingScale(IList<IScaleDTO> scales);
    }
}
