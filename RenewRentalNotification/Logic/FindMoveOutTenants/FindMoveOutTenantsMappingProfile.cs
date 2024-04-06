using AutoMapper;
using RenewRentalNotification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewRentalNotification.Logic.FindMoveOutTenants
{
    public class FindMoveOutTenantsMappingProfile : Profile
    {
        public FindMoveOutTenantsMappingProfile()
        {
            //CreateMap<Question, QuestionModel>();
            /*etc...*/

            CreateMap<TenantAssignment, FindMoveOutTenantsResultItem>()
                .ForMember(dst => dst.FirstName, x=> x.MapFrom(src => src.Tenant.FirstName))
                .ForMember(dst => dst.LastName, x=> x.MapFrom(src => src.Tenant.LastName))
                .ForMember(dst => dst.PhoneNumber, x=> x.MapFrom(src => src.Tenant.PhoneNumber))
                .ForMember(dst => dst.EmailAddress, x=> x.MapFrom(src => src.Tenant.EmailAddress))
                .ForMember(dst => dst.Address1, x=> x.MapFrom(src => src.RentalProperty.Address1))
                .ForMember(dst => dst.Address2, x=> x.MapFrom(src => src.RentalProperty.Address2))
                .ForMember(dst => dst.City, x=> x.MapFrom(src => src.RentalProperty.City))
                .ForMember(dst => dst.State, x=> x.MapFrom(src => src.RentalProperty.State))
                .ForMember(dst => dst.ZipCode, x=> x.MapFrom(src => src.RentalProperty.ZipCode))
                .ForMember(dst => dst.DateOfMoveIn, x=> x.MapFrom(src => src.DateOfMoveIn))
                .ForMember(dst => dst.ExpectedMoveOutDate, x=> x.MapFrom(src => src.ExpectedMoveOutDate))
                ;
        }

    }
}
