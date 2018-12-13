using CADKitCore.Contract;

namespace CADKitElevationMarks.Contract
{
    public interface IElevationMarkConfig
    {
        string Layer { get; set; }
        IEntityText Sign { get; set; }
        IEntityText Text { get; set; }
        IEntityPline Pline { get; set; }
        IEntityHatch Hatch { get; set; }
        IEntityPoint Point { get; set; }
    }
}
