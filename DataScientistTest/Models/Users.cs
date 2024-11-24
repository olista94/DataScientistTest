namespace DataScientistTest.Models
{
    public class User
    {
        public string OwnerId { get; set; }
        public string AccountId { get; set; }
        public string Currency { get; set; }
        public int CurrentBalance { get; set; } // Céntimos
        public DateTime LastBalanceUpdate { get; set; }
        public string Country { get; set; }
    }
}
