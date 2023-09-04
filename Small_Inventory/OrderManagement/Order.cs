
using Small_Inventory.ProductManagement;
using System.Text;
using System.Xml.Linq;

namespace Small_Inventory.ProductManagement
{
    public class Order
    {
        private static int id = 1;
        public string Id { get; set; }
        public DateTime OrderFulfilmentDate { get; private set; }
        public List<ProductOrdered> OrderItems { get; }


        public Order()
        {
            Id = id.ToString();
            id++;
            OrderFulfilmentDate = DateTime.Now;

            OrderItems = new List<ProductOrdered>();
        }

        public string ShowOrderDetails()
        {
            StringBuilder orderDetails = new StringBuilder();

            orderDetails.AppendLine($"Order ID: {Id}");
            orderDetails.AppendLine($"Order fulfilment date: {OrderFulfilmentDate}");
            orderDetails.AppendLine($"Order includes the following items:");

            if (OrderItems != null)
            {
                foreach (ProductOrdered item in OrderItems)
                {
                    orderDetails.AppendLine($"Name: { item.ProductName}, Quantity: { item.AmountOrdered} Pcs");
                }
            }

            return orderDetails.ToString();
        }
        public string ConvertToStringForSaving()
        {
            StringBuilder orderDetails = new StringBuilder();

            orderDetails.AppendLine($"{Id};{OrderFulfilmentDate} ");
            foreach (ProductOrdered item in OrderItems) 
            {
                orderDetails.AppendLine($"{ item.ProductName}; { item.AmountOrdered}");


            }
            return orderDetails.ToString();
        }
    }
}
