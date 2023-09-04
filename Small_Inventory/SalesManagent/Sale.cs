using Small_Inventory.ProductManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Small_Inventory.SalesManagent
{
    internal class Sale
    {
        private static int id = 1;
        public string Id { get; set; }
        public DateTime OrderFulfilmentDate { get; private set; }
        public List<ProductSold> soldItems { get; }


        public Sale()
        {
            Id = id.ToString();
            id++;
            OrderFulfilmentDate = DateTime.Now;

            soldItems = new List<ProductSold>();
        }

        public string ShowSaleDetails()
        {
            StringBuilder saleDetails = new StringBuilder();

            saleDetails.AppendLine($"Sale ID: {Id}");
            saleDetails.AppendLine($"Sale fulfilment date: {OrderFulfilmentDate}");
            saleDetails.AppendLine($"Sale includes the following items:");

            if (soldItems != null)
            {
                foreach (ProductSold item in soldItems)
                {
                    saleDetails.AppendLine($"Name: {item.ProductName}, Quantity: {item.AmountSold} Pcs");
                }
            }

            return saleDetails.ToString();
        }
        public string ConvertToStringForSaving()
        {
            StringBuilder saleDetails = new StringBuilder();

            saleDetails.AppendLine($"{Id};{OrderFulfilmentDate} ");
            foreach (ProductSold item in soldItems)
            {
                saleDetails.AppendLine($"{item.ProductName}; {item.AmountSold}");


            }
            return saleDetails.ToString();
        }
    }
}
