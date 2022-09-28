using AutoMapper;
using KD.Common.Model.Models;
using KD.Core.DomainModels;

namespace KD.Common.Model.Automapper
{
    public static class MappingExtensions
    {
        private static readonly IMapper mapper = AutoMapperConfiguration.Mapper;

        public static ProductModel ToModel(this Product entity)
        {
            return mapper.Map<ProductModel>(entity);
        }

        public static Product ToEntity(this ProductModel model)
        {
            return mapper.Map<Product>(model);
        }



        public static StockModel ToModel(this Stock entity)
        {
            return mapper.Map<StockModel>(entity);
        }

        public static Stock ToEntity(this StockModel model)
        {
            return mapper.Map<Stock>(model);
        }



        public static UserModel ToModel(this User entity)
        {
            return mapper.Map<UserModel>(entity);
        }

        public static User ToEntity(this UserModel model)
        {
            return mapper.Map<User>(model);
        }

    }
}
