namespace DbConnection.Models
{
    public class Box : WarehouseItem
    {
        public Guid PalletId { get; set; }
        public double Weight { get; set; }
        public DateOnly? ProductionDate { get; set; }
        public DateOnly? ExpirationDate { get; set; }
        public Pallet Pallet { get; set; } = null!;
    }
}
