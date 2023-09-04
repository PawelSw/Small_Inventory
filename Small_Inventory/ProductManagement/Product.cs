using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Small_Inventory.ProductManagement
{
    public class Product
    {
        private static int id = 1;
        public string Id { get; set; }
        private string name = string.Empty;
        private int maxItemsInStock = 0;
        public static int StockMinLevel = 4;

        public string Name
        {
            get { return name; }
            set
            {
                name = value.Length > 50 ? value[..50] : value;
            }
        }

        public int Price { get; set; }
        public int AmountInStock { get; set; }
        public bool IsBelowMinimumLevel { get; set; } 

        public Product(string name, int maxItemsInStock, int price, int amountInStock)
        {
            this.Name = name;
            this.maxItemsInStock = maxItemsInStock;
            this.Price = price;
            Id = id.ToString();
            id++;
            AmountInStock = amountInStock;

            if (AmountInStock < StockMinLevel)
            {
                IsBelowMinimumLevel = true;
            }
        }

        public string DisplayProductDetails()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"Id: {Id}\nName: {Name}\nPrice: {Price} PLN/pc\n{AmountInStock} pc(s) in stock\n" );

            if (IsBelowMinimumLevel)
            {
                sb.Append("\nBelow minimum stock!");
            }

            return sb.ToString();
        }

        public string ShortDisplayOfProduct()
        {
            return $"Product Id: {Id} Name: {Name}";
        }
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void UpdateLowStock()
        {
            if (AmountInStock < StockMinLevel)
            {
                IsBelowMinimumLevel = true;
            }
        }

        public void BuyProduct(int quantity)
        {
            if (quantity <= AmountInStock)
            {
                AmountInStock -= quantity;
                UpdateLowStock();
                Console.ForegroundColor = ConsoleColor.Green;
                Log($"Updated stock after last sale:");
                Console.ResetColor();
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Log($"Not enough items on stock for {ShortDisplayOfProduct()}.\n{AmountInStock} pcs available but {quantity} requested.");
                Console.ResetColor();
            }   
        }

        public void IncreaseStockQuantity(int amount)
        {
            int newStock = AmountInStock + amount;

            if (newStock <= maxItemsInStock)
            {
                AmountInStock += amount;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Log($"{ShortDisplayOfProduct()} stock overflow. Order includes {amount} but only {maxItemsInStock - AmountInStock} can be stored.");
                Console.ResetColor();
            }   

        }
        public string ConvertToStringForSaving()
        {
            return $"{Id};{Name};{maxItemsInStock};{Price};{AmountInStock}";
        }
        public static void ChangeStockTreshold(int newStockTreshhold)
        {
            if (newStockTreshhold > 0)
                StockMinLevel = newStockTreshhold;
        }
    }
}
