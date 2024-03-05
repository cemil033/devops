using Build.MicroService.Entities;

public static class Extensions{
    public static ItemDto AsDto(this Item item){
        return 
        new ItemDto(
            item.Id,
            item.UserId,
            item.Address,
            item.Description,
            item.MetroStation,
            item.Price,
            item.Floor,
            item.RoomCount,
            item.Size,
            item.Images,
            item.Longitute,
            item.Latitude,
            item.Type,
            item.CreatedDate,
            item.ExpiredDate);
    }
}