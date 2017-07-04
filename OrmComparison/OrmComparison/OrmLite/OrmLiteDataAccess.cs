using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ServiceStack.OrmLite;

namespace OrmComparison.OrmLite
{
    public class OrmLiteDataAccess : IDataAccess
    {
        private static readonly OrmLiteConnectionFactory _dbFactory = new OrmLiteConnectionFactory(
            ConfigurationManager.ConnectionStrings["TestData"].ConnectionString,
            SqlServerDialect.Provider);

        public string Name => "ServiceStack.OrmLite";

        public Dto SelectOne(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                using (var cmd = db.SqlProc("GetSingleRecord", new { IntData = id }))
                {
                    return cmd.ConvertTo<Dto>();
                }
            }
        }

        public List<Dto> SelectAll()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                using (var cmd = db.SqlProc("GetAllRecords"))
                {
                    return cmd.ConvertToList<Dto>();
                }
            }
        }

        public int RunNonQuery()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                using (var cmd = db.SqlProc("ExecuteNonQuery", new
                    {
                        IntData = 1,
                        GuidData = Guid.NewGuid(),
                        StringData = "string",
                        BoolData = true,
                        DateData = DateTime.Now
                    }))
                {
                    var output = cmd.AddParam("OutputData", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    cmd.ExecuteNonQuery();

                    return (int)output.Value;
                }
            }
        }
    }
}