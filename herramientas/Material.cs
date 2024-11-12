using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Herramientas
{
    public class Material
    {
        public string Identificador { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int CantidadRegistrada { get; set; }
        public int CantidadActual { get; set; }

        public Persona Persona { get; set; }

        public Material(string identificador, string titulo, DateTime fechaRegistro, int cantidadRegistrada, int cantidadActual)
        {
            Identificador = identificador;
            Titulo = titulo;
            FechaRegistro = fechaRegistro;
            CantidadRegistrada = cantidadRegistrada;
            CantidadActual = cantidadActual;
        }
    }

    public class Persona
    {
        public string Nombre { get; set; }
        public int Cedula { get; set; }
        public rol Roles { get; set; }

        public Persona(string nombre, int cedula, rol role)
        {
            Nombre = nombre;
            Cedula = cedula;
            Roles = role;
        }

        public enum rol { estudiante, profesor, administrativo }
    }

    public class Movimiento
    {
        public Material Material { get; set; }
        public Persona Persona { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public tipo Tip { get; set; }

        public Movimiento(Material material, Persona persona, DateTime fechaMovimiento, tipo tip)
        {
            Material = material;
            Persona = persona;
            FechaMovimiento = fechaMovimiento;
            Tip = tip;
        }

        public enum tipo { valorPrestamo, valorDevolucion }
    }

    public class Biblioteca
    {
        public List<Material> Materials { get; set; } = new List<Material>();
        public List<Persona> Personas { get; set; } = new List<Persona>();
        public List<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
    }

    public static class MaterialService
    {
        public static List<Material> ObtenerMateriales()
        {
            List<Material> materiales = new List<Material>
            {
                new Material("001", "Material 1", DateTime.Now, 5, 5),
                new Material("002", "Material 2", DateTime.Now, 10, 10),
                new Material("003", "Material 3", DateTime.Now, 3, 3)
            };

            return materiales;
        }
    }
}
