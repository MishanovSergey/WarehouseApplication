namespace WarehouseApp.Contracts.Models
{
    public class WarehouseItem //модель общего шаблона складской единицы
    {
        public Guid Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Depth { get; set; }
        public double? Weight { get; set; }
        public int? Volume { get; set; }
        public DateOnly? ExpirationDate { get; set; }
    }
}
