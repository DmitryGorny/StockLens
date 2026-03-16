using Npgsql.TypeMapping;
using StockLens.Dtos.AuthDtos;
using StockLens.Models;
using System.Runtime.CompilerServices;

namespace StockLens.Mappers
{
    public static class UsersMapper
    {
        public static UsersСharacteristicsDto GetUsersCharacteristicsDto(this User user)
        {
            return new UsersСharacteristicsDto
            { 
                Experience = user.Experience,
                InvestmentHorizon = user.InvestmentHorizon,
                MaxDrawdownPercent = user.MaxDrawdownPercent,
                ReactionToDrop = user.ReactionToDrop,
            };

        }
    }
}
