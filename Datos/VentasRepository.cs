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

        // cargar ventas sin validaciones
        public void CargarVentas(string rutaArchivo)
        {
            List<Ventas> ventas = LeerArchivoVentas(rutaArchivo);

            foreach (var venta in ventas)
            {
                InsertarVentas(venta);
            }
        }



        //public void CargarVentas(string rutaArchivo)
        //{
        //    List<Ventas> ventas = LeerArchivoVentas(rutaArchivo);

        //    // Obtener la lista de productos existentes en la base de datos
        //    List<Productos> productosBD = new ProductosRepository(this.connection.ConnectionString).GetAll();

        //    // Verificar que la sede seleccionada coincida con la sede encontrada en el archivo
        //    //if (!ventas.Any(v => v.Sedes.Id_sede == idSede))
        //    //{
        //    //    throw new Exception("La sede seleccionada no coincide con la sede encontrada en el archivo.");
        //    //}

        //    // Validar y registrar nuevos productos en la base de datos
        //    ProductosRepository productosRepository = new ProductosRepository(this.connection.ConnectionString);
        //    foreach (var venta in ventas)
        //    {
        //        if (!productosBD.Any(p => p.Codigo_producto == venta.Productos.Codigo_producto))
        //        {
        //            // Nuevo producto encontrado, registrarlo en la base de datos
        //            Productos nuevoProducto = new Productos
        //            {
        //                Codigo_producto = venta.Productos.Codigo_producto,
        //                Nombre_producto = venta.Productos.Nombre_producto,
        //                Valor = venta.Productos.Valor
        //            };
        //            productosRepository.InsertarProductos(nuevoProducto);

        //            productosBD.Add(nuevoProducto);
        //        }
        //    }

        //    List<Ventas> ventasValidas = ventas.Where(v => productosBD.Any(p => p.Codigo_producto == v.Productos.Codigo_producto && p.Valor == v.Valor)).ToList();

        //    foreach (var ventaValida in ventasValidas)
        //    {
        //        InsertarVentas(ventaValida);
        //    }
        //}

        //private List<Ventas> LeerArchivoVentas(string rutaArchivo)
        //{
        //    List<Ventas> ventas = new List<Ventas>();

        //    using (StreamReader sr = new StreamReader(rutaArchivo))
        //    {
        //        string linea;
        //        while ((linea = sr.ReadLine()) != null)
        //        {
        //            string[] datos = linea.Split(';');
        //            if (datos.Length == 3)
        //            {
        //                string idSede = datos[0].Trim();
        //                string codigoProducto = datos[1].Trim();
        //                double valor = double.Parse(datos[2].Trim());

        //                ventas.Add(new Ventas() 
        //                { 
        //                    Sedes = new Sedes() 
        //                    { 
        //                        Id_sede = idSede 
        //                    }, 
        //                    Productos = new Productos()
        //                    {
        //                        Codigo_producto = codigoProducto,
        //                    }, 
        //                    Valor = valor 
        //                });
        //            }
        //        }
        //    }

        //    return ventas;
        //}
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
