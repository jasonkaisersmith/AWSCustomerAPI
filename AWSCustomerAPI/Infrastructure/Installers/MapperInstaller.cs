using AWSCustomerAPI.Commands;
using AWSCustomerAPI.Contracts.V1.Requests;
using AWSCustomerAPI.Contracts.V1.Responses;
using AWSCustomerAPI.Models;
using Mapster;
using MapsterMapper;
using System.Reflection;

namespace AWSCustomerAPI.Installers
{
    public class MapperInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var config = new TypeAdapterConfig()
            {
                AllowImplicitDestinationInheritance = true,
                AllowImplicitSourceInheritance = true,
            };


            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
        }
    }
}
