
using Newtonsoft.Json;

namespace AWSCustomerAPI.Models
{
    public class Customer : ICustomer
    {
        [JsonProperty("pk")]
        public string Pk => Id;

        [JsonProperty("sk")]
        public string Sk => Pk;

        [JsonProperty("id")]
        public string Id { get; set; } = String.Empty; //Guuid not supported in dynamo DB

        [JsonProperty("lastupdate")]
        public DateTime? LastUpdate { get; set; }

        [JsonProperty("firstname")]
        public string? Firstname { get; set; }

        [JsonProperty("surname")]
        public string? Surname { get; set; }

        public Customer()
        {

        }
    }
}
