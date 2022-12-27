using AutoMapper;
using BankManagementApi.Controllers;
using BankManagementApi.Dto;
using BankManagementApi.Interfaces;
using BankManagementApi.Models;
using BankManagementApi.Repository;
using Castle.Core.Resource;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementAPITests.Controller
{
	public class CustomerControllerTests
	{
		private readonly IMasterRepository _masterRepository;
		private readonly IBalanceInfoRepository _balanceInfoRepository;
		private readonly IMapper _mapper;

		public CustomerControllerTests()
		{
			_masterRepository = A.Fake<IMasterRepository>();
			_balanceInfoRepository = A.Fake<IBalanceInfoRepository>();
			_mapper = A.Fake<IMapper>();
		}

		[Fact]
		public void CustomerController_GetCustomers_ReturnOk()
		{
			//Arrange
			var customers = A.Fake<ICollection<CustomerMasterDto>>();
			var customerList = A.Fake<List<CustomerMasterDto>>();
			A.CallTo(() => _mapper.Map<List<CustomerMasterDto>>(customers)).Returns(customerList);
			var controller = new CustomerController(_masterRepository, _balanceInfoRepository, _mapper);

			//Act
			var result = controller.GetCustomers();

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

		[Fact]
		public void CustomerController_GetCustomer_ReturnOk()
		{
			//Arrange
			int customerId = 1;
			var customer = A.Fake<CustomerMasterDto>();
			A.CallTo(() => _mapper.Map<CustomerMasterDto>(customer)).Returns(customer);
			var controller = new CustomerController(_masterRepository, _balanceInfoRepository, _mapper);

			//Act
			var result = controller.GetCustomer(customerId);

			//Assert
			result.Should().NotBeNull();

		}

		[Fact]
		public void CustomerController_DeleteCustomer_ReturnOk()
		{
			//Arrange
			int customerId = 1;
			var customer = A.Fake<CustomerMaster>();
			var accounts = A.Fake<List<CustomerBalanceInfo>>();
			A.CallTo(() => _balanceInfoRepository.GetAccountOfACustomer(customerId)).Returns(accounts);
			A.CallTo(() => _masterRepository.GetCustomer(customerId)).Returns(customer);
			A.CallTo(() => _masterRepository.DeleteCustomer(customer));
			A.CallTo(() => _balanceInfoRepository.DeleteAccounts(accounts));
			var controller = new CustomerController(_masterRepository, _balanceInfoRepository, _mapper);

			//Act
			var result = controller.DeleteCustomer(customerId);

			//Assert
			result.Should().NotBeNull();
		}

		[Fact]
		public void CustomerController_UpdateCustomer_ReturnOk()
		{
			//Arrange
			int customerId = 1;
			var customer = A.Fake<CustomerMaster>();
			var updatedCustomer = A.Fake<CustomerMasterDto>();
			var customerMap = A.Fake<CustomerMaster>();
			A.CallTo(() => _masterRepository.CustomerExists(customerId)).Returns(true);
			A.CallTo(() => _mapper.Map<CustomerMaster>(updatedCustomer)).Returns(customer);
		}

	}
}
