using StockLens.Dtos.AuthDtos;

namespace StockLens.Dtos.QuotesDtos.Analytics.Fabric
{
    public class AnalyticsBuilder<T> where T : IAnalyticsDto 
    {
        private readonly List<T> AnalyticsDtos = new List<T>();
        public void AddAnalyticsDto(T Dto)
        {
            AnalyticsDtos.Add(Dto);
        }
        public AnalyticsContainerDto<T> WrapAnalyticsDtos(UsersСharacteristicsDto characterisctics)
        {
            return new AnalyticsContainerDto<T>
            {
                ReactionToDrop = characterisctics.ReactionToDrop,
                MaxDrawdownPercent = characterisctics.MaxDrawdownPercent,
                InvestmentHorizon = characterisctics.InvestmentHorizon,
                Experience = characterisctics.Experience,
                AnalyticsDtos = AnalyticsDtos
            };
        } 
    }
}
