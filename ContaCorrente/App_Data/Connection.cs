using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace ContaCorrente.Extensions
{
    public abstract class Connection
    {
        private string dataBaseLocation = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/db.mdf");
        public SqlConnection sqlConn = null;

        public Connection()
        {
            sqlConn = new SqlConnection(string.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={0};Integrated Security=True;Connect Timeout=30", dataBaseLocation));
        }
    }
}