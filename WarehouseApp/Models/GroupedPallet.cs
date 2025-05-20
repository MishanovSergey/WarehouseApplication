namespace WarehouseApp.Models
{
    internal class GroupedPallet
    {
        public DateOnly ExpirationPalletDate { get; set; }
        public required List<PalletWithWeight> WeightSortedPallets { get; set; }
    }
}
