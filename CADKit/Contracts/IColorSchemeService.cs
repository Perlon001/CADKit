using CADKit.Models;
using System.Drawing;

namespace CADKit.Contracts
{
    public interface IColorSchemeService
    {
        InterfaceScheme GetScheme();
        Color GetBackColor();
        Color GetForeColor();
    }
}
