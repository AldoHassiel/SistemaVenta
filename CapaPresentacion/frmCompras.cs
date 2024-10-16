using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;

//using CapaPresentacion.Modales;
using CapaPresentacion.utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmCompras : Form
    {

        private Usuario _Usuario;

        public frmCompras(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }

        private void frmCompras_Load(object sender, EventArgs e)
        {
            cbotipodocumento.Items.Add(new opcionCombo() { Valor = "Boleto", Texto = "Boleto" });
            cbotipodocumento.Items.Add(new opcionCombo() { Valor = "Factura", Texto = "Factura" });
            cbotipodocumento.DisplayMember = "Texto";
            cbotipodocumento.ValueMember = "Valor";
            cbotipodocumento.SelectedIndex = 0;

            txtfecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtidproveedor.Text = "0";
            txtidproducto.Text = "0";
        }

        private void btnbuscarproveedor_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProveedor())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtidproveedor.Text = modal._Proveedor.IdProveedor.ToString();
                    txtdocproveedor.Text = modal._Proveedor.Documento;
                    txtnombreproveedor.Text = modal._Proveedor.RazonSocial;
                }
                else
                {
                    txtidproveedor.Select();
                }
            }
        }

        private void btnbuscarproducto_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProducto())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtidproducto.Text = modal._Producto.IdProducto.ToString();
                    txtcodproducto.Text = modal._Producto.Codigo;
                    txtproducto.Text = modal._Producto.Nombre;
                    txtpreciocompra.Select();
                }
                else
                {
                    txtcodproducto.Select();
                }
            }
        }

        private void txtcodproducto_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void btnagregarproducto_Click(object sender, EventArgs e)
        {
            

        }

        private void limpiarProducto()
        {
            
        }

        private void calcularTotal()
        {
            
        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            
        }

        private void txtpreciocompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtprecioventa_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void btnregistrar_Click(object sender, EventArgs e)
        {

        }
    }
}