using L3WebAPI.Common.Request;

namespace L3WebAPI.Common.Dto;

public class PriceDTO
{
    public decimal valeur { get; set;}
    public Currency currency { get; set;}
}