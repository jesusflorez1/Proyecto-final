using Herramientas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblioteca
{
    public partial class Form2 : Form
    {
        public List<Material> Materiales { get; set; }
        public List<Herramientas.Persona> Personas { get; set; }

        public Form2(List<Material> materiales = null, List<Herramientas.Persona> personas = null)
        {
            InitializeComponent();
            Materiales = materiales ?? new List<Material>();
            Personas = personas ?? new List<Herramientas.Persona>();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Mostrar los materiales en el DataGridView
            if (Materiales != null && Materiales.Count > 0)
            {
                dataGridView1.DataSource = null; // Limpiar el DataGridView antes de agregar los datos
                dataGridView1.DataSource = Materiales;
                dataGridView1.AutoResizeColumns();
                dataGridView1.AutoGenerateColumns = true; // Automáticamente genera las columnas basadas en la clase Material
            }
            else
            {
                MessageBox.Show("No hay materiales registrados.");
            }

            
            
        }
    }
}
