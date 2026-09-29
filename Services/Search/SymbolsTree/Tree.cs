

namespace StockLens.Services.Search.SymbolsTree
{
    public class Tree<T> 
    {
        public TreeNode<T> root { get; }  
        public Tree(string id)
        {
            root = new();
            root.Children.Add(root.Id, root);
        }

        public Tree(TreeNode<T> root)
        {
            this.root = new();
            this.root.Children.Add(root.Id, root);
        }

        public TreeNode<T> GetConsistencyValues(string сonsist)
        {
            var currentNode = root;

            for (var i = 0; i < сonsist.Count(); i++) 
            {
                if (!currentNode!.Children.ContainsKey(сonsist[i].ToString()))
                    break;

                currentNode = currentNode.Children.GetValueOrDefault(сonsist[i].ToString());
            }
            return currentNode!;
        }

        public void AddNextTreeNode(string сonsist, IEnumerable<T> items, char id)
        {
            var currentNode = root;

            for (var i = 0; i < сonsist.Count(); i++)
            {
                if (!currentNode.Children.ContainsKey(сonsist[i].ToString()))
                {
                    if (сonsist[i].ToString() != currentNode.Id)
                        currentNode.Children.Add(сonsist[i].ToString(), new TreeNode<T>(сonsist[i].ToString()));
                }
                
                var next = currentNode.Children.GetValueOrDefault(сonsist[i].ToString());
                currentNode = next != null ? next : currentNode;

                if (currentNode.Id == id.ToString())
                    currentNode.Vals = items.ToList();
               
            }
        }
    } 
}
