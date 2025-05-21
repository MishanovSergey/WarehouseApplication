namespace WarehouseApp.Contracts.Models
{
    public class GroupedPallets //модель для работы со сгруппированными паллетами на уровне логики
    {
        public DateOnly GroupKey { get; set; }
        public required List<Pallet> Pallets { get; set; }
    }
}
