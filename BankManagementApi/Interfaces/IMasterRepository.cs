using BankManagementApi.Models;

namespace BankManagementApi.Interfaces
{
	public interface IMasterRepository
	{
		ICollection<CustomerMaster> GetCustomers();
		CustomerMaster GetCustomer(int id);

		bool CustomerExists(int customerId);

		bool CreateCustomer(CustomerMaster customer);
		bool UpdateCustomer(CustomerMaster customer);
		bool DeleteCustomer(CustomerMaster customer);
		bool Save();
	}
}
