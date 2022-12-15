using BankManagementApi.Data;
using BankManagementApi.Interfaces;
using BankManagementApi.Models;
using Microsoft.Identity.Client;
using System.Security.Principal;

namespace BankManagementApi.Repository
{
	public class BalanceInfoRepository : IBalanceInfoRepository
	{
		private readonly ApplicationDbContext _context;

		public BalanceInfoRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public bool AccountExists(int accountid)
		{
			return _context.CustomerBalanceInfo.Any(a => a.Id == accountid);
		}

		public bool CreateAccount(CustomerBalanceInfo account)
		{
			_context.Add(account);
			return Save();
		}

		public bool DeleteAccount(CustomerBalanceInfo account)
		{
			_context.Remove(account);
			return Save();
		}

		public bool DeleteAccounts(List<CustomerBalanceInfo> accounts)
		{
			_context.RemoveRange(accounts);
			return Save();
		}

		public CustomerBalanceInfo GetAccount(int accountid)
		{
			return _context.CustomerBalanceInfo.Where(a => a.Id == accountid).FirstOrDefault();
		}

		public ICollection<CustomerBalanceInfo> GetAccountOfACustomer(int customerid)
		{
			return _context.CustomerBalanceInfo.Where(a => a.Customer.Id== customerid).ToList();
		}

		public ICollection<CustomerBalanceInfo> GetAccounts()
		{
			return _context.CustomerBalanceInfo.ToList();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateAccount(CustomerBalanceInfo account)
		{
			_context.Update(account);
			return Save();
		}
	}
}
