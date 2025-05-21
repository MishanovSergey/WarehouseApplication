namespace WarehouseApp.Contracts.Models
{
    public class Pallet : WarehouseItem //модель для работы с паллетами на уровне логики
    {
        public List<Box> Boxes { get; set; } = [];
    }
}
