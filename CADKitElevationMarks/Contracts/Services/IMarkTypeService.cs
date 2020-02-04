using CADKitElevationMarks.DTO;
using CADKitElevationMarks.Models;
using System;
using System.Collections.Generic;

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
