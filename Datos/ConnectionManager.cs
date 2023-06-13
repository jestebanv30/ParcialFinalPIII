using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ConnectionManager
    {
        protected OracleConnection connection;

        public ConnectionManager(string connectionString)
        {
            connection = new OracleConnection(connectionString);
        }

        public void Open()
        {
            connection.Open();
        }
        public void Close()
        {
            connection.Close();
        }
    }
}
