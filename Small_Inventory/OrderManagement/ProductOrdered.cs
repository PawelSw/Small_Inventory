

namespace Small_Inventory.ProductManagement
{
    public class ProductOrdered
    {
      
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int AmountOrdered { get; set; }

        public override string ToString()
        {
            return $"Product ID: {ProductId} - Name: {ProductName} - Amount ordered: {AmountOrdered} Pcs";
        }



    }
}
