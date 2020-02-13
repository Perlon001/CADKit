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
        MarkDTO GetMark(int _markNumber);
        //string GetMarkName(int markNumber);
        //string GetMarkDescription(int markNumber);
        //string GetMarkDescription(DrawingStandards standard, MarkTypes type);
        //Type GetMarkType(int markNumber);
        //Type GetJigType(int markNumber);
    }
}