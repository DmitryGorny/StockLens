namespace StockLens.Services.Search.SymbolsTree
{
    public class TreeNode<T>
    {
        public TreeNode() { }
        public TreeNode(string id)
        {
            Id = id;
        }
        public TreeNode(string id, List<T> vals)
        {
            Id = id;
            Vals = vals;   
        }
        public string Id { get; set; }
        public Dictionary<string, TreeNode<T>> Children { get; } = new Dictionary<string, TreeNode<T>>();
        public List<T> Vals { get; set; }
    }
}
