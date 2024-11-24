namespace DataScientistTest.Models
{
    public class Transaction
    {
        public string AccountId { get; set; }
        public int Amount { get; set; } // Céntimos
        public DateTime CreatedDate { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
    }
}
