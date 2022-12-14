using BankManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BankManagementApi.Data
{
	public class ApplicationDbContext:DbContext
	{
		public ApplicationDbContext(DbContextOptions options):base(options)
		{

		}

		public DbSet<CustomerMaster> CustomerMaster { get; set; }
		public DbSet<CustomerBalanceInfo> CustomerBalanceInfo { get; set; }
		public DbSet<CustomerLoyaltyPoints> CustomerLoyaltyPoints { get; set; }
	}
}
