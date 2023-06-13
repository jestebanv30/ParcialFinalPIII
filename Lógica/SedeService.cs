using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lógica
{
    public class SedeService
    {
        SedesRepository sedesRepository;

        public SedeService(string conexion)
        {
            sedesRepository = new SedesRepository(conexion);
        }

        public List<Sedes> GetAll()
        {
            return sedesRepository.GetAll();
        }
    }
}
