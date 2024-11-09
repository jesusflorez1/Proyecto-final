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
        // Lista que contiene los materiales registrados
        private List<Material> materiales = new List<Material>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Llenar el ComboBox con los valores del enum Persona.rol
            cmbRol.DataSource = Enum.GetValues(typeof(Herramientas.Persona.rol));
        }

        // Evento para registrar el material
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Recuperar los datos del formulario
            string id = txtIdentificador.Text.Trim();
            string titulo = txtTitulo.Text.Trim();
            DateTime fechaRegistro = dtpFechaRegistro.Value;
            int cantidad = (int)Txtcantidad.Value;

            string nombre = txtNombre.Text.Trim();
            int cedula = int.TryParse(txtCedula.Text.Trim(), out int cedulaResult) ? cedulaResult : 0;

            // Obtener el rol del ComboBox
            var rol = (Herramientas.Persona.rol)Enum.Parse(typeof(Herramientas.Persona.rol), cmbRol.SelectedItem.ToString());
            Herramientas.Persona persona = new Herramientas.Persona(nombre, cedula, rol);

            // Verificar si el material ya existe en la lista
            if (materiales.Exists(m => m.Identificador == id))
            {
                MessageBox.Show("El identificador ya existe.");
                return;
            }

            // Crear el nuevo material
            Material nuevoMaterial = new Material(id, titulo, fechaRegistro, cantidad, cantidad);

            // Asignar la persona al material
            nuevoMaterial.Persona = persona;

            // Agregar el nuevo material a la lista
            materiales.Add(nuevoMaterial);

            // Limpiar los campos después de registrar el material
            txtIdentificador.Clear();
            txtTitulo.Clear();
            txtNombre.Clear();
            txtCedula.Clear();
            Txtcantidad.Value = 0;
            cmbRol.SelectedIndex = -1; // Limpiar el ComboBox

            MessageBox.Show("Material y persona registrados.");
        }
    }
}


