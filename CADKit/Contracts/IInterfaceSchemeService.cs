using CADKit.Models;
using System;
using System.Drawing;

namespace CADKit.Contracts
{
    public interface IInterfaceSchemeService
    {
        InterfaceScheme GetScheme();
        Color GetBackColor();
        Color GetForeColor();
    }
}
