using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Herramientas;


// optimize el codgio basandome en las clases que elaboror yisus




namespace Biblioteca
{
    public partial class Form1 : Form
    {
        private List<Material> materiales = new List<Material>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbRol.DataSource = Enum.GetValues(typeof(Herramientas.Persona.rol));
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string id = txtIdentificador.Text.Trim();
            string titulo = txtTitulo.Text.Trim();
            DateTime fechaRegistro = dtpFechaRegistro.Value;
            int cantidad = (int)Txtcantidad.Value;

            string nombre = txtNombre.Text.Trim();
            int cedula = int.TryParse(txtCedula.Text.Trim(), out int cedulaResult) ? cedulaResult : 0;

            var rol = (Herramientas.Persona.rol)Enum.Parse(typeof(Herramientas.Persona.rol), cmbRol.SelectedItem.ToString());
            Herramientas.Persona persona = new Herramientas.Persona(nombre, cedula, rol);

            if (materiales.Exists(m => m.Identificador == id))
            {
                MessageBox.Show("El identificador ya existe.");
                return;
            }

            Material nuevoMaterial = new Material(id, titulo, fechaRegistro, cantidad, cantidad);

            nuevoMaterial.Persona = persona;

            materiales.Add(nuevoMaterial);

            txtIdentificador.Clear();
            txtTitulo.Clear();
            txtNombre.Clear();
            txtCedula.Clear();
            Txtcantidad.Value = 0;
            cmbRol.SelectedIndex = -1; 

            MessageBox.Show("Material y persona registrados.");
        }
    }
}


