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

        public Material(string identificador, string titulo, DateTime fechaRegistro, int cantidadRegistrada, int cantidadActual)
        {
            Identificador = identificador;
            Titulo = titulo;
            FechaRegistro = fechaRegistro;
            CantidadRegistrada = cantidadRegistrada;
            CantidadActual = cantidadActual;
        }

        public bool PrestarMaterial()
        {
            if (CantidadActual > 0)
            {
                CantidadActual--;
                return true;
            }
            return false;
        }

        public void DevolverMaterial()
        {
            if (CantidadActual < CantidadRegistrada)
            {
                CantidadActual++;
            }
        }

        public void IncrementarCantidad(int cantidad)
        {
            CantidadRegistrada += cantidad;
            CantidadActual += cantidad;
        }
    }

    public class Persona
    {
        public string Nombre { get; set; }
        public int Cedula { get; set; }
        public rol Roles { get; set; }
        public int CantidadPrestada { get; set; } 

        public Persona(string nombre, int cedula, rol role)
        {
            Nombre = nombre;
            Cedula = cedula;
            Roles = role;
            CantidadPrestada = 0; 
        }

        public bool PuedePedirMaterial()
        {
            switch (Roles)
            {
                case rol.estudiante:
                    return CantidadPrestada < 5;
                case rol.profesor:
                    return CantidadPrestada < 3;
                case rol.administrativo:
                    return CantidadPrestada < 1;
                default:
                    return false;
            }
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

        public void RegistrarMaterial(Material material)
        {
            Materials.Add(material);
        }

        public void RegistrarPersona(Persona persona)
        {
            if (!Personas.Any(p => p.Cedula == persona.Cedula))
            {
                Personas.Add(persona);
            }
            else
            {
                Console.WriteLine("Error: Ya existe una persona con esa cédula.");
            }
        }

        public void EliminarPersona(int cedula)
        {
            var persona = Personas.FirstOrDefault(p => p.Cedula == cedula);
            if (persona != null && persona.CantidadPrestada == 0)
            {
                Personas.Remove(persona);
            }
            else
            {
                Console.WriteLine("Error: No se puede eliminar la persona, tiene materiales prestados.");
            }
        }

        public void RegistrarPrestamo(int cedulaPersona, string identificadorMaterial)
        {
            var persona = Personas.FirstOrDefault(p => p.Cedula == cedulaPersona);
            var material = Materials.FirstOrDefault(m => m.Identificador == identificadorMaterial);

            if (persona == null || material == null)
            {
                Console.WriteLine("Error: Persona o material no encontrado.");
                return;
            }

            if (persona.PuedePedirMaterial() && material.PrestarMaterial())
            {
                persona.CantidadPrestada++;
                Movimientos.Add(new Movimiento(material, persona, DateTime.Now, Movimiento.tipo.valorPrestamo));
                Console.WriteLine($"Préstamo registrado: {persona.Nombre} ha pedido {material.Titulo}.");
            }
            else
            {
                Console.WriteLine("No se puede realizar el préstamo. Verifique el límite de materiales o la disponibilidad.");
            }
        }

        public void RegistrarDevolucion(int cedulaPersona, string identificadorMaterial)
        {
            var persona = Personas.FirstOrDefault(p => p.Cedula == cedulaPersona);
            var material = Materials.FirstOrDefault(m => m.Identificador == identificadorMaterial);

            if (persona == null || material == null)
            {
                Console.WriteLine("Error: Persona o material no encontrado.");
                return;
            }

            if (persona.CantidadPrestada > 0)
            {
                persona.CantidadPrestada--;
                material.DevolverMaterial();
                Movimientos.Add(new Movimiento(material, persona, DateTime.Now, Movimiento.tipo.valorDevolucion));
                Console.WriteLine($"Devolución registrada: {persona.Nombre} ha devuelto {material.Titulo}.");
            }
            else
            {
                Console.WriteLine("Error: La persona no tiene materiales prestados.");
            }
        }

        public void ConsultarHistorial()
        {
            foreach (var movimiento in Movimientos)
            {
                Console.WriteLine($"Fecha: {movimiento.FechaMovimiento}, Persona: {movimiento.Persona.Nombre}, Material: {movimiento.Material.Titulo}, Tipo: {movimiento.Tip}");
            }
        }
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
