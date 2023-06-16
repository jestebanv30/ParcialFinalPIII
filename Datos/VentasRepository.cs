using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.IO;

namespace Datos
{
    public class VentasRepository : ConnectionManager
    {
        public VentasRepository(string connectionString) : base(connectionString)
        {
        }
        public List<Ventas> GetAll()
        {
            List<Ventas> ventas = new List<Ventas>();

            var comando = connection.CreateCommand();
            comando.CommandText = "SELECT * FROM ventas";
            Open();
            OracleDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                ventas.Add(MapperToVentas(lector));
            }
            Close();
            return ventas;
        }

        private Ventas MapperToVentas(OracleDataReader oracleDataReader)
        {
            if (!oracleDataReader.HasRows) return null;

            Ventas ventas = new Ventas();
            ventas.Sedes.Id_sede = oracleDataReader.GetString(0);
            ventas.Productos.Codigo_producto = oracleDataReader.GetString(1);
            ventas.Valor = oracleDataReader.GetDouble(2);

            return ventas;
        }
        public string InsertarVentas(Ventas ventas)
        {
            using (var comando = connection.CreateCommand())
            {
                comando.CommandText = "InsertarVenta";
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("p_codigo_sede", OracleType.NVarChar).Value = ventas.Sedes.Id_sede;
                comando.Parameters.Add("p_codigo_producto", OracleType.NVarChar).Value = ventas.Productos.Codigo_producto;
                comando.Parameters.Add("p_valor", OracleType.Number).Value = Convert.ToDecimal(ventas.Valor);

                Open();
                comando.ExecuteNonQuery();
                Close();
            }
            return "Venta insertada correctamente";
        }

        public void CargarVentasValidando(string rutaArchivo, string idSede)
        {
            List<Ventas> ventas = LeerArchivoVentas(rutaArchivo);

            List<Productos> productos = new ProductosRepository(this.connection.ConnectionString).GetAll();

            ProductosRepository productosRepository = new ProductosRepository(this.connection.ConnectionString);
            foreach (var venta in ventas)
            {
                if (!productos.Any(p => p.Codigo_producto == venta.Productos.Codigo_producto))
                {
                    Productos nuevoProducto = new Productos
                    {
                        Codigo_producto = venta.Productos.Codigo_producto,
                        Valor = venta.Valor
                    };
                    productosRepository.InsertarProductos(nuevoProducto);

                    productos.Add(nuevoProducto);
                }
            }

            List<Ventas> ventasSedeDeseada = ventas.Where(v => v.Sedes.Id_sede == idSede).ToList();

            foreach (var venta in ventasSedeDeseada)
            {
                InsertarVentas(venta);
            }
        }

        public void CargarVentas(string rutaArchivo)
        {
            List<Ventas> ventas = LeerArchivoVentas(rutaArchivo);

            List<Productos> productos = new ProductosRepository(this.connection.ConnectionString).GetAll();

            ProductosRepository productosRepository = new ProductosRepository(this.connection.ConnectionString);
            foreach (var venta in ventas)
            {
                if (!productos.Any(p => p.Codigo_producto == venta.Productos.Codigo_producto))
                {
                    Productos nuevoProducto = new Productos
                    {
                        Codigo_producto = venta.Productos.Codigo_producto,
                        Valor = venta.Valor
                    };
                    productosRepository.InsertarProductos(nuevoProducto);

                    productos.Add(nuevoProducto);
                }
            }
            foreach (var venta in ventas)
            {
                InsertarVentas(venta);
            }
        }

        public Ventas MapeadorVentas(String linea)
        {
            string[] aux = linea.Split(';');
            Ventas ventas = new Ventas()
            {
                Sedes = new Sedes()
                {
                    Id_sede = aux[0],
                },
                Productos = new Productos()
                {
                    Codigo_producto = aux[1],
                },
                Valor = Convert.ToDouble(aux[2]),
            };

            return ventas;
        }

        public List<Ventas> LeerArchivoVentas(string ruta)
        {
            try
            {
                StreamReader sr = new StreamReader(ruta);
                List<Ventas> ventas = new List<Ventas>();
                while (!sr.EndOfStream)
                {
                    ventas.Add(MapeadorVentas(sr.ReadLine()));
                }
                sr.Close();
                return ventas;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
