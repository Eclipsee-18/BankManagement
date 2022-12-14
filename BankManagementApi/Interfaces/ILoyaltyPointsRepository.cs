using BankManagementApi.Models;

namespace BankManagementApi.Interfaces
{
	public interface ILoyaltyPointsRepository
	{
		ICollection<CustomerLoyaltyPoints> GetLoyalties();
		CustomerLoyaltyPoints GetLoyalty(int loyaltyid);
		CustomerLoyaltyPoints GetLoyaltyOfAccount(int accountid);
		CustomerLoyaltyPoints GetLoyaltyOfCustomer(int customerid, int accountid);

		bool LoyaltyExists(int loyaltyid);

		bool CreateLoyalty(CustomerLoyaltyPoints loyalty);

		bool Save();
	}
}
