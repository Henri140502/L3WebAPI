namespace L3WebAPI.Common.Dto;

public class GameDTO
{
    public Guid AppId { get; set; }
    public string Name { get; set; } = null!;
    public IEnumerable<PriceDTO> Prices { get; set; } = null!;
    public Uri LogoUri { get; set; } = null!;
}