
using System.Text;
namespace Small_Inventory.ProductManagement
{
    internal class ProductRepository
    {

        private string directory = @"E:\Files_Small_Inv\";

        private string productsFileName = "products.txt";
        private string productsSaveFileName = "products.txt";
   

        private void CheckForExistingProductFile()
        {
            string path = $"{directory}{productsFileName}";

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

        public List<Product> LoadProductsFromFile()
        {
            List<Product> products = new List<Product>();

            string path = $"{directory}{productsFileName}";
            try
            {
                CheckForExistingProductFile();

                string[] productsAsString = File.ReadAllLines(path);
                for (int i = 0; i < productsAsString.Length; i++)
                {
                    string[] productSplits = productsAsString[i].Split(';');

                    string productId = productSplits[0];
                    string name = productSplits[1];
             
                    bool success = int.TryParse(productSplits[2], out int maxItemsInStock);
                    if (!success)
                    {
                        maxItemsInStock = 100;
                    }

                    success = int.TryParse(productSplits[3], out int itemPrice);
                    if (!success)
                    {
                        itemPrice = 0;
                    }
                    success = int.TryParse(productSplits[4], out int amountInStock);
                    if (!success)
                    {
                        amountInStock = 0;
                    }

                    Product product = new Product( name, maxItemsInStock, itemPrice, amountInStock);
                    products.Add(product);

                }
            }

            catch (IndexOutOfRangeException iex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong parsing the file, please check the data!");
                Console.WriteLine(iex.Message);
            }
            catch (FileNotFoundException fnfex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The file couldn't be found!");
                Console.WriteLine(fnfex.Message);
                Console.WriteLine(fnfex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong while loading the file!");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ResetColor();
            }

            return products;
        }


        public void SaveToFile(List<Product> list)
        {
            StringBuilder sb = new StringBuilder();
            string path = $"{directory}{productsSaveFileName}";

            foreach (var item in list)
            {
                sb.Append(item.ConvertToStringForSaving());
                sb.Append(Environment.NewLine);
            }

            File.WriteAllText(path, sb.ToString());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Saved items successfully");
            Console.ResetColor();
        }

    }
}
