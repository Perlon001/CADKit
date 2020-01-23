namespace CADKit.Contracts
{
    public interface IComponent
    {
        string LeafName { get; set; }
        string LeafTitle { get; set; }
        string Layer { get; set; }
        string Linetype { get; set; }
        short ColorIndex { get; set; }
        IComponent Parent { get; set; }
    }
}
