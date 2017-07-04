using System;
using System.Collections.Generic;
using System.Linq;

namespace OrmComparison.LinqToSql
{
    public class LinqToSqlDataAccess : IDataAccess
    {
        public string Name => "LinqToSql";

        public Dto SelectOne(int id)
        {
            using (var context = new DataClasses1DataContext())
            {
                var result = context.GetSingleRecord(id).First();
                return new Dto()
                {
                    BoolData = result.BoolData,
                    DateData = result.DateData,
                    GuidData = result.GuidData,
                    IntData = result.IntData,
                    StringData = result.StringData
                };
            }
        }

        public List<Dto> SelectAll()
        {
            using (var context = new DataClasses1DataContext())
            {
                var results = context.GetAllRecords();
                return results.Select(result => new Dto()
                {
                    BoolData = result.BoolData,
                    DateData = result.DateData,
                    GuidData = result.GuidData,
                    IntData = result.IntData,
                    StringData = result.StringData
                }).ToList();
            }
        }
        
        public int RunNonQuery()
        {
            using (var context = new DataClasses1DataContext())
            {
                int? retval = 0;
                var result = context.ExecuteNonQuery(
                    ref retval,
                    1,
                    Guid.NewGuid(), 
                    "string",
                    true,
                    DateTime.Now);
                return retval.Value;
            }
        }
    }
}