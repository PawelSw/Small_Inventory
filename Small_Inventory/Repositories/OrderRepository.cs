using Small_Inventory.ProductManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Small_Inventory.Repositories
{
    internal class OrderRepository
    {
        private string directory = @"E:\Files_Small_Inv\";

        private string ordersFileName = "orders.txt";
        private string ordersSaveFileName = "orders.txt";

        private void CheckForExistingProductFile()
        {
            string path = $"{directory}{ordersFileName}";

            bool existingFileFound = File.Exists(path);
            if (!existingFileFound)
            {
                //Create the directory
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(directory);

                //Create the empty file
                using FileStream fs = File.Create(path);
            }
        }
        public void SaveToFile(List<Order> list)
        {
            StringBuilder sb = new StringBuilder();
            string path = $"{directory}{ordersSaveFileName}";

            foreach (var item in list)
            {
                sb.Append(item.ConvertToStringForSaving());
                sb.Append(Environment.NewLine);
            }

            File.WriteAllText(path, sb.ToString());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Saved orders successfully");
            Console.ResetColor();
        }
    }
}
