using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;

namespace OrmComparison.EntityFramework
{
    public class EFDataAccess : IDataAccess
    {
        public string Name => "EntityFramework";
        public Dto SelectOne(int id)
        {
            using (var context = new ORMTestEntities())
            {
                var result = context.GetSingleRecord(id).First();
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
            using (var context = new ORMTestEntities())
            {
                var results = context.GetAllRecords();
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
            using (var context = new ORMTestEntities())
            {
                var outputParam = new ObjectParameter("OutputData", SqlDbType.Int);
                context.ExecuteNonQuery(outputParam, 1, Guid.NewGuid(), "string", true, DateTime.Now);

                return (int) outputParam.Value;
            }
        }
    }
}