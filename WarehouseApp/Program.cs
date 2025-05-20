using DbConnection;
using DbConnection.Repositories;
using Microsoft.EntityFrameworkCore;
using WarehouseApp.Contracts.Models;
using WarehouseApp.Helpers;

namespace WarehouseApp
{
    public class Program
    {
        const int NumOfBoxes = 3;
        const string userOptions = "Введите один из возможных вариантов:" +
                    "\n\"1\" - Добавить новые коробки и паллеты на склад" +
                    "\n\"2\" - Вывести сгруппированные по сроку годности паллеты" +
                    "\n\"3\" - Вывести отсортированные по объему паллеты, содержащие коробки с наибольшим сроком годности" +
                    "\n\"4\" - Закончить работу\n";
        const string palletsVolume = "Объемы паллет, содержащих коробки с максимальным сроком годности:";
        const string incorrectOption = "Выбран некорректный вариант";
        static void Main(string[] args)
        {
            using var context = new ApplicationContext();
            var palletRepository = new PalletRepository(context);

            context.Database.Migrate();

            string userOption;
            do
            {
                Console.WriteLine(userOptions);

                userOption = Console.ReadLine();
                Console.WriteLine("\n");

                switch (userOption)
                {
                    case "1":
                        List<Pallet> inputPallets = AddItemsHelper.AddItems();

                        foreach (var input in inputPallets)
                        {
                            palletRepository.Create(input);
                        }

                        Console.WriteLine("Объекты помещены на склад." +
                            $"\nПомещено паллет: {inputPallets.Count}" +
                            $"\nПомещено коробок: {inputPallets.SelectMany(p => p.Boxes).Count()}\n");
                        break;
                    case "2":
                        var grupedPallets = SortedItemsHelper.GetExpirationDatePallets(palletRepository);

                        foreach (var pallets in grupedPallets)
                        {
                            Console.WriteLine($"Срок годности: {pallets.GroupKey}");

                            foreach (var pallet in pallets.Pallets)
                            {
                                Console.WriteLine($"\tВес паллеты: {pallet.Weight}");
                            }
                        }

                        Console.WriteLine("\n");
                        break;
                    case "3":
                        var maxExpirationDatePallets = SortedItemsHelper.GetMaxDatePallets(palletRepository, NumOfBoxes);

                        Console.WriteLine(palletsVolume);

                        foreach (var pallet in maxExpirationDatePallets)
                        {
                            Console.WriteLine(pallet.Volume);
                        }

                        Console.WriteLine("\n");
                        break;
                    case "4":
                        break;
                    default:
                        Console.WriteLine(incorrectOption);
                        break;
                }
            }while (userOption != "4");
        }
    }
}
