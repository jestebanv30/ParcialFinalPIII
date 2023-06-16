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

        // cargar ventas sin validaciones (Funcional)
        //public void CargarVentas(string rutaArchivo)
        //{
        //    List<Ventas> ventas = LeerArchivoVentas(rutaArchivo);

        //    foreach (var venta in ventas)
        //    {
        //        InsertarVentas(venta);
        //    }
        //}

        // Cargar ventas validando la que está seleccionada en el combobox, es decir, se carga solo las de sede norte si se selecciona
        //public void CargarVentas(string rutaArchivo, string idSedeDeseada)
        //{
        //    List<Ventas> ventas = LeerArchivoVentas(rutaArchivo);

        //    List<Ventas> ventasSedeDeseada = ventas.Where(v => v.Sedes.Id_sede == idSedeDeseada).ToList(); 

        //    foreach (var venta in ventasSedeDeseada)
        //    {
        //        InsertarVentas(venta);
        //    }
        //}

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
