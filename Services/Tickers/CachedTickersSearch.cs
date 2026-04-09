using Newtonsoft.Json;
using StockLens.Dtos.TickersDto;
using StockLens.Models;
using StockLens.Services.Cache;
using StockLens.Services.Search;
using StockLens.Services.Search.SymbolsTree;
using System.Runtime.CompilerServices;
using System.Text.Json;

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

                Tree<SearchTickerDto> NewTree = new Tree<SearchTickerDto>(
                    new TreeNode<SearchTickerDto>(query[0].ToString(), tickers.ToList()));

                treeNode = NewTree.root;
            }

            Tree<SearchTickerDto> tree = new Tree<SearchTickerDto>(treeNode._children[query[0].ToString()]);

            var node = tree.GetConsistencyValues(query);

            IEnumerable<SearchTickerDto> result;
            if (node.id != query[^1].ToString())
            {
                if (node.vals == null)
                {
                    result = await _tickersService.Search(query);
                } else
                {
                    result = node.vals.Where(t =>
                    {
                        return query.All(l => query.IndexOf(l) == t.Symbol.IndexOf(l));
                    });
                }

                tree.AddNextTreeNode(query, result, query[^1]);
                await _cacheService.SetCache(tree.root, "TickersSearch", query[0].ToString());
                return result;

            } else if (node.vals == null)
            {
                node = tree.GetConsistencyValues(query[0].ToString());
                result = node.vals.Where(t =>
                {
                    return query.All(l => query.IndexOf(l) == t.Symbol.IndexOf(l));
                });

                tree.AddNextTreeNode(query, result, query[^1]);
                await _cacheService.SetCache(tree.root, "TickersSearch", query[0].ToString());
                return result;
            }

            await _cacheService.SetCache(tree.root, "TickersSearch", query[0].ToString());
            return node.vals;
                    
        }
    }
}
