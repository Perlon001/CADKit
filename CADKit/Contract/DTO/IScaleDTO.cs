using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKit.Contract.DTO
{
    public interface IScaleDTO
    {
        IntPtr UniqueIdentifier { get; }
        string Name { get; set; }
    }
}
