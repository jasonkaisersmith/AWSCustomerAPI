using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using AWSCustomerAPI.Models.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSCustomerAPI.Installers
{
    public class ServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {/*
            services.AddAWSService<IAmazonDynamoDB>(new AWSOptions
            {
                Credentials = new BasicAWSCredentials("ASIAUU7QCJEYEGWUPU7Y", "qToHjTsvgdT4d2VER0NyMKRhIlqgfrPIBTTZj0ok")
            });
            */
        }
    }
}
