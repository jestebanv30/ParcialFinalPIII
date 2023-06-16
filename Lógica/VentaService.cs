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

        public List<Ventas> GetAll()
        {
            return ventasRepository.GetAll();
        }
        public void CargarVentasValidando(string rutaArchivo, string idSede) 
        {
            ventasRepository.CargarVentasValidando(rutaArchivo, idSede);
        }
        public void CargarVentas(string rutaArchivo) 
        {
            ventasRepository.CargarVentas(rutaArchivo);
        }
    }
}
