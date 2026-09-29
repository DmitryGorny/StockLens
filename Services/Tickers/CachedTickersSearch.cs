using StockLens.Dtos.TickersDto;
using StockLens.Services.Cache;
using StockLens.Services.Search;
using StockLens.Services.Search.SymbolsTree;

namespace StockLens.Services.Tickers
{
    public class CachedTickersSearch : ISearch<string, SearchTickerDto>
    {
        private readonly ISearch<string, SearchTickerDto> _tickersService;
        private readonly ICacheService _cacheService;

        public CachedTickersSearch(ISearch<string, SearchTickerDto> search, ICacheService cache) 
        {
            _tickersService = search;
            _cacheService = cache;
        }

        public async Task<IEnumerable<SearchTickerDto>> Search(string query)
        {
            var treeNode = await _cacheService.GetCache<TreeNode<SearchTickerDto>>("TickersSearch", query[0].ToString());

            if (treeNode == null) 
            {
                var tickers = await _tickersService.Search(query[0].ToString());

                var NewTree = new Tree<SearchTickerDto>(
                    new TreeNode<SearchTickerDto>(query[0].ToString(), tickers.ToList()));

                treeNode = NewTree.root;
            }

            var tree = new Tree<SearchTickerDto>(treeNode.Children[query[0].ToString()]);

            var node = tree.GetConsistencyValues(query);

            IEnumerable<SearchTickerDto> result;
            if (node.Id != query[^1].ToString())
            {
                if (node.Vals == null)
                {
                    result = await _tickersService.Search(query);
                } else
                {
                    result = node.Vals.Where(t =>
                    {
                        return query.All(l => query.IndexOf(l) == t.Symbol.IndexOf(l));
                    });
                }

                tree.AddNextTreeNode(query, result, query[^1]);
                await _cacheService.SetCache(tree.root, "TickersSearch", query[0].ToString());
                
                return result;

            } else if (node.Vals == null)
            {
                node = tree.GetConsistencyValues(query[0].ToString());
                result = node.Vals.Where(t =>
                {
                    return query.All(l => query.IndexOf(l) == t.Symbol.IndexOf(l));
                });

                tree.AddNextTreeNode(query, result, query[^1]);
                await _cacheService.SetCache(tree.root, "TickersSearch", query[0].ToString());

                return result;
            }

            await _cacheService.SetCache(tree.root, "TickersSearch", query[0].ToString());
            return node.Vals;
        }
    }
}
