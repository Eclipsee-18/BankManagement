using AutoMapper;
using BankManagementApi.Dto;
using BankManagementApi.Interfaces;
using BankManagementApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace BankManagementApi.Controllers
{
	[EnableCors("appCors")]
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly IBalanceInfoRepository _balanceInfoRepository;
		private readonly IMasterRepository _masterRepository;
		private readonly ILoyaltyPointsRepository _loyaltyPointsRepository;
		private readonly IMapper _mapper;

		public AccountsController(IBalanceInfoRepository balanceInfoRepository
			,IMasterRepository masterRepository,ILoyaltyPointsRepository loyaltyPointsRepository,IMapper mapper)
		{
			_balanceInfoRepository = balanceInfoRepository;
			_masterRepository = masterRepository;
			_loyaltyPointsRepository = loyaltyPointsRepository;
			_mapper = mapper;
		}

		[HttpGet]
		//[ProducesResponseType(200, Type = typeof(IEnumerable<Account>))]
		public IActionResult GetAccounts()
		{
			var accounts = _mapper.Map<List<CustomerBalanceInfoDto>>(_balanceInfoRepository.GetAccounts());

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(accounts);
		}

		[HttpGet("{customerId}")]
		public IActionResult GetAccountOfACustomer(int customerId)
		{
			var accounts = _mapper.Map<List<CustomerBalanceInfoDto>>(_balanceInfoRepository.GetAccountOfACustomer(customerId));

			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			return Ok(accounts);
		}

		[HttpPost("createAccount/{customerId}")]
		public IActionResult CreateAccount([FromRoute] int customerId, [FromBody] CustomerBalanceInfoDto accountCreate)
		{
			if (accountCreate == null)
			{
				return BadRequest(ModelState);
			}
			

			var accounts = _balanceInfoRepository.GetAccounts()
				.Where(c => c.Id == accountCreate.Id).FirstOrDefault();

			if (accounts != null)
			{
				ModelState.AddModelError("", "Customer Already Exist");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
			{ return BadRequest(ModelState); }

			var accountMap = _mapper.Map<CustomerBalanceInfo>(accountCreate);

			accountMap.Customer = _masterRepository.GetCustomer(customerId);



			if (!_balanceInfoRepository.CreateAccount(accountMap))
			{
				ModelState.AddModelError("", "Something went wrong");
				return StatusCode(500, ModelState);
			}

			

			int points = 0;
			if (accountMap.Balance >= 1000 && accountMap.Balance < 5000)
			{
				points = 500;
			}
			if (accountMap.Balance >= 5000)
			{
				points = 1000;
			}

			var lol = new CustomerLoyaltyPoints()

			{
				Points = points,
				AccountId = accountMap.Id,
				CustomerId = customerId

			};

			_loyaltyPointsRepository.CreateLoyalty(lol);

			return Ok("Successfully Created");
		}

		[HttpPut("updateAccount/{accountId}")]
		public IActionResult UpdateAccount(int accountId, [FromBody] CustomerBalanceInfoDto updatedAccount)
		{
			//if (updatedAccount == null)
			//{
			//	return BadRequest(ModelState);
			//}
			//if (accountId != updatedAccount.Id)
			//{
			//	return BadRequest(ModelState);
			//}
			//if (!_balanceInfoRepository.AccountExists(accountId))
			//{
			//	return NotFound();
			//}
			//if (!ModelState.IsValid)
			//{
			//	return BadRequest();
			//}
			var accountMap = _mapper.Map<CustomerBalanceInfo>(updatedAccount);

			if (!_balanceInfoRepository.UpdateAccount(accountMap))
			{
				ModelState.AddModelError("", "Something went wrong updating");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}

		[HttpDelete("deleteAccount/{accountId}")]
		public IActionResult DeleteAccount(int accountId)
		{
			if (!_balanceInfoRepository.AccountExists(accountId))
			{
				return NotFound();
			}

			var accountToDelete = _balanceInfoRepository.GetAccount(accountId);


			if (!ModelState.IsValid)
			{ return BadRequest(ModelState); }

			if (!_balanceInfoRepository.DeleteAccount(accountToDelete))
			{
				ModelState.AddModelError("", "Something went wrong");
			}

			return NoContent();
		}
	}
}
