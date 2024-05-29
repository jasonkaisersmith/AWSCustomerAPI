using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Newtonsoft.Json;

namespace AWSCustomerAPI.Models.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string? _tableName;
        private readonly IAmazonDynamoDB _dynamoDB;

        public CustomerRepository(IConfiguration configuration, IAmazonDynamoDB dynamoDB)
        {
            _tableName = configuration.GetValue<string>(Constants.AppSettings.CustomerDB_TableName) ??
                throw new ArgumentNullException(nameof(_tableName), "Unable to connect to Database due to null value.");

            _dynamoDB = dynamoDB;           
        }

        public async Task<bool> Create(Customer entity)
        {
            var customerAsJson = JsonConvert.SerializeObject(entity);
            var itemAsDocument = Document.FromJson(customerAsJson);
            var itemAsAttributes = itemAsDocument.ToAttributeMap();

            var createItemRequest = new PutItemRequest
            {
                TableName = _tableName,
                Item = itemAsAttributes
            };
            var response = await _dynamoDB.PutItemAsync(createItemRequest);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<bool> Delete(Guid id)
        {
            var deleteItemRequest = new DeleteItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
                 {
                     { "pk", new AttributeValue{S = id.ToString() } }
                     //{ "sk", new AttributeValue{S = id.ToString() } }
                 }

            };
            var response = await _dynamoDB.DeleteItemAsync(deleteItemRequest);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<Customer?> GetById(Guid id)
        {
            var getItemRequest = new GetItemRequest
            {
                TableName = _tableName,
                 Key = new Dictionary<string, AttributeValue>
                 {
                     { "pk", new AttributeValue{S = id.ToString() } }
                     //{ "sk", new AttributeValue{S = id.ToString() } }
                 }
                
            };
            var response = await _dynamoDB.GetItemAsync(getItemRequest);
            if (response.Item.Count == 0)
                return null;

            var itemAsDoucment = Document.FromAttributeMap(response.Item);
            var customer = JsonConvert.DeserializeObject<Customer>(itemAsDoucment.ToJson());
            return customer;
        }

        public async Task<bool> Update(Customer entity)
        {
            return await Create(entity);
        }
    }
}
