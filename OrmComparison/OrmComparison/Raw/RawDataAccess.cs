using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace OrmComparison.Raw
{
    public class RawDataAccess : IDataAccess
    {
        public string Name => "RawDataAccess";

        public Dto SelectOne(int id)
        {
            using (var cmd = new SqlCommand("GetSingleRecord", GetConnection()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IntData", id);

                try
                {
                    cmd.Connection.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                            return CreateDtoFromCurrentReader(reader);

                        return null;
                    }
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
        }

        public List<Dto> SelectAll()
        {
            using (var cmd = new SqlCommand("GetAllRecords", GetConnection()))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    cmd.Connection.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        var data = new List<Dto>();
                        while (reader.Read())
                            data.Add(CreateDtoFromCurrentReader(reader));

                        return data;
                    }
                }
                finally
                {
                    cmd.Connection.Close();
                }
                
            }
        }

        public int RunNonQuery()
        {
            using (var cmd = new SqlCommand("ExecuteNonQuery", GetConnection()))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var outputParam = new SqlParameter("@OutputData", SqlDbType.Int);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);

                    cmd.Parameters.AddWithValue("@IntData", 1);
                    cmd.Parameters.AddWithValue("@GuidData", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("@StringData", "string");
                    cmd.Parameters.AddWithValue("@BoolData", true);
                    cmd.Parameters.AddWithValue("@DateData", DateTime.Now);

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();

                    return (int)outputParam.Value;
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
        }

        private Dto CreateDtoFromCurrentReader(SqlDataReader reader)
        {
            return new Dto()
            {
                BoolData = (bool) reader["BoolData"],
                DateData = (DateTime)reader["DateData"],
                GuidData = (Guid)reader["GuidData"],
                IntData = (int)reader["IntData"],
                StringData = (string)reader["StringData"]
            };
        }

        private SqlConnection GetConnection()
            => new SqlConnection(ConfigurationManager.ConnectionStrings["TestData"].ConnectionString);
    }
}