using CADKit.Models;
using CADKitElevationMarks.DTO;
using CADKitElevationMarks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Contracts.Services
{
    public interface IMarkTypeService
    {
        IList<MarkButtonDTO> GetMarks();
        IList<MarkButtonDTO> GetMarks(DrawingStandards standard);
        IList<MarkButtonDTO> GetMarks(DrawingStandards standard, MarkTypes[] types);
        IList<MarkButtonDTO> GetMarks(MarkTypes[] types);
        Type GetMarkType(int markNumber);
    }
}
