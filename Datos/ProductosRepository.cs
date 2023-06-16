using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ProductosRepository : ConnectionManager
    {
        public ProductosRepository(string connectionString) : base(connectionString)
        {
        }

        public List<Productos> GetAll()
        {
            List<Productos> productos = new List<Productos>();

            var comando = connection.CreateCommand();
            comando.CommandText = "Select codigo_producto, valor FROM productos";
            Open();
            OracleDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                productos.Add(MapperToProductos(lector));
            }
            Close();
            return productos;
        }

        private Productos MapperToProductos(OracleDataReader oracleDataReader)
        {
            if (!oracleDataReader.HasRows) return null;

            Productos productos = new Productos();
            productos.Codigo_producto = oracleDataReader.GetString(0);
            productos.Valor = Convert.ToDouble(oracleDataReader.GetDouble(1));

            return productos;
        }

        public string InsertarProductos(Productos productos)
        {
            using (var comando = connection.CreateCommand())
            {
                comando.CommandText = "InsertarProducto";
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("p_codigo_producto", OracleType.VarChar).Value = productos.Codigo_producto;
                comando.Parameters.Add("p_valor", OracleType.Number).Value = productos.Valor;

                Open();
                comando.ExecuteNonQuery();
                Close();
            }
            return "Nuevo producto insertado correctamente";
        }
    }
}
