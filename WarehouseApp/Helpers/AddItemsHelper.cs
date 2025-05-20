using DbConnection;
using DbConnection.Models;
using Microsoft.EntityFrameworkCore;

namespace WarehouseApp.Helpers
{
    internal class AddItemsHelper
    {
        public static (int PalletsCount, int BoxesCount) AddItems(ApplicationContext context)
        {
                var random = new Random();

                int palletCount = random.Next(1, 6);

                for (int i = 0; i < palletCount; i++)
                {
                    int palletType = random.Next(1, 3);

                    var pallet = new Pallet
                    {
                        Id = Guid.NewGuid(),
                        Width = palletType == 1 ? 800 : 1200,
                        Height = 145,
                        Depth = 1200
                    };

                    int boxCount = random.Next(1, 8);

                    for (int j = 0; j < boxCount; j++)
                    {
                        int dateRandomType = random.Next(1, 3);
                        var productiondaysOffset = random.Next(-75, -10);
                        var expirationdaysOffset = random.Next(10, 75);
                        DateOnly? productionDate = dateRandomType == 1 ? null : DateOnly.FromDateTime(DateTime.Now.AddDays(productiondaysOffset));
                        DateOnly? expirationDate = dateRandomType == 2 ? null : DateOnly.FromDateTime(DateTime.Now.AddDays(expirationdaysOffset));

                        var box = new Box
                        {
                            Id = Guid.NewGuid(),
                            Width = palletType == 1 ? random.Next(100, 800) : random.Next(100, 1200),
                            Height = random.Next(20, 50),
                            Depth = random.Next(100, 1200),
                            Weight = Math.Round(random.NextDouble() * 50, 2),
                            ProductionDate = productionDate,
                            ExpirationDate = expirationDate,
                            PalletId = pallet.Id
                        };

                        context.Boxes.Add(box);
                    }

                    context.Pallets.Add(pallet);
                }

                context.SaveChanges();

            var palletsCount = context.Pallets.AsNoTracking().Count();
            var boxesCount = context.Boxes.AsNoTracking().Count();

            return (PalletsCount: palletsCount, BoxesCount: boxesCount);
        }
    }
}
