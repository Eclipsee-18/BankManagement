using BankManagementApi.Data;
using BankManagementApi.Interfaces;
using BankManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BankManagementApi.Repository
{
	public class MasterRepository : IMasterRepository
	{
		private readonly ApplicationDbContext _context;

		public MasterRepository(ApplicationDbContext context) 
		{
			_context = context;
		}
		public bool CreateCustomer(CustomerMaster customer)
		{
			_context.Add(customer);
			return Save();
		}

		public bool CustomerExists(int customerId)
		{
			return _context.CustomerMaster.Any(p => p.Id == customerId);
		}

		public bool DeleteCustomer(CustomerMaster customer)
		{
			_context.Remove(customer);
			return Save();
		}

		public CustomerMaster GetCustomer(int id)
		{
			return _context.CustomerMaster.Where(e => e.Id == id).FirstOrDefault();
		}

		public ICollection<CustomerMaster> GetCustomers()
		{
			return _context.CustomerMaster.OrderBy(x => x.Id).ToList();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateCustomer(CustomerMaster customer)
		{
			_context.Update(customer);
			return Save();
		}
	}
}
