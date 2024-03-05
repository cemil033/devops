using Microsoft.AspNetCore.Mvc;
using Build.MicroService.Entities;
using Build.Common;
using MassTransit;
using Build.Contracts;

[ApiController]
[Route("items")]
public class BuildController : ControllerBase
{
    private readonly IRepository<Item> itemRepository;
    private readonly IPublishEndpoint publishEndpoint;

    public BuildController(IRepository<Item> itemRepository, IPublishEndpoint publishEndpoint)
    {
        this.itemRepository = itemRepository;
        this.publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<IEnumerable<ItemDto>> GetAsync()
    {
        var items = (await itemRepository.GetAllAsync())
                    .Select(item => item.AsDto());
        return items;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
    {
        var item = await itemRepository.GetAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return item.AsDto();
    }
    [HttpPost]
    public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
    {
        var item = new Item
        {
            Id = Guid.NewGuid(),
            UserId = createItemDto.UserId,
            Address = createItemDto.Address,
            Description = createItemDto.Description,
            MetroStation = createItemDto.MetroStation,
            Price = createItemDto.Price,
            Floor = createItemDto.Floor,
            RoomCount = createItemDto.RoomCount,
            Size = createItemDto.Size,
            Images = createItemDto.Images,
            Longitute = createItemDto.Longitute,
            Latitude = createItemDto.Latitude,
            Type = createItemDto.Type,
            CreatedDate = DateTimeOffset.UtcNow,
            ExpiredDate = DateTimeOffset.UtcNow
        };
        await itemRepository.CreateAsync(item);
        await publishEndpoint.Publish(new BuildItemCreated(
                                                        item.Id,item.UserId,item.Address,item.Description,item.MetroStation,
                                                        item.Price,item.Floor,item.RoomCount,item.Size,item.Images,
                                                        item.Longitute,item.Latitude,item.Type,item.CreatedDate,item.ExpiredDate));
        return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateitemDto)
    {
        var existingitem = await itemRepository.GetAsync(id);
        if (existingitem == null)
        {
            return NotFound();
        }
        existingitem.Address = updateitemDto.Address;
        existingitem.Description = updateitemDto.Description;
        existingitem.MetroStation = updateitemDto.MetroStation;
        existingitem.Price = updateitemDto.Price;
        existingitem.Floor = updateitemDto.Floor;
        existingitem.RoomCount = updateitemDto.RoomCount;
        existingitem.Size = updateitemDto.Size;
        existingitem.Images = updateitemDto.Images;
        existingitem.Longitute = updateitemDto.Longitute;
        existingitem.Latitude = updateitemDto.Latitude;
        existingitem.Type = updateitemDto.Type;
        await itemRepository.UpdateAsync(existingitem);
        await publishEndpoint.Publish(new BuildItemUpdated(
                                                        existingitem.Id,existingitem.UserId,existingitem.Address,existingitem.Description,existingitem.MetroStation,
                                                        existingitem.Price,existingitem.Floor,existingitem.RoomCount,existingitem.Size,existingitem.Images,
                                                        existingitem.Longitute,existingitem.Latitude,existingitem.Type,existingitem.CreatedDate,existingitem.ExpiredDate));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var item = await itemRepository.GetAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        await itemRepository.RemoveAsync(item.Id);
        await publishEndpoint.Publish(new BuildItemDeleted(id));
        return NoContent();
    }
}