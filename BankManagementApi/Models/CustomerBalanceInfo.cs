namespace BankManagementApi.Models
{
	public class CustomerBalanceInfo
	{
		public int Id { get; set; }
		public long Balance { get; set; }
		public string AccountType { get; set; }

		//nav
		public CustomerMaster Customer { get; set; }
		public CustomerLoyaltyPoints LoyaltyPoints { get; set; }
	}
}
