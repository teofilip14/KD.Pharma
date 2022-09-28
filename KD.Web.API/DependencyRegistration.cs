using KD.Core.Data;
using FluentValidation;
using KD.Common.Model.Models;
using KD.Common.Model.Models.Validators;
using KD.Services.Stock;
using KD.Services.Product;
using KD.Services.User;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace KD.Web.API
{
    public class DependencyRegistration
    {
        /// <summary>
        /// Register services
        /// </summary>
        /// <param name="builder">
        /// </param>
        public void Register(IServiceCollection builder)
        {
            //Per request lifetime

            //Singleton services

            //Transient services

            builder.AddTransient(typeof(IRepository<>), typeof(EFCoreRepository<>));
            builder.AddTransient<IProductService, ProductService>();
            builder.AddTransient<IStockService, StockService>();
            builder.AddTransient<IUserService, UserService>();

            builder.AddTransient<IValidator<ProductModel>, ProductModelValidator>();
            builder.AddTransient<IValidator<StockModel>, StockModelValidator>();
            builder.AddTransient<IValidator<UserModel>, UserModelValidator>();
        }
    }
}