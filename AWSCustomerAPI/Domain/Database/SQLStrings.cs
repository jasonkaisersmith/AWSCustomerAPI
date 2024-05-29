using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSCustomerAPI.Database
{
    public static class SQLStrings
    {
        public static readonly string Schema = "AWSCustomerAPI";

        #region Customer
        public static readonly string AWSCustomerAPI_INSERT = $"INSERT INTO {Schema}.CustomerExample (Name, Status, StatusReason, " +
            "href, BaseType, SchemaLocation, Type, ValidForStartDateTime, ValidForEndDateTime) " +
               " VALUES (@Name, @Status, @StatusReason, @href, @BaseType, @SchemaLocation, @Type, @ValidForStartDateTime, @ValidForEndDateTime) returning Id;";

        public static readonly string AWSCustomerAPI_UPDATE = $"update {Schema}.CustomerExample set Name = @Name, Status = @Status, StatusReason = @StatusReason, " +
            "href = @href, BaseType = @BaseType, SchemaLocation = @SchemaLocation, Type = @Type, ValidForStartDateTime = @ValidForStartDateTime, " +
            "ValidForEndDateTime = @ValidForEndDateTime " +
            " where Id = @Id";


        public static readonly string AWSCustomerAPI_SELECT_IDs = $"Select * FROM {Schema}.CustomerExample WHERE Id = ANY(@Ids)";

        public static readonly string AWSCustomerAPI_DELETE = $"delete from {Schema}.CustomerExample where Id = @Id";
        public static readonly string AWSCustomerAPI_SELECT_ALL = $"select * from {Schema}.CustomerExample";
        public static readonly string AWSCustomerAPI_SELECT_ID = $"select * from {Schema}.CustomerExample where Id = @Id";
        #endregion

    }
}
