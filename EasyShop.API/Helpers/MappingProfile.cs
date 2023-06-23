using AutoMapper;
using EasyShop.API.DTOs;
using EasyShop.Core.Entities;

namespace EasyShop.API.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            CreateMap<Customer, CustomerDTO>();
                
        }
    }
}
