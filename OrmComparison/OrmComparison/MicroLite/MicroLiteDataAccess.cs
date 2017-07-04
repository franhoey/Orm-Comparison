using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MicroLite;
using MicroLite.Configuration;

namespace OrmComparison.MicroLite
{
    public class MicroLiteDataAccess : IDataAccess
    {
        private static readonly ISessionFactory _sessionFactory = Configure
            .Fluently()
            .ForMsSql2012Connection("TestData")
            .CreateSessionFactory();

        public string Name => "MicroLite";
        public Dto SelectOne(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Single<Dto>(new SqlQuery("exec GetSingleRecord @IntData", id));
            }
        }

        public List<Dto> SelectAll()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Fetch<Dto>(new SqlQuery("exec GetAllRecords")).ToList();
            }
        }

        public int RunNonQuery()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                int outputParam = 0;
                session.Single<Dto>(new SqlQuery("exec ExecuteNonQuery @OutputData OUTPUT, @IntData, @GuidData, @StringData, @BoolData, @DateData", 
                    outputParam,
                    1,
                    Guid.NewGuid(),
                    "string",
                    true,
                    DateTime.Now));
                //THIS DOESN'T WORK CANNOT GET THE OUTPUT
                return outputParam;
            }
        }
    }
}