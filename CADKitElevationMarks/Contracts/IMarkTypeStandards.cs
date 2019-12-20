using CADKit.Models;
using CADKitElevationMarks.DTO;
using CADKitElevationMarks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Contracts
{
    public interface IMarkTypeStandards
    {
        Dictionary<string, DrawingStandards> GetStandards();
        Dictionary<string, MarkTypes> GetTypes();
        IList<MarkButtonDTO> GetMarksList();
        IList<MarkButtonDTO> GetMarksList(DrawingStandards standard);
    }
}
