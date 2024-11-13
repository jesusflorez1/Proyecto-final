using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using herramientas;
using Herramientas;


// Agregue la parte de almacenar informacion

namespace Biblioteca
{
    public partial class Form1 : Form
    {
        private List<Material> materiales = new List<Material>();
        private List<Herramientas.Persona> personas = new List<Herramientas.Persona>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbRol.DataSource = Enum.GetValues(typeof(Herramientas.Persona.rol));
            cmbRol.SelectedIndex = 0;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (cmbRol.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un rol.");
                return;
            }

            string nombre = txtNombre.Text.Trim();
            int cedula = int.TryParse(txtCedula.Text.Trim(), out int cedulaResult) ? cedulaResult : 0;
            var rol = (Herramientas.Persona.rol)Enum.Parse(typeof(Herramientas.Persona.rol), cmbRol.SelectedItem.ToString());

            if (personas.Exists(p => p.Cedula == cedula))
            {
                MessageBox.Show("La cédula ya está registrada.");
                return;
            }

            Herramientas.Persona persona = new Herramientas.Persona(nombre, cedula, rol);
            personas.Add(persona);

            txtNombre.Clear();
            txtCedula.Clear();
            cmbRol.SelectedIndex = -1;

            MessageBox.Show("Persona registrada exitosamente.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdentificador.Text) || string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MessageBox.Show("Por favor, completa todos los campos del material.");
                return;
            }

            string id = txtIdentificador.Text.Trim();
            string titulo = txtTitulo.Text.Trim();
            DateTime fechaRegistro = dtpFechaRegistro.Value;
            int cantidad = (int)Txtcantidad.Value;

            if (materiales.Exists(m => m.Identificador == id))
            {
                MessageBox.Show("El identificador del material ya existe.");
                return;
            }

            Material nuevoMaterial = new Material(id, titulo, fechaRegistro, cantidad, cantidad);
            materiales.Add(nuevoMaterial);

            txtIdentificador.Clear();
            txtTitulo.Clear();
            Txtcantidad.Value = 0;

            MessageBox.Show("Material registrado exitosamente.");
        }

        private void btnAbrirForm2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Materiales = materiales;
            form2.Personas = personas;
            form2.Show();
        }
    }
}



