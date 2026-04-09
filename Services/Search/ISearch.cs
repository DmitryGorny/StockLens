namespace StockLens.Services.Search
{
    public interface ISearch<T, K>
    {
        public Task<IEnumerable<K>> Search(T query);
    }
}
