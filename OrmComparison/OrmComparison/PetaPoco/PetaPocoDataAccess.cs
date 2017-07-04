using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PetaPoco;

namespace OrmComparison.PetaPoco
{
    public class PetaPocoDataAccess : IDataAccess
    {
        public string Name => "PetaPoco";
        public Dto SelectOne(int id)
        {
            using (var db = new Database("TestData") {EnableAutoSelect = false})
            {
                return db.FirstOrDefault<Dto>("exec GetSingleRecord @IntData", new
                {
                    IntData = id
                });
            }
        }

        public List<Dto> SelectAll()
        {
            using (var db = new Database("TestData") { EnableAutoSelect = false })
            {
                return db.Fetch<Dto>("exec GetAllRecords");
            }
        }

        public int RunNonQuery()
        {
            using (var db = new Database("TestData") { EnableAutoSelect = false })
            {
                var outputParam = new SqlParameter("@OutputData", SqlDbType.Int);
                outputParam.Direction = ParameterDirection.Output;
                
                db.Execute("exec ExecuteNonQuery @OutputData OUTPUT, @IntData, @GuidData, @StringData, @BoolData, @DateData", new
                {
                    OutputData = outputParam,
                    IntData = 1,
                    GuidData = Guid.NewGuid(),
                    StringData = "string",
                    BoolData = true,
                    DateData = DateTime.Now
                });

                return (int) outputParam.Value;
            }
        }
    }
}