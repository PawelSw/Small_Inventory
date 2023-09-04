using Small_Inventory;
using Small_Inventory.ProductManagement;

internal class Program
{
    private static void Main(string[] args)
    {
        PrintWelcome();
        
        Utilities.InitializeStock();

        Utilities.ShowMainMenu();

        Console.WriteLine("Application shutting down...");

        Console.ReadLine();



    }
    static void PrintWelcome()
    {

        Console.ForegroundColor = ConsoleColor.Green;

        Console.WriteLine("Welcome at the Small Inventory Management");

        Console.ResetColor();

        Console.WriteLine("Press enter key to start.");

        Console.ReadLine();

        Console.Clear();
    }
}