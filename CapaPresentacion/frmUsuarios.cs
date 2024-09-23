using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaPresentacion.utilidades;
using CapaEntidad;
using CapaNegocio;
using System.Windows.Forms.ComponentModel.Com2Interop;
namespace CapaPresentacion
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
            //Cambio remoto
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            cboestado.Items.Add(new opcionCombo() { Valor = 1, Texto = "Activo" });
            cboestado.Items.Add(new opcionCombo() { Valor = 0, Texto = "No Activo" });
            cboestado.DisplayMember = "Texto";
            cboestado.ValueMember = "Valor";
            cboestado.SelectedIndex = 0;

            List<Rol> listaRol = new CN_Rol().Listar();

            foreach(var rol in listaRol)
            {
                cborol.Items.Add(new opcionCombo() { Valor = rol.IdRol, Texto = rol.Descripcion });
            }
            cborol.DisplayMember = "Texto";
            cborol.ValueMember = "Valor";
            cborol.SelectedIndex = 0;

            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                if (columna.Visible==true && columna.Name != "btnseleccionar")
                {
                    cbobúsqueda.Items.Add(new opcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });

                }
            }
            cbobúsqueda.DisplayMember = "Texto";
            cbobúsqueda.ValueMember = "Valor";
            cbobúsqueda.SelectedIndex = 0;



            //mostrar todos los usuarios

            List<Usuario> listaUsuario = new CN_Usuario().Listar();

            foreach (var usuario in listaUsuario)
            {
                dgvdata.Rows.Add(new object[] {"",usuario.IdUsuario, usuario.Documento, usuario.NombreCompleto, usuario.Correo, usuario.Clave,
                usuario.oRol.IdRol,
                usuario.oRol.Descripcion,
                usuario.Estado == true ? 1 : 0,
                usuario.Estado == true ? "Activo" : "No Activo"
                });
            }

        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            dgvdata.Rows.Add(new object[] {"",txtid.Text,txtdocumento.Text,txtnombrecompleto.Text,txtcorreo.Text,txtclave.Text,
            ((opcionCombo)cborol.SelectedItem).Valor.ToString(),
             ((opcionCombo)cborol.SelectedItem).Texto.ToString(),
              ((opcionCombo)cboestado.SelectedItem).Valor.ToString(),
               ((opcionCombo)cboestado.SelectedItem).Texto.ToString(),
            });

            Limpiar();

        }

        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtid.Text = "";
            txtdocumento.Text = "";
            txtnombrecompleto.Text = "";
            txtcorreo.Text = "";
            txtclave.Text = "";
            txtconfirmarclave.Text = "";
            cborol.SelectedIndex = 0;
            cboestado.SelectedIndex = 0;
        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.chek.Width;
                var h = Properties.Resources.chek.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.chek, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {
                int indice = e.RowIndex;
                if(indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtid.Text = dgvdata.Rows[indice].Cells["Id"].Value.ToString();
                    txtdocumento.Text = dgvdata.Rows[indice].Cells["Documento"].Value.ToString();
                    txtnombrecompleto.Text = dgvdata.Rows[indice].Cells["NombreCompleto"].Value.ToString();
                    txtcorreo.Text = dgvdata.Rows[indice].Cells["Correo"].Value.ToString();
                    txtclave.Text = dgvdata.Rows[indice].Cells["Clave"].Value.ToString();
                    txtconfirmarclave.Text = dgvdata.Rows[indice].Cells["Clave"].Value.ToString();


                    foreach(opcionCombo oc in cborol.Items)
                    {
                        if(Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["IdRol"].Value))
                        {
                            int indice_combo = cborol.Items.IndexOf(oc);
                            cborol.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                    foreach (opcionCombo oc in cboestado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indice_combo = cboestado.Items.IndexOf(oc);
                            cboestado.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                }
            }
        }
    }
}
