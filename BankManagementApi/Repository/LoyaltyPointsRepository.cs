using BankManagementApi.Data;
using BankManagementApi.Interfaces;
using BankManagementApi.Models;

namespace BankManagementApi.Repository
{
	public class LoyaltyPointsRepository : ILoyaltyPointsRepository
	{
		private readonly ApplicationDbContext _context;

		public LoyaltyPointsRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public bool CreateLoyalty(CustomerLoyaltyPoints loyalty)
		{
			_context.Add(loyalty);
			return Save();
		}

		public ICollection<CustomerLoyaltyPoints> GetLoyalties()
		{
			return _context.CustomerLoyaltyPoints.ToList();
		}

		public CustomerLoyaltyPoints GetLoyalty(int loyaltyid)
		{
			return _context.CustomerLoyaltyPoints.Where(a => a.Id == loyaltyid).FirstOrDefault();
		}

		public CustomerLoyaltyPoints GetLoyaltyOfAccount(int accountid)
		{
			return _context.CustomerLoyaltyPoints.Where(a => a.Account.Id == accountid).FirstOrDefault();
		}

		public CustomerLoyaltyPoints GetLoyaltyOfCustomer(int customerid, int accountid)
		{
			return _context.CustomerLoyaltyPoints.Where(a => a.Account.Customer.Id == customerid).FirstOrDefault();
		}

		public bool LoyaltyExists(int loyaltyid)
		{
			return _context.CustomerLoyaltyPoints.Any(a => a.Id == loyaltyid);
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
