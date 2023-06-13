using Entidades;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class SedesRepository : ConnectionManager
    {
        public SedesRepository(string connectionString) : base(connectionString)
        {
        }

        public List<Sedes> GetAll()
        {
            List<Sedes> sedes = new List<Sedes>();

            var comando = connection.CreateCommand();
            comando.CommandText = "Select * FROM sedes";
            Open();
            OracleDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                sedes.Add(MapperToSedes(lector));
            }
            Close();
            return sedes;
        }

        private Sedes MapperToSedes(OracleDataReader oracleDataReader)
        {
            if (!oracleDataReader.HasRows) return null;

            Sedes sedes = new Sedes();
            sedes.Id_sede = oracleDataReader.GetString(0);
            sedes.Nombre_sede = oracleDataReader.GetString(1);

            return sedes;
        }
    }
}
