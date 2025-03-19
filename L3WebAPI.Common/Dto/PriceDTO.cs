using L3WebAPI.Common.Dao;
using L3WebAPI.Common.Request;

namespace L3WebAPI.Common.Dto;

public class PriceDTO
{
    public decimal valeur { get; set;}
    public Currency currency { get; set;}
}

public static class PriceDTOExtensions
{
    public static PriceDTO ToDTO(this PriceDAO priceDao)
    {
        return new PriceDTO
        {
            valeur = priceDao.valeur,
            currency = (Currency)priceDao.currency
        };
    }
}