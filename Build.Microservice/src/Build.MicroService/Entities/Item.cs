using Build.Common;

namespace Build.MicroService.Entities
{
    public class Item:IEntity{
        public Guid Id{ get; set; }
        public Guid UserId{get;set;}
        public string Address{ get; set; }
        public string Description{ get; set; }
        public string MetroStation{ get; set; }
        public decimal Price{ get; set; }
        public int Floor{ get; set; }
        public int RoomCount{ get; set; }
        public decimal Size{ get; set; }
        public List<string> Images{ get; set; }
        public decimal Longitute{ get; set; }
        public decimal Latitude{ get; set; }
        public string Type{ get; set; }
        public DateTimeOffset CreatedDate{ get; set; }
        public DateTimeOffset ExpiredDate{ get; set; }
    }
}