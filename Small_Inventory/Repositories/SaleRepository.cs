using Small_Inventory.ProductManagement;
using Small_Inventory.SalesManagent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Small_Inventory.Repositories
{
    internal class SaleRepository
    {
        private string directory = @"E:\Files_Small_Inv\";

        private string saleFileName = "sales.txt";
        private string saleSaveFileName = "sales.txt";

        private void CheckForExistingProductFile()
        {
            string path = $"{directory}{saleFileName}";

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
        public void SaveToFile(List<Sale> list)
        {
            StringBuilder sb = new StringBuilder();
            string path = $"{directory}{saleFileName}";

            foreach (var item in list)
            {
                sb.Append(item.ConvertToStringForSaving());
                sb.Append(Environment.NewLine);
            }

            File.WriteAllText(path, sb.ToString());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Saved sales successfully");
            Console.ResetColor();
        }
    }
}
