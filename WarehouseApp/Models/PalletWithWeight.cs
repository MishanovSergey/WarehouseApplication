using DbConnection.Models;

namespace WarehouseApp.Models
{
    internal class PalletWithWeight
    {
        public required Pallet Pallet { get; set; }
        public double Weight { get; set; }
    }
}
