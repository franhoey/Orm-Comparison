using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OrmComparison.EntityFrameworkCore
{
    public class EFCoreDataAccess : IDataAccess
    {
        public string Name => "EntityFrameworkCore";

        public Dto SelectOne(int id)
        {
            using (var context = new OrmtestContext())
            {
                var result = context.TestData.FromSql("GetSingleRecord @p0", id).First();
                return new Dto()
                {
                    BoolData = result.BoolData,
                    DateData = result.DateData,
                    IntData = result.IntData,
                    GuidData = result.GuidData,
                    StringData = result.StringData
                };
            }
        }

        public List<Dto> SelectAll()
        {
            using (var context = new OrmtestContext())
            {
                var results = context.TestData.FromSql("GetAllRecords");
                return results.Select(result => new Dto()
                {
                    BoolData = result.BoolData,
                    DateData = result.DateData,
                    IntData = result.IntData,
                    GuidData = result.GuidData,
                    StringData = result.StringData
                }).ToList();
            }
        }

        public int RunNonQuery()
        {
            using (var context = new OrmtestContext())
            {
                var outputParam = new SqlParameter("OutputData", SqlDbType.Int);
                outputParam.Direction = ParameterDirection.Output;
                context.Database.ExecuteSqlCommand("ExecuteNonQuery @OutputData OUT, @IntData, @GuidData, @StringData, @BoolData, @DateData", 
                    outputParam, 
                    new SqlParameter("IntData", 1),
                    new SqlParameter("GuidData", Guid.NewGuid()), 
                    new SqlParameter("StringData", "string"), 
                    new SqlParameter("BoolData", true), 
                    new SqlParameter("DateData", DateTime.Now));

                return (int)outputParam.Value;
            }
        }
    }
}