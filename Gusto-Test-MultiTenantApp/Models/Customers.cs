namespace Gusto_Test_MultiTenantApp.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class Address
    {
        public string street { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public object postalCode { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
    }

    public class Customer
    {
        public string id { get; set; }
        public string companyName { get; set; }
        public string contactName { get; set; }
        public string contactTitle { get; set; }
        public Address address { get; set; }
    }


}
