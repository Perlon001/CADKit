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
        Type GetMarkType(int markNumber);
        string GetMarkName(int markNumber);
        string GetMarkName(MarkTypes type);


    }
}
