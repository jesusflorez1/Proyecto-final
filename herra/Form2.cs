using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using static proyecto_final__respaldo_.Biblioteca;

namespace proyecto_final__respaldo_
{
    public partial class Form2 : Form
    {
        private Form1 form1;
        private List<Persona> personas;
        private List<Material> materiales;

        public Form2(Form1 form1, List<Persona> personas, List<Material> materiales)
        {
            InitializeComponent();
            this.form1 = form1;
            this.personas = personas ?? new List<Persona>(); 
            this.materiales = materiales ?? new List<Material>(); 
            ConfigurarDataGridView();
            ActualizarDataGridView();
            MessageBox.Show("Base de Datos");
            this.FormClosing += Form2_FormClosing;
        }

        public Form2(Form1 form1)
        {
            this.form1 = form1;
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void ConfigurarDataGridView()
        {
            dataGridView.Columns.Clear();

            dataGridView.Columns.Add("Nombre", "Nombre");
            dataGridView.Columns.Add("Cedula", "Cédula");
            dataGridView.Columns.Add("Roles", "Rol");
            dataGridView.Columns.Add("Identificador", "Identificador");
            dataGridView.Columns.Add("Titulo", "Título");
            dataGridView.Columns.Add("FechaRegistro", "Fecha de Registro");
            dataGridView.Columns.Add("CantidadRegistrada", "Cantidad Registrada");
        }

        public void ActualizarDataGridView()
        {
            dataGridView.Rows.Clear();

            if (personas == null || personas.Count == 0)
            {
                MessageBox.Show("No hay personas registradas.");
                return;
            }

            foreach (var persona in personas)
            {
                bool personaAgregada = false;

                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells[1].Value != null && row.Cells[1].Value.ToString() == persona.Cedula.ToString())
                    {
                        personaAgregada = true;
                        break;
                    }
                }

                if (!personaAgregada)
                {
                    var personaFila = new DataGridViewRow();
                    personaFila.CreateCells(dataGridView, persona.Nombre, persona.Cedula.ToString(), persona.Roles, "", "", "", "");
                    dataGridView.Rows.Add(personaFila);
                }

                foreach (var material in persona.Materiales)
                {
                    var materialFila = new DataGridViewRow();
                    materialFila.CreateCells(dataGridView,
                        persona.Nombre,
                        persona.Cedula.ToString(),
                        persona.Roles.ToString(),
                        material.Identificador,
                        material.Titulo,
                        material.Fecharegistro.ToShortDateString(),
                        material.Cantidad_registrada);
                    dataGridView.Rows.Add(materialFila);
                }
            }

            dataGridView.AutoResizeColumns();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Index % 2 == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }

            if (dataGridView.Rows.Count > 0)
            {
                dataGridView.Rows[0].Selected = true;
            }
        }

        public void AgregarPersona(Persona nuevaPersona)
        {
            personas.Add(nuevaPersona);
            ActualizarDataGridView();
        }

        public void GuardarEnArchivo()
        {
            try
            {
                MessageBox.Show("Guardando datos...");  

                using (StreamWriter writer = new StreamWriter("datos.txt", false))
                {
                    foreach (var persona in personas)
                    {
                        writer.WriteLine($"Nombre: {persona.Nombre}");
                        writer.WriteLine($"Cédula: {persona.Cedula}");
                        writer.WriteLine($"Rol: {persona.Roles}");
                        writer.WriteLine("Materiales:");

                        foreach (var material in persona.Materiales)
                        {
                            writer.WriteLine($"\tIdentificador: {material.Identificador}");
                            writer.WriteLine($"\tTítulo: {material.Titulo}");
                            writer.WriteLine($"\tFecha de Registro: {material.Fecharegistro.ToShortDateString()}");
                            writer.WriteLine($"\tCantidad Registrada: {material.Cantidad_registrada}");
                        }

                        writer.WriteLine();
                    }
                }

                MessageBox.Show("Datos guardados correctamente en 'datos.txt'");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos: {ex.Message}");
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            GuardarEnArchivo();
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            GuardarEnArchivo();
        }   

        public void RefrescarVista()
        {
            ActualizarDataGridView();
        }
    }
}
