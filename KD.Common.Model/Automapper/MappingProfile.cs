using AutoMapper;
using KD.Common.Model.Models;
using KD.Core.DomainModels;

namespace KD.Common.Model.Automapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>()
               .ForMember(s => s.Stock,d=>d.Ignore());



            CreateMap<Stock, StockModel>()
                .ForMember(s => s.Product, d => d.Ignore());
            CreateMap<StockModel, Stock>();
                

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
        }
    }
}
