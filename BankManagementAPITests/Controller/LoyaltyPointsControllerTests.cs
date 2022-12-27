using AutoMapper;
using BankManagementApi.Controllers;
using BankManagementApi.Dto;
using BankManagementApi.Interfaces;
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
	public class LoyaltyPointsControllerTests
	{
		private readonly IBalanceInfoRepository _balanceInfoRepository;
		private readonly IMasterRepository _masterRepository;
		private readonly ILoyaltyPointsRepository _loyaltyPointsRepository;
		private readonly IMapper _mapper;

		public LoyaltyPointsControllerTests()
		{
			_masterRepository = A.Fake<IMasterRepository>();
			_balanceInfoRepository = A.Fake<IBalanceInfoRepository>();
			_loyaltyPointsRepository=A.Fake<ILoyaltyPointsRepository>();
			_mapper = A.Fake<IMapper>();
		}

		[Fact]
		public void LoyaltyController_GetLoyaltyOfAccount_ReturnOk()
		{
			//Arrange
			int accountId = 1;
			var loyalty = A.Fake<CustomerLoyaltyPointsDto>();

			A.CallTo(() => _mapper.Map<CustomerLoyaltyPointsDto>(loyalty)).Returns(loyalty);
			var controller = new LoyaltyPointsController(_loyaltyPointsRepository,_masterRepository,_balanceInfoRepository, _mapper);

			//Act
			var result = controller.GetLoyaltyOfAccount(accountId);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}
	}
}
