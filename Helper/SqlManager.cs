using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeAPP.Helper
{
    public class SQLManager
    {
        SqlConnection Connection;
        SqlDataAdapter SqlAdapter;
        SqlCommand Command;
        string ConnectionString;
       // public IConfiguration Configuration { get; }
        public SQLManager(IConfiguration Configuration)
        {
           // Configuration = configuration;
            ConnectionString = Configuration["ConnectionStrings:MSSQLConnectionString"];
        }

        public DataTable GetTable(string Query, List<SqlParameter> SqlParameters)
        {
            try
            {
                DataTable Dtable = new DataTable();
                using (Connection = new SqlConnection(ConnectionString))
                {
                    using (Command = new SqlCommand(Query,Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        if(SqlParameters != null)
                        {
                            foreach (SqlParameter p in SqlParameters)
                            {
                                Command.Parameters.Add(p);
                            }
                        }
                        using (SqlAdapter = new SqlDataAdapter(Command))
                        {
                            SqlAdapter.Fill(Dtable);
                        }
                    }
                }
                return Dtable;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object Execute(string spName, DynamicParameters parameters)
        {
            try
            {
                using (Connection = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        Connection.Open();
                        return (Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
                    }
                    catch(Exception ex)
                    {
                        throw;
                    }
                    finally { Connection.Close(); }
                }
            }
            catch (Exception ex)
            {
                throw;
            }            
        }        
    }
}
