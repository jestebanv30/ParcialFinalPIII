using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lógica
{
    public class VentaService 
    {
        VentasRepository ventasRepository;
        public VentaService(string conexion)
        {
            ventasRepository = new VentasRepository(conexion);
        }

        public string Insert(Ventas ventas)
        {
            return ventasRepository.InsertarVentas(ventas);
        }

        public void CargarVentas(string rutaArchivo)
        {
            ventasRepository.CargarVentas(rutaArchivo);
        }
    }
}
