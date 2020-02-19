namespace CADKit.Contracts
{
    public interface IComponent
    {
        string Name { get; }
        string Title { get; set; }
        //string Layer { get; set; }
        //string Linetype { get; set; }
        //short ColorIndex { get; set; }
        IComponent Parent { get; set; }
        bool IsComposite { get; }
    }
}
