using AutoMapper;
using RenewRentalNotification.Logic.FindMoveOutTenants;
using RenewRentalNotification.Logic.SendEmailToTenant;
using RenewRentalNotification.Logic.SendMoveOutListToManagement;
using RenewRentalNotification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewRentalNotification.Logic.Shared
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Question, QuestionModel>();
            /*etc...*/

            CreateMap<FindMoveOutTenantsResultItem, SendEmailToTenantItem>()
                .ForMember(dst => dst.FullAddress, x => x.MapFrom(src => string.Concat(src.Address1," ",src.Address2, " ", src.City, ", ", src.State)))
                ;

            CreateMap<FindMoveOutTenantsResultItem, SendMoveOutListToManagementListItem>()
                ;
        }

    }
}
