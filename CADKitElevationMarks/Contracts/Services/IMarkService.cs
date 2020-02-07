using CADKitElevationMarks.Contracts;
using System.Collections.Generic;

namespace CADKitElevationMarks.Contract.Services
{
    public interface IMarkService
    {
        IEnumerable<IMark> GetMarks();
        IMark GetMrk(DrawingStandards standard, MarkTypes type);
    }
}