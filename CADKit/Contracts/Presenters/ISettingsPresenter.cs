using CADKit.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Contracts.Presenters
{
    public interface ISettingsPresenter : IPresenter
    {
        void OnDrawUnitSelect(object sender, EventArgs e);
        void OnDimUnitSelect(object sender, EventArgs e);
        void OnScaleSelect(object sender, EventArgs e);
    }
}
