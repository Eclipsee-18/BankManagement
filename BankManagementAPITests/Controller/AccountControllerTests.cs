using AutoMapper;
using BankManagementApi.Controllers;
using BankManagementApi.Dto;
using BankManagementApi.Interfaces;
using BankManagementApi.Repository;
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
	public class AccountControllerTests
	{
		private readonly IBalanceInfoRepository _balanceInfoRepository;
		private readonly IMasterRepository _masterRepository;
		private readonly ILoyaltyPointsRepository _loyaltyPointsRepository;
		private readonly IMapper _mapper;

		public AccountControllerTests()
		{
			
			_masterRepository = A.Fake<IMasterRepository>();
			_balanceInfoRepository = A.Fake<IBalanceInfoRepository>();
			_loyaltyPointsRepository = A.Fake<ILoyaltyPointsRepository>();
			_mapper = A.Fake<IMapper>();

		}

		[Fact]
		public void AccountController_Accounts_ReturnOk()
		{
			//Arrange
			var accounts = A.Fake<ICollection<CustomerBalanceInfoDto>>();
			var accountList = A.Fake<List<CustomerBalanceInfoDto>>();
			A.CallTo(() => _mapper.Map<List<CustomerBalanceInfoDto>>(accounts)).Returns(accountList);
			var controller = new AccountsController(_balanceInfoRepository, _masterRepository,  _loyaltyPointsRepository, _mapper);

			//Act
			var result = controller.GetAccounts();

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}
	}
	
}
