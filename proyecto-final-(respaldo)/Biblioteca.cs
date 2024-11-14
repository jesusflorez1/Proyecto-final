﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyecto_final__respaldo_
{
    internal class Biblioteca
    {
        public class Material
        {
            private string identificador;
            private string titulo;
            private DateTime fecharegistro;
            private int cantidad_registrada;
            private int cantidad_actual;

            public string Identificador { get => identificador; set => identificador = value; }
            public string Titulo { get => titulo; set => titulo = value; }
            public DateTime Fecharegistro { get => fecharegistro; set => fecharegistro = value; }
            public int Cantidad_registrada { get => cantidad_registrada; set => cantidad_registrada = value; }
            public int Cantidad_actual { get => cantidad_actual; set => cantidad_actual = value; }

            public Material(string identificador, string titulo, DateTime fecharegistro, int cantidad_registrada, int cantidad_actual)
            {
                this.Identificador = identificador;
                this.Titulo = titulo;
                this.Fecharegistro = fecharegistro;
                this.Cantidad_registrada = cantidad_registrada;
                this.Cantidad_actual = cantidad_actual;
            }
            
        }
        public class Persona
        {
            private string nombre;
            private int cedula;
            private rol roles;

            public string Nombre { get => nombre; set => nombre = value; }
            public int Cedula { get => cedula; set => cedula = value; }
            internal rol Roles { get => roles; set => roles = value; }

            public Persona(string nombre, int cedula, rol roles)
            {
                this.Nombre = nombre;
                this.Cedula = cedula;
                this.Roles = roles;
            }

            public enum rol { estudiante, profesor , administrativo}
        }
        public class Movimiento
        {
            private Material material;
            private Persona persona;
            private DateTime fecha_movimiento;
            private tipo tip;

            internal Material Material { get => material; set => material = value; }
            internal Persona Persona { get => persona; set => persona = value; }
            public DateTime Fecha_movimiento { get => fecha_movimiento; set => fecha_movimiento = value; }
            internal tipo Tip { get => tip; set => tip = value; }

            public Movimiento(Material material, Persona persona, DateTime fecha_movimiento, tipo tip)
            {
                this.Material = material;
                this.Persona = persona;
                this.Fecha_movimiento = fecha_movimiento;
                this.Tip = tip;
            }

            public enum tipo { valorPrestamo, valorDevolucion}
        }
        public class biblioteca
        {
            public List<Material> Materials { get; set; } = new List<Material>();
            public List<Persona> Persona { get; set; } = new List<Persona>();
            public List<Movimiento> Movimiento { get; set;} = new List<Movimiento>();
            
        }
    }
}