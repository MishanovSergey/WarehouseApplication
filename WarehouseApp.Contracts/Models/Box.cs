namespace WarehouseApp.Contracts.Models
{
    public class Box : WarehouseItem //модель для работы с коробками на уровне логики
    {
        public DateOnly? ProductionDate { get; set; }
    }
}
