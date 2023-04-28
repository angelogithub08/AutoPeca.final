using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoPeca.Formularios
{
    public partial class Peças : Form
    {
        private VO.Pecas pec;
        private List<VO.Pecas> lista;
        private BE.pecaBE be;

        public Peças()
        {
            InitializeComponent();
            pec = new VO.Pecas();
            lista = new List<VO.Pecas>();
            liberarEdicao(false);
            carregar();
        }       
        private void carregar()
        {
            be = new BE.pecaBE(this.pec);
            listpecas.DataSource = null;
            listpecas.DataSource = be.listar();
            listpecas.SelectedIndex = -1;
            listpecas.ValueMember = "ID";
            listpecas.DisplayMember = "Codigo";
            listpecas.Refresh();
        }

        private void limpar()
        {
            txtcod.Text = "";
            txtid.Text = "";
            txtdesc.Text = "";
            txtcodbarra.Text = "";
        }

        private void InteractToObject()
        {

            pec.Codigo = int.Parse(txtcod.Text);
            pec.Id = int.Parse(txtid.Text);
            pec.Descricao = txtdesc.Text;
            pec.CodigoBarras = txtcodbarra.Text;
        }

        private void liberarEdicao(bool habilita)
        {
            btnsalvar.Enabled = !habilita;
            btneditar.Enabled = habilita;
        }



        private void txtid_TextChanged(object sender, EventArgs e)
        {

        }

        private void Peças_Load(object sender, EventArgs e)
        {

        }

        private void btnsalvar_Click(object sender, EventArgs e)
        {
            try
            {
                pec = new VO.Pecas();
                InteractToObject();
                be = new BE.pecaBE(this.pec);
                be.incluir();
                lista.Add(pec);
                limpar();
                carregar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro No Aplicativo");

            }
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            InteractToObject();
            be = new BE.pecaBE(this.pec);
            be.alterar();
            carregar();
        }

        private void btnselect_Click(object sender, EventArgs e)
        {
            pec = ((VO.Pecas)listpecas.Items[listpecas.SelectedIndex]);
            be = new BE.pecaBE(this.pec);
            txtcod.Text = pec.Codigo.ToString();
            txtid.Text = pec.Id.ToString();
            txtdesc.Text = pec.Descricao.ToString();
            txtcodbarra.Text = pec.CodigoBarras.ToString();
            liberarEdicao(true);
        }

        private void btnexcluir_Click(object sender, EventArgs e)
        {            
            be = new BE.pecaBE(this.pec);
            be.remover(int.Parse(listpecas.SelectedIndex.ToString()));
            carregar();
        }

        private void btnlimpar_Click(object sender, EventArgs e)
        {
            limpar();
            liberarEdicao(false);
        }
    }
}
