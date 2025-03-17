using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class Login : Form
    {
        private const int INTENTOS_PERMITIDOS = 5;
        private int _intentosFallidos;
        private DateTime _fechaDesbloqueo;
        private Timer _temporizador;
        private int IntentosFallidos
        {
            get => _intentosFallidos;
            set
            {
                _intentosFallidos = value;
                Properties.Settings.Default.IntentosFallidos = value;
                Properties.Settings.Default.Save();
            }
        }
        private DateTime FechaDesbloqueo
        {
            get => _fechaDesbloqueo;
            set
            {
                _fechaDesbloqueo = value;
                Properties.Settings.Default.FechaDesbloqueo = value;
                Properties.Settings.Default.Save();
            }
        }
        public Login()
        {
            InitializeComponent();
            _intentosFallidos = Properties.Settings.Default.IntentosFallidos;
            _fechaDesbloqueo = (_intentosFallidos > 0) ? Properties.Settings.Default.FechaDesbloqueo : DateTime.Now;
            if (FechaDesbloqueo > DateTime.Now)
            {
                EsperarUnRatito();
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
                IntentosFallidos++;
                int intentosRestantes = INTENTOS_PERMITIDOS - (IntentosFallidos % INTENTOS_PERMITIDOS);

                if (IntentosFallidos % INTENTOS_PERMITIDOS == 0)
                {
                    EsperarUnRatito();
                    MessageBox.Show("Clave incorrecta\nSe ha bloqueado temporalmente el acceso", "Mensaje", 
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (intentosRestantes == 1)
                {
                    MessageBox.Show("Clave incorrecta\nAl siguiente intento fallido se bloqueará temporalmente el acceso", 
                        "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show($"Clave incorrecta\nQuedan {intentosRestantes} intentos antes del bloqueo", 
                        "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return;
            }
            IntentosFallidos = 0;
            INICIO form = new INICIO(ousuario);
            form.Show();
            this.Hide();
            form.FormClosing += frm_closing;
        }
        private void EsperarUnRatito()
        {
            btnIngresar.Enabled = false;
            int bloqueosAnteriores = IntentosFallidos / INTENTOS_PERMITIDOS;
            int segundosBloqueo = 30 * bloqueosAnteriores * bloqueosAnteriores;
            FechaDesbloqueo = DateTime.Now.AddSeconds(segundosBloqueo);

            _temporizador = new Timer();
            _temporizador.Interval = 1000;
            _temporizador.Tick += ActualizarInterfaz;
            _temporizador.Start();
        }
        private void ActualizarInterfaz(object sender, EventArgs e)
        {
            TimeSpan tiempoRestante = FechaDesbloqueo - DateTime.Now;
            Properties.Settings.Default.TiempoEspera = tiempoRestante.TotalSeconds;
            if (tiempoRestante.TotalSeconds <= 0)
            {
                _temporizador.Stop();
                lblTiempoDeEspera.Text = "";
                btnIngresar.Enabled = true;
                return;
            }
            string formato = tiempoRestante.Hours > 0 ? @"hh\:mm\:ss" : @"mm\:ss";
            lblTiempoDeEspera.Text = $"Tiempo de espera: {tiempoRestante.ToString(formato)}";
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
