using DbConnection.Models;
using DbConnection;
using Microsoft.EntityFrameworkCore;
using WarehouseApp.Helpers;

namespace WarehouseApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new ApplicationContext();

            context.Database.Migrate();

            string userOption;
            do
            {
                Console.WriteLine("Введите один из возможных вариантов:" +
                    "\n\"1\" - Добавить новые коробки и паллеты на склад" +
                    "\n\"2\" - Вывести сгруппированные по сроку годности паллеты" +
                    "\n\"3\" - Вывести отсортированные по объему паллеты, содержащие коробки с наибольшим сроком годности" +
                    "\n\"4\" - Закончить работу\n");

                userOption = Console.ReadLine();
                Console.WriteLine("\n");

                switch (userOption)
                {
                    case "1":
                        (int PalletsCount, int BoxesCount) resultObjectsCount = AddItemsHelper.AddItems(context);

                        Console.WriteLine($"Объекты помещены на склад." +
                            $"\nВсего паллет: {resultObjectsCount.PalletsCount}" +
                            $"\nВсего коробок: {resultObjectsCount.PalletsCount}\n");
                        break;
                    case "2":
                        var palletsByExpirationDate = SortedItemsHelper.GetExpirationDatePallets(context);

                        foreach (var pallets in palletsByExpirationDate)
                        {
                            Console.WriteLine($"Срок годности: {pallets.ExpirationPalletDate}");

                            foreach (var pallet in pallets.WeightSortedPallets)
                            {
                                Console.WriteLine($"\tВес паллеты: {pallet.Weight}");
                            }
                        }

                        Console.WriteLine("\n");
                        break;
                    case "3":
                        var maxExpirationDatePallets = SortedItemsHelper.GetMaxDatePallets(context);

                        Console.WriteLine("Объемы паллет, содержащих коробки с максимальным сроком годности:");

                        foreach (var pallet in maxExpirationDatePallets)
                        {
                            Console.WriteLine(pallet.Volume);
                        }

                        Console.WriteLine("\n");
                        break;
                    case "4":
                        break;
                    default:
                        Console.WriteLine("Выбран некорректный вариант");
                        break;
                }
            }while (userOption != "4");
        }
    }
}
