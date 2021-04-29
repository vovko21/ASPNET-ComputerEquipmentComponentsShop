using AutoMapper;
using BLL.Models;
using DAL.Entity.Orders;
using DAL.Entity.StoreProducts;
using UI.Models;

namespace UI.Utils
{
    public class AutomapperConfiguration : Profile
    {
        public AutomapperConfiguration()
        {
            CreateMap<ComponentDB, ComponentDTO>()
                .ForMember(x => x.Type, s => s.MapFrom(z => z.Type.Name))
                .ForMember(x => x.Producer, s => s.MapFrom(z => z.Producer.Name));

            CreateMap<ComponentDTO, ComponentDB>()
                .ForMember(x => x.Type, s => s.MapFrom(z => new TypeDB { Name = z.Type }))
                .ForMember(x => x.Producer, s => s.MapFrom(z => new ProducerDB { Name = z.Producer }));

            CreateMap<ComponentDTO, ComponentViewModel>();
            CreateMap<ComponentViewModel, ComponentDTO>();

            //Order | OrderDTO
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, Order>();

            CreateMap<OrderItems, OrderItemDTO>();
            CreateMap<OrderItemDTO, OrderItems>();

            CreateMap<Client, ClientDTO>();
            CreateMap<ClientDTO, Client>();

            //OrderViewModel | OrderDTO
            CreateMap<OrderDTO, OrderViewModel>();
            CreateMap<OrderViewModel, OrderDTO>();

            CreateMap<OrderItemDTO, OrderItemViewModel>()
                .ForMember(x => x.Component, opt => opt.Ignore());
            //    .ReverseMap()
            //    .ForMember(d => d.ComponentDBId, opts => opts.MapFrom(x => x.Component.Id));
            CreateMap<OrderItemViewModel, OrderItemDTO>()
                .ForMember(x => x.ComponentDBId, opt => opt.MapFrom(x => x.Component.Id));
            //    .ReverseMap()
            //    .ForMember(d => d.Component, opts => opts.ConvertUsing<EntityConverter, int>(src => src.ComponentDBId));

            CreateMap<ClientDTO, ClientViewModel>();
            CreateMap<ClientViewModel, ClientDTO>();
        }
    }
}