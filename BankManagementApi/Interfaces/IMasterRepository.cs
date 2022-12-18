using BankManagementApi.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace BankManagementApi.Interfaces
{
	public interface IMasterRepository
	{
		ICollection<CustomerMaster> GetCustomers();
		CustomerMaster GetCustomer(int id);

		bool CustomerExists(int customerId);

		bool CreateCustomer(CustomerMaster customer);
		bool UpdateCustomer(CustomerMaster customer);

		bool UpdateCustomerPatch(JsonPatchDocument<CustomerMaster> customer,int customerid);
		bool DeleteCustomer(CustomerMaster customer);
		bool Save();
	}
}
