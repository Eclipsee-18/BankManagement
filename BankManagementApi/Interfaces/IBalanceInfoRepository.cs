using BankManagementApi.Models;

namespace BankManagementApi.Interfaces
{
	public interface IBalanceInfoRepository
	{
		ICollection<CustomerBalanceInfo> GetAccounts();
		CustomerBalanceInfo GetAccount(int accountid);

		ICollection<CustomerBalanceInfo> GetAccountOfACustomer(int customerid);
		bool AccountExists(int accountid);

		bool CreateAccount(CustomerBalanceInfo account);
		bool UpdateAccount(CustomerBalanceInfo account);
		bool DeleteAccount(CustomerBalanceInfo account);
		bool DeleteAccounts(List<CustomerBalanceInfo> accounts);
		bool Save();
	}
}
