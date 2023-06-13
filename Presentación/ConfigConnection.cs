using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentación
{
    public class ConfigConnection
    {
        public static string conexionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
    }
}
