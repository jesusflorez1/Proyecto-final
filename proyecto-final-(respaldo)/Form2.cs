using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static proyecto_final__respaldo_.Biblioteca;

namespace proyecto_final__respaldo_
{
    public partial class Form2 : Form
    {
        private List<Persona> personas;

        public Form2(List<Persona> personas)
        {
            InitializeComponent();
            this.personas = personas;

            ActualizarDataGridView();

            MessageBox.Show("Base de Datos");
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        public void ActualizarDataGridView()
        {
            dataGridView.Rows.Clear();

            foreach (var persona in personas)
            {
                // Filas para la persona
                var personaFila = new DataGridViewRow();
                personaFila.Cells.Add(new DataGridViewTextBoxCell() { Value = persona.Nombre });
                personaFila.Cells.Add(new DataGridViewTextBoxCell() { Value = persona.Cedula });

                dataGridView.Rows.Add(personaFila);

                // Añadir los materiales de cada persona
                foreach (var material in persona.Materiales)
                {
                    var materialFila = new DataGridViewRow();
                    materialFila.Cells.Add(new DataGridViewTextBoxCell() { Value = persona.Nombre }); // Nombre de la persona
                    materialFila.Cells.Add(new DataGridViewTextBoxCell() { Value = persona.Cedula }); // Cédula de la persona
                    materialFila.Cells.Add(new DataGridViewTextBoxCell() { Value = material.Identificador }); // Identificador del material
                    materialFila.Cells.Add(new DataGridViewTextBoxCell() { Value = material.Titulo }); // Título del material
                    materialFila.Cells.Add(new DataGridViewTextBoxCell() { Value = material.Fecharegistro.ToShortDateString() }); // Fecha del material
                    materialFila.Cells.Add(new DataGridViewTextBoxCell() { Value = material.Cantidad_registrada }); // Cantidad registrada del material

                    dataGridView.Rows.Add(materialFila);
                }
            }
        }

        // Método para agregar una nueva persona a la lista
        public void AgregarPersona(Persona nuevaPersona)
        {
            personas.Add(nuevaPersona);  // Agregar la persona a la lista
            ActualizarDataGridView();    // Actualizar el DataGridView
        }

        // Método para eliminar una persona
        public void EliminarPersona(int cedula)
        {
            var personaAEliminar = personas.FirstOrDefault(p => p.Cedula == cedula);
            if (personaAEliminar != null)
            {
                personas.Remove(personaAEliminar);  // Eliminar la persona de la lista
                ActualizarDataGridView();           // Actualizar el DataGridView
            }
        }
    }
}
