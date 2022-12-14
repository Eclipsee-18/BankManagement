using System.Security.Principal;

namespace BankManagementApi.Models
{
	public class CustomerLoyaltyPoints
	{
		public int Id { get; set; }
		public int Points { get; set; }
		public int CustomerId { get; set; }

		//nav
		public CustomerBalanceInfo Account { get; set; }
		public int AccountId { get; set; }
		
	}
}
