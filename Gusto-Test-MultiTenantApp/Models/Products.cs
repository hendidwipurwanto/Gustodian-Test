namespace Gusto_Test_MultiTenantApp.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);


    public class Category
    {
        public int id { get; set; }
        public string description { get; set; }
        public string name { get; set; }
    }

    public class Product
    {
        public int id { get; set; }
        public int supplierId { get; set; }
        public int categoryId { get; set; }
        public string quantityPerUnit { get; set; }
        public double unitPrice { get; set; }
        public decimal unitsInStock { get; set; }
        public int unitsOnOrder { get; set; }
        public int reorderLevel { get; set; }
        public bool discontinued { get; set; }
        public string name { get; set; }
        public Supplier supplier { get; set; }
        public Category category { get; set; }
    }

    public class Supplier
    {
        public int id { get; set; }
        public string companyName { get; set; }
        public string contactName { get; set; }
        public string contactTitle { get; set; }
        public Address address { get; set; }
    }


}
