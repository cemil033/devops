using System.ComponentModel.DataAnnotations;

public record ItemDto(Guid Id,Guid UserId, string Address, string Description, string MetroStation, decimal Price, int Floor, int RoomCount, decimal Size,List<string> Images, decimal Longitute,decimal Latitude,string Type,DateTimeOffset CreatedDate,DateTimeOffset ExpiredDate);
public record CreateItemDto([Required]Guid UserId,[Required]string Address, string Description, string MetroStation,[Range(0,99999999)] decimal Price,[Range(0,99999999)] int Floor,[Range(0,99999999)] int RoomCount,[Range(0,99999999)] decimal Size,List<string> Images,[Required] decimal Longitute,[Required]decimal Latitude,[Required]string Type);
public record UpdateItemDto([Required]string Address, string Description, string MetroStation,[Range(0,99999999)]decimal Price,[Range(0,99999999)] int Floor,[Range(0,99999999)] int RoomCount,[Range(0,99999999)] decimal Size,List<string> Images,[Required] decimal Longitute,[Required]decimal Latitude,[Required]string Type);
