using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class Login : Form
    {
        private static int intentosFallidos;
        private static DateTime FechaDesbloqueo;
        private static Timer temporizador;
        private static TimeSpan tiempoDeEspera;
        public Login()
        {
            InitializeComponent();
            intentosFallidos = Properties.Settings.Default.IntentosFallidos;
            FechaDesbloqueo = (intentosFallidos == 0) ? DateTime.Now : Properties.Settings.Default.FechaDesbloqueo;
            if (FechaDesbloqueo > DateTime.Now) btnIngresar.Enabled = false;
            double tiempo = Properties.Settings.Default.TiempoEspera;
            if (tiempo > 0)
            {
                temporizador = new Timer();
                temporizador.Interval = (FechaDesbloqueo - DateTime.Now).Seconds;
                temporizador.Tick += ActualizarEtiquetaTiempo;
                temporizador.Start();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            Usuario ousuario = new CN_Usuario().Listar().Where(u => u.Documento == txtdocumento.Text).FirstOrDefault();

            if (ousuario == null)
            {
                MessageBox.Show("No se encontró el usuario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (ousuario.Estado == false)
            {
                MessageBox.Show("El usuario está desactivado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (ousuario.Clave != txtclave.Text)
            {
                intentosFallidos++;
                
                if (intentosFallidos % 3 == 2)
                {
                    MessageBox.Show($"Clave incorrecta\nIntento número: {intentosFallidos}\nAl siguiente intento fallido se bloqueará temporalmente el acceso.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (intentosFallidos % 3 == 0)
                {
                    MessageBox.Show($"Clave incorrecta\nEspere un tiempo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    EsperarUnTiempito();
                }
                else
                {
                    MessageBox.Show($"Clave incorrecta\nIntentalo de nuevo\nIntento número: {intentosFallidos}", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                return;
            }

            intentosFallidos = 0;
            Properties.Settings.Default.IntentosFallidos = intentosFallidos;
            Properties.Settings.Default.Save();
            INICIO form = new INICIO(ousuario);
            form.Show();
            this.Hide();
            form.FormClosing += frm_closing;
        }
        private void EsperarUnTiempito()
        {
            btnIngresar.Enabled = false;
            int tiempoDeAumento = Convert.ToInt32(Math.Pow(((intentosFallidos / 3) * 30), 2)) / 60;
            FechaDesbloqueo = DateTime.Now.AddSeconds(tiempoDeAumento);
            temporizador = new Timer();
            temporizador.Interval = tiempoDeAumento;
            temporizador.Tick += ActualizarEtiquetaTiempo;

            Properties.Settings.Default.FechaDesbloqueo = FechaDesbloqueo;
            Properties.Settings.Default.IntentosFallidos = intentosFallidos;
            Properties.Settings.Default.Save();

            temporizador.Start();
        }
        private void ActualizarEtiquetaTiempo(Object sender, EventArgs e)
        {
            tiempoDeEspera = FechaDesbloqueo - DateTime.Now;
            Properties.Settings.Default.TiempoEspera = tiempoDeEspera.TotalSeconds;
            Properties.Settings.Default.Save();
            lblTiempoDeEspera.Text = $"Tiempo de espera: {tiempoDeEspera.Hours}:{tiempoDeEspera.Minutes}:{tiempoDeEspera.Seconds}";

            if (tiempoDeEspera.TotalSeconds <= 0)
            {
                temporizador.Stop();
                btnIngresar.Enabled = true;
                lblTiempoDeEspera.Text = "";

                FechaDesbloqueo = DateTime.Now;
                Properties.Settings.Default.FechaDesbloqueo = FechaDesbloqueo;
                Properties.Settings.Default.Save();
            }
        }
        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            txtdocumento.Text = "";
            txtclave.Text = "";
            this.Show();
        }

        private void txtdocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var txtBox = (TextBox)sender;
                switch (txtBox.Name)
                {
                    case "txtdocumento":
                        txtclave.Focus();
                        break;
                    case "txtclave":
                      btnIngresar .Focus();
                        break;
                }
            }
        }
    }
}
