using DbConnection.Models;

namespace WarehouseApp.Models
{
    internal class PalletWithVolume
    {
        public required Pallet Pallet { get; set; }
        public int Volume { get; set; }
    }
}
