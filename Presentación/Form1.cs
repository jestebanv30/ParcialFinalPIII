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

        private void CargarGrilla()
        {
            dataGridView1.DataSource = VentaService.GetAll();
        }

        private void ValidacionesyCargarArchivo()
        {
            if (rbValidar.Checked == true)
            {
                if (cbSedes.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione una sede para validar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
                    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string rutaArchivo = openFileDialog.FileName;

                        try
                        {
                            string idSedeSeleccionada = cbSedes.SelectedValue.ToString();
                            VentaService.CargarVentasValidando(rutaArchivo, idSedeSeleccionada);

                            MessageBox.Show("Las ventas se han cargado correctamente.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al cargar las ventas: {ex.Message}");
                        }
                    }
                }
            }
            else if (rbSinvalidar.Checked == true)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
                    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string rutaArchivo = openFileDialog.FileName;

                        try
                        {
                            VentaService.CargarVentas(rutaArchivo);

                            MessageBox.Show("Las ventas se han cargado correctamente.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al cargar las ventas: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione una opción de validación", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCargar_Click(object sender, EventArgs e)
        {
            ValidacionesyCargarArchivo();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarSedes();
            //CargarGrilla();
        }

        private void cbSedes_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
