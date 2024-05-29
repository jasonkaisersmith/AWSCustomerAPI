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

namespace AWSCustomerAPI.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
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
                DurationSeconds = 3600,
                RoleSessionName = "Session1",
                RoleArn = roleArnToAssume
            };

            //Task.Delay(5); //Wait for it to take effect??
            var assumeRoleRes = stsClient.AssumeRoleAsync(assumeRoleReq).Result;

/*
            // Now create a new client based on the credentials of the caller assuming the role.
            var client2 = new AmazonSecurityTokenServiceClient(credentials: assumeRoleRes.Credentials);

            // Get and display information about the caller that has assumed the defined role.
            var caller2 = client2.GetCallerIdentityAsync(callerIdRequest).Result;
            Console.WriteLine($"AssumedRole Caller: {caller2.Arn}");
*/

            var config = new AmazonDynamoDBConfig()
            {
                RegionEndpoint = RegionEndpoint.USEast1,
                AllowAutoRedirect = true          
                
            };

            var client = new AmazonDynamoDBClient(assumeRoleRes.Credentials, config);

            //Working
            //var client = new AmazonDynamoDBClient(LoadSsoCredentials(), config);

            //Not working
            //var client = new AmazonDynamoDBClient(config);
            //var client = new AmazonDynamoDBClient(AWS_ACCESS_KEY_ID, AWS_SECRET_ACCESS_KEY);


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
            if (!chain.TryGetAWSCredentials("319941658928_SSO-Consumer-admin", out var credentials))
                throw new Exception("Failed to find the profile");

            return credentials;
        }
    }
}
