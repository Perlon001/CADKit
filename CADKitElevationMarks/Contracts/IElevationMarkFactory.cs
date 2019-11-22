namespace CADKitElevationMarks.Contracts
{
    public interface IElevationMarkFactory
    {
        IElevationMark Create(ElevationMarkType type);
        IElevationMark Create(ElevationMarkType type, IElevationMarkConfig config);
    }
}