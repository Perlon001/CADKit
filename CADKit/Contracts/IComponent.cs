namespace CADKit.Contracts
{
    public interface IComponent
    {
        string Name { get; }
        string Title { get; set; }

        IComposite Parent { get; set; }
        bool IsComposite { get; }
    }
}
