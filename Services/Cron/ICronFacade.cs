using StockLens.Dtos.QuotationsDtos;

namespace StockLens.Services.Cron
{
    public interface ICronFacade
    {
        public Task RequestQuotesDaily();
    }
}
