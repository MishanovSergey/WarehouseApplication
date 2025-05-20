using DbConnection.Migrations;
using DbConnection.Models;
using Microsoft.EntityFrameworkCore;
using WarehouseApp;

namespace DbConnection.Repositories
{
    public class PalletRepository
    {
        private readonly ApplicationContext _context;

        public PalletRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<WarehouseApp.Contracts.Models.Pallet> GetAllPallets()
        {
            var result = _context.Pallets
                .Include(p => p.Boxes)
                .Select(p => new WarehouseApp.Contracts.Models.Pallet
                {
                    Id = p.Id,
                    Depth = p.Depth,
                    Height = p.Height,
                    Width = p.Width,
                    Boxes = p.Boxes
                        .Select(b => new WarehouseApp.Contracts.Models.Box
                        {
                            Id = b.Id,
                            Depth = b.Depth,
                            Height = b.Height,
                            Width = b.Width,
                            Weight = b.Weight,
                            ExpirationDate = b.ExpirationDate,
                            ProductionDate = b.ProductionDate,
                        })
                        .ToList()
                })
                .AsNoTracking()
                .ToList();

            return result;
        }

        public List<WarehouseApp.Contracts.Models.Pallet> GetMaxDatePallets(int numOfBoxes)
        {
            var maxExpirationDatePalletIds = _context.Boxes
                .OrderBy(b => b.ProductionDate != null ? b.ProductionDate.Value.AddDays(100) : b.ExpirationDate)
                .Select(b => b.PalletId)
                .Distinct()
                .ToList()
                .TakeLast(numOfBoxes)
                .ToList();

            var result = _context.Pallets
                .Include(p => p.Boxes)
                .Where(p => maxExpirationDatePalletIds.Contains(p.Id))
                .Select(p => new WarehouseApp.Contracts.Models.Pallet
                {
                    Id = p.Id,
                    Depth = p.Depth,
                    Height = p.Height,
                    Width = p.Width,
                    Boxes = p.Boxes
                        .Select(b => new WarehouseApp.Contracts.Models.Box
                        {
                            Id = b.Id,
                            Depth = b.Depth,
                            Height = b.Height,
                            Width = b.Width,
                            Weight = b.Weight,
                            ExpirationDate = b.ExpirationDate,
                            ProductionDate = b.ProductionDate,
                        })
                        .ToList()
                })
                .AsNoTracking()
                .ToList();

            return result;
        }

        public void Create(WarehouseApp.Contracts.Models.Pallet input)
        {
            var mappedPallet = new Pallet
            {
                Id = input.Id,
                Depth = input.Depth,
                Height = input.Height,
                Width = input.Width,
            };

            var mappedBoxes = input.Boxes.Select(input => new Box
            {
                Id = input.Id,
                Depth = input.Depth,
                Height = input.Height,
                Width = input.Width,
                Weight = input.Weight.Value,
                ExpirationDate = input.ExpirationDate,
                ProductionDate = input.ProductionDate,
                PalletId = mappedPallet.Id,
            });

            _context.Pallets.Add(mappedPallet);
            _context.Boxes.AddRange(mappedBoxes);

            _context.SaveChanges();
        }
    }
}
