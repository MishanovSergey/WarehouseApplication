using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConnection.Models
{
    public class Pallet : WarehouseItem
    {
        public ICollection<Box> Boxes { get; } = new HashSet<Box>();
    }
}
