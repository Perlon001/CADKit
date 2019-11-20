namespace CADKitElevationMarks.Contracts
{
    public interface IElevationMarkFactory
    {
        IElevationMark ElevationMark(ElevationMarkType type);
        IElevationMark ElevationMark(ElevationMarkType type, IElevationMarkConfig config);
    }
}