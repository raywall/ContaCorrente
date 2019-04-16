using System.Configuration;
using System.Data.SqlClient;

namespace ContaCorrente.DataAccess
{
    public abstract class Connection
    {
        public SqlConnection sqlConn = null;

        public Connection()
        {
            if (sqlConn == null)
                sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MSSQL_DB"].ConnectionString);
        }
    }
}
