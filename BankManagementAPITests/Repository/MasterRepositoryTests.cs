using BankManagementApi.Data;
using BankManagementApi.Models;
using BankManagementApi.Repository;
using Castle.Core.Resource;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementAPITests.Repository
{
	public class MasterRepositoryTests
	{
		private async Task<ApplicationDbContext> GetDatabaseContext()
		{
			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;
			var databaseContext = new ApplicationDbContext(options);
			databaseContext.Database.EnsureCreated();

			if (await databaseContext.CustomerMaster.CountAsync() <= 0)
			{
				for (int i = 0; i < 10; i++)
				{
					databaseContext.CustomerMaster.Add(
						new CustomerMaster()
						{
							FullName = "John",
							Address = "newyork",
							Email = "@.com",
							PhoneNo = 9974,
							Age=25
						});
					await databaseContext.SaveChangesAsync();
				}
			}
			return databaseContext;
		}

		[Fact]
		public async void MasterRepository_GetCustomer_ReturnCustomer()
		{
			//Arrange
			var id = 1;
			var dbContext = await GetDatabaseContext();
			var customerRepository = new MasterRepository(dbContext);

			//Act
			var result = customerRepository.GetCustomer(id);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType<CustomerMaster>();
		}

		[Fact]
		public async void MasterRepository_GetCustomers_ReturnCustomers()
		{
			//Arrange

			var dbContext = await GetDatabaseContext();
			var customerRepository = new MasterRepository(dbContext);

			//Act
			var result = customerRepository.GetCustomers();

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType<List<CustomerMaster>>();
		}

		[Fact]
		public async void MasterRepository_DeleteCustomer_ReturnOk()
		{
			//Arrange
			var id = 1;

			var dbContext = await GetDatabaseContext();
			var customerRepository = new MasterRepository(dbContext);
			var customer = customerRepository.GetCustomer(id);

			//Act
			var result = customerRepository.DeleteCustomer(customer);

			//Assert
			result.Should().BeTrue();

		}

		[Fact]
		public async void MasterRepository_UpdateCustomer_ReturnOk()
		{
			//Arrange
			var id = 1;

			var dbContext = await GetDatabaseContext();
			var customerRepository = new MasterRepository(dbContext);
			var customer = customerRepository.GetCustomer(id);

			//Act
			var result = customerRepository.UpdateCustomer(customer);

			//Assert
			result.Should().BeTrue();

		}
	}
}
