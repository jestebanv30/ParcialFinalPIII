using Lógica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentación
{
    public partial class Form1 : Form
    {
        SedeService SedeService = new SedeService(ConfigConnection.conexionString);
        VentaService VentaService = new VentaService(ConfigConnection.conexionString);
        public Form1()
        {
            InitializeComponent();
        }

        private void CargarSedes()
        {
            var sedes = SedeService.GetAll();

            cbSedes.DisplayMember = "Nombre_sede";
            cbSedes.ValueMember = "Id_sede";
            cbSedes.DataSource = sedes;
        }

        private void CargarArchivo()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string rutaArchivo = openFileDialog.FileName;
                    // Obtener la sede seleccionada en el ComboBox
                    //string idSede = cbSedes.SelectedValue.ToString();
                    // Imprimir la ruta del archivo en la consola
                    Console.WriteLine("Ruta del archivo seleccionado: " + rutaArchivo);

                    // O mostrar la ruta en un MessageBox
                    MessageBox.Show("Ruta del archivo seleccionado: " + rutaArchivo);
                    try
                    {
                        // Cargar las ventas en la base de datos
                        VentaService.CargarVentas(rutaArchivo);

                        // Mostrar mensaje de éxito
                        MessageBox.Show("Las ventas se han cargado correctamente.");
                    }
                    catch (Exception ex)
                    {
                        // Mostrar mensaje de error
                        MessageBox.Show($"Error al cargar las ventas: {ex.Message}");
                    }
                }
            }
        }
        private void btnCargar_Click(object sender, EventArgs e)
        {
            CargarArchivo();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarSedes();
        }
    }
}
