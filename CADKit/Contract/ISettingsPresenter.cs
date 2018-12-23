using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitCore.Contract
{
    public interface ISettingsPresenter : IPresenter
    {
        void OnDrawUnitSelect(object sender, EventArgs e);
        void OnDimUnitSelect(object sender, EventArgs e);
        void OnScaleSelect(object sender, EventArgs e);
    }
}
