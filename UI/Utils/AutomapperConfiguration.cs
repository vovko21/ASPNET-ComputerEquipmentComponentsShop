using AutoMapper;
using BLL.Models;
using DAL.Entity;
using UI.Models;

namespace UI.Utils
{
    public class AutomapperConfiguration : Profile
    {
        public AutomapperConfiguration()
        {
            CreateMap<Component, ComponentDTO>()
                .ForMember(x => x.Type, s => s.MapFrom(z => z.Type.Name))
                .ForMember(x => x.Producer, s => s.MapFrom(z => z.Producer.Name));

            CreateMap<ComponentDTO, Component>()
                .ForMember(x => x.Type, s => s.MapFrom(z => new Type { Name = z.Type }))
                .ForMember(x => x.Producer, s => s.MapFrom(z => new Producer { Name = z.Producer }));

            CreateMap<ComponentDTO, ComponentViewModel>();
            CreateMap<ComponentViewModel, ComponentDTO>();
        }
    }
}