namespace StockLens.Services.Search.SymbolsTree
{
    public class TreeNode<T>
    {
        public TreeNode() { }
        public TreeNode(string id)
        {
            this.id = id;
        }

        public TreeNode(string id, List<T> vals)
        {
            this.id = id;
            this.vals = vals;   
        }
        public string id { get; set; }
        public Dictionary<string, TreeNode<T>> _children { get; } = new Dictionary<string, TreeNode<T>>();
        public List<T> vals { get; set; }
    }
}
