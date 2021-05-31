using CADKit.Contracts;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.DTO;
using System;
using System.Collections.Generic;

namespace CADKitElevationMarks.Contract.Services
{
    public interface IMarkService : IComponentService
    {
        IEnumerable<MarkButtonDTO> GetMarkButtons();
        MarkButtonDTO GetMarkButton(DrawingStandards standard, MarkTypes type);
        MarkDTO GetMark(int _markNumber);
        IEnumerable<MarkDTO> GetMarks();

    }
}