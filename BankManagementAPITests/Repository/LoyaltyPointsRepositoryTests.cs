using BankManagementApi.Data;
using BankManagementApi.Dto;
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
	public class LoyaltyPointsRepositoryTests
	{
		private async Task<ApplicationDbContext> GetDatabaseContext()
		{
			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;
			var databaseContext = new ApplicationDbContext(options);
			databaseContext.Database.EnsureCreated();

			if (await databaseContext.CustomerLoyaltyPoints.CountAsync() <= 0)
			{
				for (int i = 0; i < 10; i++)
				{
					databaseContext.CustomerLoyaltyPoints.Add(
						new CustomerLoyaltyPoints()
						{
							Points = 500,
							CustomerId=1
							
							

						});
					await databaseContext.SaveChangesAsync();
				}
			}
			return databaseContext;
		}

		[Fact]
		public async void LoyaltyPointsRepository_GetLoyalty_ReturnLoyalty()
		{
			//Arrange
			var id = 1;
			var dbContext = await GetDatabaseContext();
			var loyaltyRepository = new LoyaltyPointsRepository(dbContext);

			//Act
			var result = loyaltyRepository.GetLoyalty(id);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType<CustomerLoyaltyPoints>();
		}

	
	}
}
