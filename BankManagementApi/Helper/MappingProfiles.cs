using AutoMapper;
using BankManagementApi.Dto;
using BankManagementApi.Models;

namespace BankManagementApi.Helper
{
	public class MappingProfiles:Profile
	{
		public MappingProfiles()
		{
			CreateMap<CustomerMaster, CustomerMasterDto>();
			CreateMap<CustomerMasterDto, CustomerMaster>();
			CreateMap<CustomerBalanceInfo, CustomerBalanceInfoDto>();
			CreateMap<CustomerBalanceInfoDto, CustomerBalanceInfo>();
			CreateMap<CustomerLoyaltyPoints, CustomerLoyaltyPointsDto>();
			CreateMap<CustomerLoyaltyPointsDto, CustomerLoyaltyPoints>();
		}
		
	}
}
