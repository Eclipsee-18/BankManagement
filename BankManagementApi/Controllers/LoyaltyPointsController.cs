using AutoMapper;
using BankManagementApi.Dto;
using BankManagementApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankManagementApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoyaltyPointsController : ControllerBase
	{
		private readonly ILoyaltyPointsRepository _loyaltyPointsRepository;
		private readonly IMasterRepository _masterRepository;
		private readonly IBalanceInfoRepository _balanceInfoRepository;
		private readonly IMapper _mapper;

		public LoyaltyPointsController(ILoyaltyPointsRepository loyaltyPointsRepository
				,IMasterRepository masterRepository,IBalanceInfoRepository balanceInfoRepository,IMapper mapper )
		{
			_loyaltyPointsRepository = loyaltyPointsRepository;
			_masterRepository = masterRepository;
			_balanceInfoRepository = balanceInfoRepository;
			_mapper = mapper;
		}

		[HttpGet]
		//[ProducesResponseType(200, Type = typeof(IEnumerable<CustomerLoyaltyPoints>))]
		public IActionResult GetLoyalties()
		{
			var loyalties = _mapper.Map<List<CustomerLoyaltyPointsDto>>(_loyaltyPointsRepository.GetLoyalties());

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(loyalties);
		}

		[HttpGet("{accountId}")]
		public IActionResult GetLoyaltyOfAccount(int accountId)
		{

			var loyalty = _mapper.Map<CustomerLoyaltyPointsDto>(_loyaltyPointsRepository.GetLoyaltyOfAccount(accountId));

			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			return Ok(loyalty);
		}
	}
}
