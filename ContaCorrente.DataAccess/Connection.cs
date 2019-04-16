using System.Data.SqlClient;

namespace ContaCorrente.DataAccess
{
    public abstract class Connection
    {
        public SqlConnection sqlConn = null;

        public Connection()
        {
            sqlConn = new SqlConnection("Data Source=db.mic-br.com;Initial Catalog=teste;Persist Security Info=True;User ID=cognizant;Password=cognizant;");
        }
    }
}
