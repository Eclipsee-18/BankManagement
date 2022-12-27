using BankManagementApi.Data;
using BankManagementApi.Models;
using BankManagementApi.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementAPITests.Repository
{
	public class BalanceInfoRepositoryTests
	{
		private async Task<ApplicationDbContext> GetDatabaseContext()
		{
			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;
			var databaseContext = new ApplicationDbContext(options);
			databaseContext.Database.EnsureCreated();

			if (await databaseContext.CustomerBalanceInfo.CountAsync() <= 0)
			{
				for (int i = 0; i < 10; i++)
				{
					databaseContext.CustomerBalanceInfo.Add(
						new CustomerBalanceInfo()
						{
							Balance=6000,
							AccountType="Savings",
							
							
						});
					await databaseContext.SaveChangesAsync();
				}
			}
			return databaseContext;
		}

		[Fact]
		public async void BalanceInfoRepository_GetAccountOfACustome_ReturnAccount()
		{
			//Arrange
			var customerid = 1;
			var dbContext = await GetDatabaseContext();
			var accountRepository = new BalanceInfoRepository(dbContext);

			//Act
			var result = accountRepository.GetAccountOfACustomer(customerid);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType<List<CustomerBalanceInfo>>();
		}

		[Fact]
		public async void BalanceInfoRepository_CreateAccount_ReturnOk()
		{
			//Arrange
			var account = new CustomerBalanceInfo()
			{
				Balance = 3000,
				AccountType = "Savings",
			};
			var dbContext = await GetDatabaseContext();
			var accountRepository = new BalanceInfoRepository(dbContext);

			//Act
			var result = accountRepository.CreateAccount(account);

			//Assert
			result.Should().BeTrue();

		}

		[Fact]
		public async void BalanceInfoRepository_DeleteAccount_ReturnOk()
		{
			//Arrange
			var id = 1;

			var dbContext = await GetDatabaseContext();
			var accountRepository = new BalanceInfoRepository(dbContext);
			var account = accountRepository.GetAccount(id);

			//Act
			var result = accountRepository.DeleteAccount(account);

			//Assert
			result.Should().BeTrue();

		}

		[Fact]
		public async void MasterRepository_UpdateAccount_ReturnOk()
		{
			//Arrange
			var id = 1;

			var dbContext = await GetDatabaseContext();
			var accountRepository = new BalanceInfoRepository(dbContext);
			var account = accountRepository.GetAccount(id);

			//Act
			var result = accountRepository.UpdateAccount(account);

			//Assert
			result.Should().BeTrue();

		}
	}
}
