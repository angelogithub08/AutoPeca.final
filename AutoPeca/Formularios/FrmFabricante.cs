using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoPeca.Formularios
{
    public partial class FrmFabricante : Form
    {

        private VO.Fabricante fab;
        private List<VO.Fabricante> lista;
        private BE.FabricanteBE be;

        public FrmFabricante()
        {
            InitializeComponent();            
            liberarEdicao(false);
            InicializarVeiculos();
            carregar();
        }
        private void InicializarVeiculos()
        {
            fab = new VO.Fabricante();
            if (DAO.DAO.listaFabricante == null)
            {
                DAO.DAO.listaFabricante = new List<VO.Fabricante>();
            }
            lista = DAO.DAO.listaFabricante;
        }

        private void txtnome_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtcod_TextChanged(object sender, EventArgs e)
        {

        }

        private void InteractToObject()
        {

            fab.codigo = int.Parse(txtcod.Text);
            fab.nome = txtnome.Text;
            fab.desc = txtdesc.Text;


        }
        private void limpar1()
        {
            txtcod.Text = "";
            txtnome.Text = "";
            txtdesc.Text = "";
        }
        private void carregar()
        {
            be = new BE.FabricanteBE(this.fab);
            lstfabricante.DataSource = null;
            lstfabricante.DataSource = be.listar();
            lstfabricante.SelectedIndex = -1;
            lstfabricante.ValueMember = "codigo";
            lstfabricante.DisplayMember = "nome";
            lstfabricante.Refresh();
        }

        private void btnlimpar2_Click(object sender, EventArgs e)
        {
            limpar1();
            liberarEdicao(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                fab = new VO.Fabricante();
                InteractToObject();
                be = new BE.FabricanteBE(this.fab);
                be.incluir();
                lista.Add(fab);
                limpar1();
                carregar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro No Aplicativo");

            }
        }

        private void btnselecionar_Click(object sender, EventArgs e)
        {
          
            fab = ((VO.Fabricante)lstfabricante.Items[lstfabricante.SelectedIndex]);
            be = new BE.FabricanteBE(this.fab);
            txtcod.Text = fab.codigo.ToString();
            txtnome.Text = fab.nome.ToString();
            txtdesc.Text = fab.desc.ToString();
            liberarEdicao(true);
        }
        private void liberarEdicao(bool habilita)
        {
            button1.Enabled = !habilita;
            btneditar.Enabled = habilita;
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            InteractToObject();
            be = new BE.FabricanteBE(this.fab);
            be.alterar();
            carregar();
        }

        private void btnexcluir_Click(object sender, EventArgs e)
        {
            be = new BE.FabricanteBE(this.fab);
            be.remover(int.Parse(lstfabricante.SelectedIndex.ToString()));
            carregar();
        }
    }

}

