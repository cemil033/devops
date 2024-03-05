
namespace Build.Contracts
{
    public record BuildItemCreated(
        Guid Id,
        Guid UserId,
        string Address,
        string Description,
        string MetroStation,
        decimal Price,
        int Floor,
        int RoomCount,
        decimal Size,
        List<string> Images,
        decimal Longitute,
        decimal Latitude,
        string Type,
        DateTimeOffset CreatedDate,
        DateTimeOffset ExpiredDate);
    public record BuildItemUpdated(
        Guid Id,
        Guid UserId,
        string Address,
        string Description,
        string MetroStation,
        decimal Price,
        int Floor,
        int RoomCount,
        decimal Size,
        List<string> Images,
        decimal Longitute,
        decimal Latitude,
        string Type,
        DateTimeOffset CreatedDate,
        DateTimeOffset ExpiredDate);
    public record BuildItemDeleted(Guid ItemId);
}