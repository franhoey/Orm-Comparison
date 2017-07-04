using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace OrmComparison.Dapper
{
    public class DapperDataAccess : IDataAccess
    {
        public string Name => "Dapper";

        public Dto SelectOne(int id)
        {
            return
                GetConnection()
                    .Query<Dto>("GetSingleRecord", new {IntData = id}, commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
        }

        public List<Dto> SelectAll()
        {
            return
                GetConnection()
                    .Query<Dto>("GetAllRecords", commandType: CommandType.StoredProcedure)
                    .ToList();
        }

        public int RunNonQuery()
        {
            var parameters = new DynamicParameters(new
            {
                IntData = 1,
                GuidData = Guid.NewGuid(),
                StringData = "string",
                BoolData = true,
                DateData = DateTime.Now
            });

            parameters.Add("OutputData", dbType:DbType.Int32, direction:ParameterDirection.Output);

            GetConnection().Execute("ExecuteNonQuery", parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("OutputData");
        }


        private SqlConnection GetConnection()
            => new SqlConnection(ConfigurationManager.ConnectionStrings["TestData"].ConnectionString);
    }
}