using CADKit.Contracts;
using System;

namespace CADKitBasic.Contracts.Presenters
{
    public interface ISettingsPresenter : IPresenter
    {
        void OnDrawUnitSelect(object sender, EventArgs e);
        void OnDimUnitSelect(object sender, EventArgs e);
        void OnScaleSelect(object sender, EventArgs e);
    }
}
