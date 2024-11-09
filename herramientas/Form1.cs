using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// optimize el codgio basandome en las clases que elaboror yisus

namespace Biblioteca
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        private List<Material> materiales;

        public Form1()
        {
            InitializeComponent();
            materiales = new List<Material>();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string id = txtIdentificador.Text.Trim();
            string titulo = txtTitulo.Text.Trim();
            DateTime fechaRegistro = dtpFechaRegistro.Value;
            int cantidad = (int)Txtcantidad.Value;

            if (materiales.Exists(m => m.Identificador == id))
            {
                MessageBox.Show("El identificador ya existe.");
                return;
            }

            Material nuevoMaterial = new Material(id, titulo, fechaRegistro, cantidad);
            materiales.Add(nuevoMaterial);
            MessageBox.Show("Material registrado.");

            txtIdentificador.Clear();
            txtTitulo.Clear();
            Txtcantidad.Value = 0;
        }
    }

    public class Material
    {
        public string Identificador { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int CantidadRegistrada { get; set; }

        public Material(string identificador, string titulo, DateTime fechaRegistro, int cantidadRegistrada)
        {
            Identificador = identificador;
            Titulo = titulo;
            FechaRegistro = fechaRegistro;
            CantidadRegistrada = cantidadRegistrada;
        }
    }
}

