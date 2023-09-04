using Small_Inventory.ProductManagement;
using Small_Inventory.Repositories;
using Small_Inventory.SalesManagent;

namespace Small_Inventory
{
    internal class Utilities
    {
        private static List<Product> inventory = new List<Product>();
        private static List<Order> listOrders = new();
        private static List<Sale> listSales = new();


        internal static void InitializeStock()
        {

            ProductRepository productRepository = new();
            inventory = productRepository.LoadProductsFromFile();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Loaded {inventory.Count} products!");

            Console.WriteLine("Press enter to continue!");
            Console.ResetColor();

            Console.ReadLine();
        }
        internal static void ShowMainMenu()
        {
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("********************");
            Console.WriteLine("* Select an action *");
            Console.WriteLine("********************");

            Console.WriteLine("1: Inventory management");
            Console.WriteLine("2: Order management");
            Console.WriteLine("3: Settings");
            Console.WriteLine("4: Save all data");
            Console.WriteLine("0: Close application");

            Console.Write("Your selection: ");

            string? userSelection = Console.ReadLine();
            switch (userSelection)
            {
                case "1":
                    ShowInventoryManagementMenu();
                    break;
                case "2":
                    ShowOrderManagementMenu();
                    break;
                case "3":
                    ShowSettingsMenu();
                    break;
                case "4":
                    SaveAllData();
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }

        private static void ShowSettingsMenu()
        {
            string? userSelection;

            do
            {
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("************");
                Console.WriteLine("* Settings *");
                Console.WriteLine("************");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("What do you want to do?");
                Console.ResetColor();

                Console.WriteLine("1: Change stock Minimum Level");
                Console.WriteLine("0: Back to main menu");

                Console.Write("Your selection: ");

                userSelection = Console.ReadLine();

                switch (userSelection)
                {
                    case "1":
                        ShowChangeStockMinQuantity();
                        break;

                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }
            while (userSelection != "0");
            ShowMainMenu();
        }

        private static void ShowChangeStockMinQuantity()
        {
            Console.WriteLine($"Enter the new stock Minimum Quantity (current value: {Product.StockMinLevel}) for all products at once.");
            Console.Write("New value: ");
            int newValue = int.Parse(Console.ReadLine() ?? "0");
            Product.StockMinLevel = newValue;
            Console.WriteLine($"New stock Minimum Quantity set to {Product.StockMinLevel}");

            foreach (var product in inventory)
            {
                product.UpdateLowStock();
            }

            Console.ReadLine();
        }
        private static void SaveAllData()
        {
            ProductRepository productRepository = new();
            productRepository.SaveToFile(inventory);

            OrderRepository orderRepository = new OrderRepository();
            orderRepository.SaveToFile(listOrders);

            SaleRepository saleRepository = new SaleRepository();
            saleRepository.SaveToFile(listSales);

            Console.ReadLine();
            ShowMainMenu();
        }
        private static void ShowInventoryManagementMenu()
        {
            string? userSelection;

            do
            {
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("************************");
                Console.WriteLine("* Inventory management *");
                Console.WriteLine("************************");

                ShowAllProductsOverview();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("What do you want to do?");
                Console.ResetColor();

                Console.WriteLine("1: Buy product");
                Console.WriteLine("2: Add new product");
                Console.WriteLine("3: View products with low stock");
                Console.WriteLine("0: Back to main menu");

                Console.Write("Your selection: ");

                userSelection = Console.ReadLine();

                switch (userSelection)
                {
                    case "1":
                        ShowDetailsAndUseProduct();
                        break;

                    case "2":
                        CreateNewProduct();
                        break;

                    case "3":
                        ShowProductsLowOnStock();
                        break;

                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }
            while (userSelection != "0");
            ShowMainMenu();
        }
        private static void ShowProductsLowOnStock()
        {
            List<Product> lowOnStockProducts = inventory.Where(p => p.IsBelowMinimumLevel).ToList();

            if (lowOnStockProducts.Count > 0)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("The following items are low on stock, order more soon!");
                Console.ResetColor();

                foreach (var product in lowOnStockProducts)
                {
                    Console.WriteLine(product.DisplayProductDetails());
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No items are currently low on stock!");
            }

            Console.ReadLine();
        }
        private static void CreateNewProduct()
        {
            Console.WriteLine("Please provide below details for product You want to create:");

            Product? newProduct = null;

            Console.Write("Enter the name of the product: ");
            string name = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter the price of the product: ");
            int price = int.Parse(Console.ReadLine() ?? "0.0");

            Console.Write("Enter the maximum number of items in stock for this product: ");
            int maxInStock = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter the initial amount in stock:");
            int amountInStock = int.Parse(Console.ReadLine() ?? "0");

            newProduct = new Product(name, maxInStock, price, amountInStock)
            {
                AmountInStock = amountInStock = 0,
            };

            if (newProduct != null)
                inventory.Add(newProduct);
        }
        private static void ShowAllProductsOverview()
        {

            foreach (var product in inventory)
            {
                Console.WriteLine(product.DisplayProductDetails());
                Console.WriteLine();
            }
        }

        private static void ShowDetailsAndUseProduct()
        {

            Sale newSale = new Sale();

            string? selectedProductId = string.Empty;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Creating new sale");
            Console.ResetColor();

            do
            {
                ShowAllProductsOverview();

                Console.WriteLine("Which product do you want to buy? (enter 0 to stop adding new products to the order)");

                Console.Write("Enter the ID of product: ");
                selectedProductId = Console.ReadLine();

                if (selectedProductId != "0")
                {
                    Product? selectedProduct = inventory.Where(p => p.Id == selectedProductId).FirstOrDefault();

                    if (selectedProduct != null)
                    {
                        Console.Write("How many do you want to buy?: ");
                        int amountOrdered = int.Parse(Console.ReadLine() ?? "0");

                        ProductSold orderItem = new ProductSold
                        {
                            ProductId = selectedProduct.Id,
                            ProductName = selectedProduct.Name,
                            AmountSold = amountOrdered,
                        };

                        selectedProduct.BuyProduct(orderItem.AmountSold);
                        newSale.soldItems.Add(orderItem);
                    }
                }

            } while (selectedProductId != "0");

            listSales.Add(newSale);

            Console.WriteLine("Sale created.");

            Console.WriteLine(newSale.ShowSaleDetails());

            Console.ReadLine();
        }

        private static void ShowOrderManagementMenu()
        {
            string? userSelection = string.Empty;

            do
            {
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("********************");
                Console.WriteLine("* Select an action *");
                Console.WriteLine("********************");

                Console.WriteLine("1: Show all purchases");
                Console.WriteLine("2: Add new order");
                Console.WriteLine("3: Show all orders");
                Console.WriteLine("0: Back to main menu");

                Console.Write("Your selection: ");

                userSelection = Console.ReadLine();

                switch (userSelection)
                {
                    case "1":
                        ShowAllPurchasesOverview();
                        break;
                    case "2":
                        ShowAddNewOrder();
                        break;
                    case "3":
                        ShowAllOrdersOverview();
                        break;

                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }
            while (userSelection != "0");
            ShowMainMenu();

        }
        private static void ShowAllOrdersOverview()
        {
            Console.WriteLine("List of all orders:");
            if (listOrders.Count == 0)
            {
                Console.WriteLine("No orders so far.");
            }
            else
            {
                foreach (Order o in listOrders)
                    Console.WriteLine(o.ShowOrderDetails());
            }

            Console.ReadLine();
        }

        private static void ShowAllPurchasesOverview()
        {
            Console.WriteLine("List of all purchases:");
            if (listSales.Count == 0)
            {
                Console.WriteLine("No sales so far.");
            }
            else
            {
                foreach (Sale o in listSales)
                    Console.WriteLine(o.ShowSaleDetails());
            }

            Console.ReadLine();
        }


        private static void ShowAddNewOrder()
        {
            Order newOrder = new Order();

            string? selectedProductId = string.Empty;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Creating new order");
            Console.ResetColor();

            do
            {
                ShowAllProductsOverview();

                Console.WriteLine("Which product do you want to order? (enter 0 to stop adding new products to the order)");

                Console.Write("Enter the ID of product: ");
                selectedProductId = Console.ReadLine();

                if (selectedProductId != "0")
                {
                    Product? selectedProduct = inventory.Where(p => p.Id == selectedProductId).FirstOrDefault();

                    if (selectedProduct != null)
                    {
                        Console.Write("How many do you want to order?: ");
                        int amountOrdered = int.Parse(Console.ReadLine() ?? "0");

                        ProductOrdered orderItem = new ProductOrdered
                        {
                            ProductId = selectedProduct.Id,
                            ProductName = selectedProduct.Name,
                            AmountOrdered = amountOrdered,


                        };

                        selectedProduct.IncreaseStockQuantity(orderItem.AmountOrdered);

                        newOrder.OrderItems.Add(orderItem);

                    }
                }

            } while (selectedProductId != "0");


            listOrders.Add(newOrder);

            Console.WriteLine("Order created.");

            Console.WriteLine(newOrder.ShowOrderDetails());

            Console.ReadLine();

        }

    }
}
