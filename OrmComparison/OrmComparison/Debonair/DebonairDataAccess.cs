using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Debonair.Data;

namespace OrmComparison.Debonair
{
    public class DebonairDataAccess : IDataAccess
    {
        public string Name => "Debonair";

        public Dto SelectOne(int id)
        {
            return GetRepo().ExecuteStoredProcedure("GetSingleRecord", new {IntData = id}).First();
        }

        public List<Dto> SelectAll()
        {
            return GetRepo().ExecuteStoredProcedure("GetAllRecords").ToList();
        }

        public int RunNonQuery()
        {
            GetRepo().ExecuteStoredProcedure("ExecuteNonQuery", new
            {
                OutputData = 1,
                IntData = 1,
                GuidData = Guid.NewGuid(),
                StringData = "string",
                BoolData = true,
                DateData = DateTime.Now
            });
            //THIS DOESN'T WORK
            return 1;
        }

        private DataRepository<Dto> GetRepo()
            => new DataRepository<Dto>(new SqlConnection(ConfigurationManager.ConnectionStrings["TestData"].ConnectionString));
    }
}