namespace L3WebAPI.Common.Request;

public class CreateGameRequest
{
    public string Name { get; set; } = null!;
    public IEnumerable<CreateGameRequestPrice> Prices { get; set; } = null!;
}

public class CreateGameRequestPrice
{
    public decimal valeur { get; set; }
    public Currency currency { get; set; }
}