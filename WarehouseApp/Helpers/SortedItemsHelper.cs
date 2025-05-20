using DbConnection;
using DbConnection.Repositories;
using Microsoft.EntityFrameworkCore;
using WarehouseApp.Contracts.Models;

namespace WarehouseApp.Helpers
{
    internal class SortedItemsHelper
    {
        public static List<GroupedPallets> GetExpirationDatePallets(PalletRepository palletRepository)
        {
            var pallets = palletRepository.GetAllPallets();

            foreach (var p in pallets)
            {
                p.ExpirationDate = p.Boxes.Min(b => b.ProductionDate != null ? b.ProductionDate.Value.AddDays(100) : b.ExpirationDate);
                p.Weight = p.Boxes.Sum(b => b.Weight) + 30;
            }

            return pallets.GroupBy(p => p.ExpirationDate)
                .OrderBy(g => g.Key)
                .Select(g => new GroupedPallets
                {
                    GroupKey = g.Key.Value,
                    Pallets = g.Select(p => p)
                .OrderBy(p => p.Weight)
                .ToList()
                }).ToList();
        }

        public static List<Pallet> GetMaxDatePallets(PalletRepository palletRepository, int numOfBoxes)
        {
            var pallets = palletRepository.GetMaxDatePallets(numOfBoxes);

            foreach(var p in pallets)
            {
                p.Volume = p.Boxes.Sum(b => b.Width * b.Height * b.Depth) + p.Width * p.Height * p.Depth;
            }

            return pallets
                .OrderBy(p => p.Volume)
                .ToList();
        }
    }
}
