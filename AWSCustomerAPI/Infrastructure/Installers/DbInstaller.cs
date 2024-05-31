using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.SecurityToken.Model;
using Amazon.SecurityToken;
using AWSCustomerAPI.Models.Repositories;
using Amazon.Runtime.CredentialManagement;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace AWSCustomerAPI.Installers
{
    public class DbInstaller : IInstaller
    {

        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var roleArnToAssume = "arn:aws:iam::319941658928:role/JasonRole_AppDynamoDbAccess";
            var ssoCredentials = LoadSsoCredentials();

            // Using AssumeRoleAWSCredentials to automatically refresh the credentials
            var assumeRoleCredentials = new AssumeRoleAWSCredentials(
                ssoCredentials,
                roleArnToAssume,
                "Session1"
            );

            var awsOptions = configuration.GetAWSOptions();
            awsOptions.Credentials = assumeRoleCredentials;
            awsOptions.Region = RegionEndpoint.USEast1;
            services.AddDefaultAWSOptions(awsOptions);

            var dbConfig = new AmazonDynamoDBConfig
            {
                RegionEndpoint = RegionEndpoint.USEast1,
                AllowAutoRedirect = true
            };

            var client = new AmazonDynamoDBClient(assumeRoleCredentials, dbConfig);

            services.AddSingleton<IAmazonDynamoDB>(client);
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });
        }

        static AWSCredentials LoadSsoCredentials()
        {
            var chain = new CredentialProfileStoreChain();
            if (chain.TryGetAWSCredentials("319941658928_SSO-Consumer-admin", out var credentials))
                return credentials;
            else
            {
                var defaultCreds = FallbackCredentialsFactory.GetCredentials();
                return defaultCreds ?? throw new Exception("Failed to find the profile");
            }
        }


        public void oldInstallServices(IServiceCollection services, IConfiguration configuration)
        {        
            var roleArnToAssume = "arn:aws:iam::319941658928:role/JasonRole_AppDynamoDbAccess";
            var stsClient = new AmazonSecurityTokenServiceClient(LoadSsoCredentials(), RegionEndpoint.EUWest1);

            /*
            // Get and display the information about the identity of the default user.
            var callerIdRequest = new GetCallerIdentityRequest();
            var caller = stsClient.GetCallerIdentityAsync(callerIdRequest).Result;
            Console.WriteLine($"Original Caller: {caller.Arn}");
            */
            // Create the request to use with the AssumeRoleAsync call.
            var assumeRoleReq = new AssumeRoleRequest()
            {
                DurationSeconds = 900,
                RoleSessionName = "Session1",
                RoleArn = roleArnToAssume
            };

            //var defaultCreds = FallbackCredentialsFactory.GetCredentials();
            //var assumeRoleRes = new AssumeRoleAWSCredentials(defaultCreds, roleArnToAssume, "Session1", new AssumeRoleAWSCredentialsOptions { DurationSeconds = 3600 });

            var assumeRoleRes = stsClient.AssumeRoleAsync(assumeRoleReq).Result;

            var awsOptions = configuration.GetAWSOptions();
            awsOptions.Credentials = assumeRoleRes.Credentials;
            awsOptions.SessionRoleArn = roleArnToAssume;
            awsOptions.SessionName = "Session1";
            awsOptions.Region = RegionEndpoint.USEast1;
            services.AddDefaultAWSOptions(awsOptions);

            /*
                        // Now create a new client based on the credentials of the caller assuming the role.
                        var client2 = new AmazonSecurityTokenServiceClient(credentials: assumeRoleRes.Credentials);

                        // Get and display information about the caller that has assumed the defined role.
                        var caller2 = client2.GetCallerIdentityAsync(callerIdRequest).Result;
                        Console.WriteLine($"AssumedRole Caller: {caller2.Arn}");
            */

            var dbConfig = new AmazonDynamoDBConfig()
            {
                RegionEndpoint = RegionEndpoint.USEast1,
                AllowAutoRedirect = true                 
            };

            var client = new AmazonDynamoDBClient(assumeRoleRes.Credentials, dbConfig);

            //var client = new AmazonDynamoDBClient(assumeRoleRes, config);

            //Working
            //var client = new AmazonDynamoDBClient(LoadSsoCredentials(), config);

            //Not working
            //var client = new AmazonDynamoDBClient(config);
            //var client = new AmazonDynamoDBClient(AWS_ACCESS_KEY_ID, AWS_SECRET_ACCESS_KEY);


            //services.AddSingleton<IAmazonDynamoDB>(client);
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });

        }

        static AWSCredentials oldLoadSsoCredentials()
        {
            var chain = new CredentialProfileStoreChain();
            if (chain.TryGetAWSCredentials("319941658928_SSO-Consumer-admin", out var credentials))
                return credentials;
            else
            {
                var defaultCreds = FallbackCredentialsFactory.GetCredentials();
                return defaultCreds == null ? throw new Exception("Failed to find the profile") : defaultCreds;
            }
        }
    }
}
