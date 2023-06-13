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
            comando.CommandText = "Select * FROM productos";
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
            productos.Nombre_producto = oracleDataReader.GetString(1);
            productos.Valor = Convert.ToDouble(oracleDataReader.GetString(2));

            return productos;
        }

        public string InsertarProductos(Productos productos)
        {
            using (var comando = connection.CreateCommand())
            {
                comando.CommandText = "InsertarProducto";
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("p_codigoProducto", OracleType.VarChar).Value = productos.Codigo_producto;
                comando.Parameters.Add("p_nombreProducto", OracleType.VarChar).Value = productos.Nombre_producto;
                comando.Parameters.Add("p_valor", OracleType.Number).Value = productos.Valor;

                Open();
                comando.ExecuteNonQuery();
                Close();
            }
            return "Nuevo producto insertado correctamente";
        }
    }
}
