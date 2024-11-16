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
using static proyecto_final__respaldo_.Biblioteca;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace proyecto_final__respaldo_
{
    public partial class Form1 : Form
    {
        private Biblioteca biblioteca;
        private BibliotecaCatalogo bibliotecaCatalogo = new BibliotecaCatalogo();

        public Form1()
        {
            InitializeComponent();
            this.biblioteca = new Biblioteca();
            bibliotecaCatalogo.Personas = new List<Persona>();
            bibliotecaCatalogo.Materials = new List<Material>();
            CargarDatosDesdeTxt();
            ActualizarDataGridView();
            MessageBox.Show("Bienvenido");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text) && string.IsNullOrWhiteSpace(textBox4.Text) && comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Por favor, llene todos los campos.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Por favor, ingrese su nombre.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Por favor, ingrese su cédula.");
                return;
            }

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un rol.");
                return;
            }

            int cedula;

            if (!int.TryParse(textBox4.Text, out cedula))
            {
                MessageBox.Show("La cédula no es válida.");
                return;
            }

            bool cedulaRepetida = false;
            foreach (var persona in bibliotecaCatalogo.Personas)
            {
                if (persona.Cedula == cedula)
                {
                    cedulaRepetida = true;
                    break;
                }
            }

            if (cedulaRepetida)
            {
                MessageBox.Show("La cédula ya está registrada.");
                return;
            }

            string nombre = textBox3.Text;
            string rol = comboBox1.SelectedItem.ToString();

            Persona nuevaPersona = new Persona(nombre, cedula, rol);
            bibliotecaCatalogo.Personas.Add(nuevaPersona);

            MessageBox.Show($"Persona Registrada: \nNombre: {nombre}\nCédula: {cedula}\nRol: {rol}");

            textBox3.Clear();
            textBox4.Clear();
            comboBox1.SelectedItem = null;

            ActualizarDataGridView();
            GuardarDatosEnTxt();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) && string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Por favor, llene todos los campos.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Por favor, ingrese un identificador.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Por favor, ingrese un título.");
                return;
            }

            int cedula;
            if (!int.TryParse(textBox5.Text, out cedula))
            {
                MessageBox.Show("La cédula no es válida.");
                return;
            }

            bool cedulaRegistrada = false;
            Persona personaRegistrada = null;
            foreach (var persona in bibliotecaCatalogo.Personas)
            {
                if (persona.Cedula == cedula)
                {
                    cedulaRegistrada = true;
                    personaRegistrada = persona;
                    break;
                }
            }

            if (!cedulaRegistrada)
            {
                MessageBox.Show("Por favor registrese.");
                return;
            }

            string identificador = textBox1.Text;

            bool identificadorRepetido = false;
            foreach (var material in bibliotecaCatalogo.Materials)
            {
                if (material.Identificador == identificador)
                {
                    identificadorRepetido = true;
                    break;
                }
            }

            if (identificadorRepetido)
            {
                MessageBox.Show("No se puede repetir identificador.");
                return;
            }

            string titulo = textBox2.Text;
            DateTime fecha = dateTimePicker1.Value;
            int cantidadRegistrada = (int)numericUpDown1.Value;
            int cantidadActual = cantidadRegistrada;

            Material nuevoMaterial = new Material(identificador, titulo, fecha, cantidadRegistrada, cantidadActual);
            bibliotecaCatalogo.Materials.Add(nuevoMaterial);

            personaRegistrada.Materiales.Add(nuevoMaterial);

            MessageBox.Show($"Material registrado \nIdentificador: {identificador}\nTítulo: {titulo}\nFecha: {fecha.ToShortDateString()}\nCantidad: {cantidadRegistrada}\nCédula: {cedula}");

            textBox1.Clear();
            textBox2.Clear();
            textBox5.Clear();
            numericUpDown1.Value = 0;

            ActualizarDataGridView();
            GuardarDatosEnTxt();
        }

        private void CargarDatosDesdeTxt()
        {
            if (!File.Exists("datos.txt"))
                return;

            using (StreamReader reader = new StreamReader("datos.txt"))
            {
                string linea;
                Persona personaActual = null;

                while ((linea = reader.ReadLine()) != null)
                {
                    var partes = linea.Split('|');
                    if (partes[0] == "Persona")
                    {
                        personaActual = new Persona(partes[1], int.Parse(partes[2]), partes[3]);
                        bibliotecaCatalogo.Personas.Add(personaActual);
                    }
                    else if (partes[0] == "Material" && personaActual != null)
                    {
                        Material material = new Material(
                            partes[1],
                            partes[2],
                            DateTime.Parse(partes[3]),
                            int.Parse(partes[4]),
                            int.Parse(partes[4])
                        );
                        personaActual.Materiales.Add(material);
                    }
                }
            }
        }

        private void GuardarDatosEnTxt()
        {
            using (StreamWriter writer = new StreamWriter("datos.txt"))
            {
                foreach (var persona in bibliotecaCatalogo.Personas)
                {
                    writer.WriteLine($"Persona|{persona.Nombre}|{persona.Cedula}|{persona.Roles}");

                    if (persona.Materiales != null && persona.Materiales.Count > 0)
                    {
                        foreach (var material in persona.Materiales)
                        {
                            writer.WriteLine($"Material|{material.Identificador}|{material.Titulo}|{material.Fecharegistro:yyyy-MM-dd}|{material.Cantidad_registrada}");
                        }
                    }
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            GuardarDatosEnTxt();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(bibliotecaCatalogo.Personas);
            form2.Show();
        }

        private void ActualizarDataGridView()
        {
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
