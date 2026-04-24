

namespace StockLens.Services.Search.SymbolsTree
{
    public class Tree<T> 
    {
        public TreeNode<T> root { get; }  

        public Tree() { }
        public Tree(string id)
        {
            var r = new TreeNode<T>(id.ToString());

            root = new();
            root._children.Add(root.id, root);
        }

        public Tree(TreeNode<T> root)
        {
            this.root = new();
            this.root._children.Add(root.id, root);
        }

        public TreeNode<T> GetConsistencyValues(string сonsist)
        {
            var currentNode = root;

            for (var i = 0; i < сonsist.Count(); i++) 
            {
                if (!currentNode._children.Keys.Contains(сonsist[i].ToString()))
                    break;

                currentNode = currentNode._children.GetValueOrDefault(сonsist[i].ToString());
            }
            return currentNode;
        }

        public void AddNextTreeNode(string сonsist, IEnumerable<T> items, char id)
        {
            var currentNode = root;

            for (var i = 0; i < сonsist.Count(); i++)
            {
                if (!currentNode._children.ContainsKey(сonsist[i].ToString()))
                {
                    if (сonsist[i].ToString() != currentNode.id)
                        currentNode._children.Add(сonsist[i].ToString(), new TreeNode<T>(сonsist[i].ToString()));
                }
                
                var next = currentNode._children.GetValueOrDefault(сonsist[i].ToString());
                currentNode = next != null ? next : currentNode;

                if (currentNode.id == id.ToString())
                    currentNode.vals = items.ToList();
               
            }
        }
    } 
}
