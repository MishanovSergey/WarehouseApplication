using DbConnection;
using WarehouseApp.Models;

namespace WarehouseApp.Helpers
{
    internal class SortedItemsHelper
    {
        public static List<GroupedPallet> GetExpirationDatePallets(ApplicationContext context)
        {
            return context.Pallets.GroupBy(p => p.Boxes
                .Min(b => b.ProductionDate != null ? ((DateOnly)b.ProductionDate).AddDays(100) : b.ExpirationDate))
                .OrderBy(g => g.Key)
                .Select(g => new GroupedPallet
                {
                    ExpirationPalletDate = (DateOnly)g.Key,
                    WeightSortedPallets = g.Select(p => new PalletWithWeight
                    {
                        Pallet = p,
                        Weight = p.Boxes.Sum(b => b.Weight) + 30
                    })
                    .OrderBy(x => x.Weight)
                    .ToList()
                }).ToList();
        }

        public static List<PalletWithVolume> GetMaxDatePallets(ApplicationContext context)
        {
            var maxExpirationDateBoxes = context.Boxes.OrderBy(b => b.ProductionDate != null ? ((DateOnly)b.ProductionDate).AddDays(100) : b.ExpirationDate).ToList().TakeLast(3).ToList();

            return context.Pallets.Where(p => p.Boxes.Any(i => maxExpirationDateBoxes.Contains(i)))
                .Select(p => new PalletWithVolume
                {
                    Pallet = p,
                    Volume = p.Boxes.Sum(b => b.Width * b.Height * b.Depth)
                    + p.Width * p.Height * p.Depth
                })
                .OrderBy(x => x.Volume)
                .ToList();
        }
    }
}
