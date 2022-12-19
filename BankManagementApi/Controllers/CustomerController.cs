using AutoMapper;

using BankManagementApi.Dto;
using BankManagementApi.Interfaces;
using BankManagementApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BankManagementApi.Controllers
{
	[EnableCors("appCors")]
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly IMasterRepository _masterRepository;
		private readonly IBalanceInfoRepository _balanceInfoRepository;
		private readonly IMapper _mapper;

		public CustomerController(IMasterRepository masterRepository
			,IBalanceInfoRepository balanceInfoRepository,IMapper mapper)
		{
			_masterRepository = masterRepository;
			_balanceInfoRepository = balanceInfoRepository;
			_mapper = mapper;
		}

		[HttpGet]
		//[ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
		public IActionResult GetCustomers()
		{
			var customers = _mapper.Map<List<CustomerMasterDto>>(_masterRepository.GetCustomers());
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(customers);
		}

		[HttpGet("{customerId}")]
		public IActionResult GetCustomer(int customerId)
		{
			if (!_masterRepository.CustomerExists(customerId))
			{
				return NotFound();

			}
			var customer = _mapper.Map<CustomerMasterDto>(_masterRepository.GetCustomer(customerId));

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(customer);
		}
		
		[HttpPost("addCustomer")]
		public IActionResult CreateCustomer([FromBody] CustomerMasterDto customerCreate)
		{
			if (customerCreate == null)
			{
				return BadRequest(ModelState);
			}

			var customers = _masterRepository.GetCustomers()
				.Where(c => c.Id == customerCreate.Id).FirstOrDefault();

			if (customers != null)
			{
				ModelState.AddModelError("", "Customer Already Exist");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
			{ return BadRequest(ModelState); }

			var customerMap = _mapper.Map<CustomerMaster>(customerCreate);


			if (!_masterRepository.CreateCustomer(customerMap))
			{
				ModelState.AddModelError("", "Something went wrong");
				return StatusCode(500, ModelState);
			}
			return Ok("Successfully Created");

		}

		[HttpPut("updateCustomer")]
		public IActionResult UpdateCustomer(int customerId, [FromBody] CustomerMasterDto updatedCustomer)
		{
			//if (updatedCustomer == null)
			//{
			//	return BadRequest(ModelState);
			//}
			//if (customerId != updatedCustomer.Id)
			//{
			//	return BadRequest(ModelState);
			//}
			//if (!_masterRepository.CustomerExists(customerId))
			//{
			//	return NotFound();
			//}
			//if (!ModelState.IsValid)
			//{
			//	return BadRequest();
			//}
			var customerMap = _mapper.Map<CustomerMaster>(updatedCustomer);

			if (!_masterRepository.UpdateCustomer(customerMap))
			{
				ModelState.AddModelError("", "Something went wrong updating");
				return StatusCode(500, ModelState);
			}
			return Ok("Successfully Updated");
		}

		[HttpPatch("updateCustomerPatch/{customerId}")]
		public IActionResult UpdateCustomerPatch( [FromBody] JsonPatchDocument<CustomerMaster> updatedCustomer,[FromRoute]int customerId)
		{
			_masterRepository.UpdateCustomerPatch(updatedCustomer, customerId);
			
			return Ok();
		}

		[HttpDelete("deleteCustomer/{customerId}")]
		public IActionResult DeleteCustomer(int customerId)
		{
			if (!_masterRepository.CustomerExists(customerId))
			{
				return NotFound();
			}

			var accountToDelete = _balanceInfoRepository.GetAccountOfACustomer(customerId);
			var customerToDelete = _masterRepository.GetCustomer(customerId);

			if (!ModelState.IsValid)
			{ return BadRequest(ModelState); }

			if (!_balanceInfoRepository.DeleteAccounts(accountToDelete.ToList()))
			{
				ModelState.AddModelError("", "Something went wrong");
			}

			if (!_masterRepository.DeleteCustomer(customerToDelete))
			{
				ModelState.AddModelError("", "Something went wrong");
			}
			return NoContent();
		}
	}
}
