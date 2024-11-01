using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace herramientas
{
    using System;

    public class Material
    {
        public string Identificador;
        public string Titulo;
        public DateTime FechaRegistro;
        public int CantidadRegistrada;

        public Material(string identificador, string titulo, DateTime fechaRegistro, int cantidadRegistrada)
        {
            Identificador = identificador;
            Titulo = titulo;
            FechaRegistro = fechaRegistro;
            CantidadRegistrada = cantidadRegistrada;
        }
    }

}
