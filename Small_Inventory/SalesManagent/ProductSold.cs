using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Small_Inventory.SalesManagent
{
    internal class ProductSold
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int AmountSold { get; set; }

        public override string ToString()
        {
            return $"Product ID: {ProductId} - Name: {ProductName} - Amount sold: {AmountSold} Pcs";
        }
    }
}
