using AutoMapper;
using Repository_基础结构层.Models;

namespace MVCEchartsManager.ProfileMVC
{
    public class SourceProfile : Profile
    {
       public SourceProfile()
        {
           base.CreateMap<ShopingInfo, ShopingInfoViewModel>();

          // base.CreateMap<ShopingInfo, ShopingInfoViewModel>().ForMember(x => x.Name,
          //      q => { q.MapFrom(z => z.ShopingName);
          //  });
        }
    }
}