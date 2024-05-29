using AWSCustomerAPI.PipelineBehaviours;
using MediatR;
using MediatR.Pipeline;
using System;

namespace AWSCustomerAPI.Installers
{
    public class MediatorInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            /*
            services.AddMediatR(typeof(Startup));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionProcessorBehavior<,>));
            */

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<Startup>();
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>), ServiceLifetime.Transient);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestExceptionProcessorBehavior<,>), ServiceLifetime.Transient);
            });

        }
    }
}
