using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.DTO;
using System;
using System.Collections.Generic;

namespace CADKitElevationMarks.Contract.Services
{
    public interface IMarkService
    {
        IEnumerable<MarkButtonDTO> GetMarks();
        MarkButtonDTO GetMarkButton(DrawingStandards standard, MarkTypes type);
        string GetMarkName(int markNumber);
        Type GetMarkType(int markNumber);
        Type GetJigType(int markNumber);
    }
}