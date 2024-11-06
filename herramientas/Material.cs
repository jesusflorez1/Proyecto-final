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
        private string identificador;
        private string titulo;
        private DateTime fechaRegistro;
        private int cantidadRegistrada;
        private int cantidadActual;

        public string Identificador { get => identificador; set => identificador = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public DateTime FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
        public int CantidadRegistrada { get => cantidadRegistrada; set => cantidadRegistrada = value; }
        public int CantidadActual { get => cantidadActual; set => cantidadActual = value; }

        public Material(string identificador, string titulo, DateTime fechaRegistro, int cantidadRegistrada, int cantidadActual)
        {
            this.Identificador = identificador;
            this.Titulo = titulo;
            this.FechaRegistro = fechaRegistro;
            this.CantidadRegistrada = cantidadRegistrada;
            this.CantidadActual = cantidadActual;
            
        }

        public class Persona
        {
            private string nombre;
            private int cedula;
            private rol roles;

            public Persona(string nombre, int cedula, rol role)
            {
                this.Nombre = nombre;
                this.Cedula = cedula;
                this.Roles = role;
                
            }

            public string Nombre { get => nombre; set => nombre = value; }
            public int Cedula { get => cedula; set => cedula = value; }
            public rol Roles { get => roles; set => roles = value; }

            public enum rol { estudiante, profesor, administrativo }

        }

        public class Movimiento
        {
            private Material material;
            private Persona persona;
            private DateTime fechaMovimiento;
            private tipo tip;

            public Movimiento(Material material, Persona persona, DateTime fechaMovimiento, tipo tip)
            {
                this.Material = material;
                this.Persona = persona;
                this.FechaMovimiento = fechaMovimiento;
                this.Tip = tip;
            }

            public Material Material { get => material; set => material = value; }
            public Persona Persona { get => persona; set => persona = value; }
            public DateTime FechaMovimiento { get => fechaMovimiento; set => fechaMovimiento = value; }
            public tipo Tip { get => tip; set => tip = value; }

            public enum tipo { valorPrestamo,valorDevolucion}
            
        }

        public class Biblioteca
        {

            public List<Material> materials = new List<Material>();
            public List<Persona> personas = new List<Persona>();
            public List<Movimiento> movimientos = new List<Movimiento>();

        }

    }

}
