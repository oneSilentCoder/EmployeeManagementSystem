using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeAPP.Helper
{
    public class SqlManager
    {
        SqlConnection Connection;
        SqlDataAdapter SqlAdapter;
        SqlCommand Command;
        string ConnectionString;
        public IConfiguration Configuration { get; }
        public SqlManager(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration["ConnectionStrings:MSSQLConnectionString"];
        }

        public DataTable GetTable(string Query, CommandType cmdType)
        {
            try
            {
                DataTable Dtable = new DataTable();
                using (Connection = new SqlConnection(ConnectionString))
                {
                    using (Command = new SqlCommand(Query,Connection))
                    {
                        Command.CommandType = cmdType;
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

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }
    }
}
