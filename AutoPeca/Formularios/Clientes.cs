using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoPeca.Formularios
{
    public partial class Clientes : Form
    {
        private VO.Clientes cli;
        private List<VO.Clientes> lista;
        private BE.clientesBE be;
        public Clientes()
        {
            InitializeComponent();
            cli = new VO.Clientes();
            lista = new List<VO.Clientes>();
            liberarEdicao(false);
            carregar();
        }

        private void carregar()
        {
            be = new BE.clientesBE(this.cli);
            listcliente.DataSource = null;
            listcliente.DataSource = be.listar();
            listcliente.SelectedIndex = -1;
            listcliente.ValueMember = "codigo";
            listcliente.DisplayMember = "nome";
            listcliente.Refresh();
        }

        private void limpar()
        {
            txtcod.Text = "";
            txtnome.Text = "";
            txtcpf.Text = "";
            txtnum.Text = "";
            txtendereco.Text = "";
            txtcidade.Text = "";
            txtestado.Text = "";
            txtpais.Text = "";

        }
        private void InteractToObject()
        {

            cli.codigo = int.Parse(txtcod.Text);
            cli.nome = txtnome.Text;
            cli.cpf = txtcpf.Text;
            cli.end = txtendereco.Text;
            cli.num = txtnum.Text;
            cli.cidade = txtcidade.Text;
            cli.estado = txtestado.Text;
            cli.pais = txtpais.Text;
        }
        private void liberarEdicao(bool habilita)
        {
            btnsalvar.Enabled = !habilita;
            btneditar.Enabled = habilita;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnsalvar_Click(object sender, EventArgs e)
        {
            try
            {
                cli = new VO.Clientes();
                InteractToObject();
                be = new BE.clientesBE(this.cli);
                be.incluir();
                lista.Add(cli);
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
            be = new BE.clientesBE(this.cli);
            be.alterar();
            carregar();
        }

        private void btnselect_Click(object sender, EventArgs e)
        {
            cli = ((VO.Clientes)listcliente.Items[listcliente.SelectedIndex]);
            be = new BE.clientesBE(this.cli);
            txtcod.Text = cli.codigo.ToString();
            txtnome.Text = cli.nome.ToString();
            txtcpf.Text = cli.cpf.ToString();
            txtendereco.Text = cli.end.ToString();
            txtnum.Text = cli.num.ToString();
            txtcidade.Text = cli.cidade.ToString();
            txtestado.Text = cli.estado.ToString();
            txtpais.Text = cli.pais.ToString();
            liberarEdicao(true);
        }

        private void btnexcluir_Click(object sender, EventArgs e)
        {
            be = new BE.clientesBE(this.cli);
            be.remover(int.Parse(listcliente.SelectedIndex.ToString()));
            carregar();
        }

        private void btnlimpar_Click(object sender, EventArgs e)
        {
            limpar();
            liberarEdicao(false);
        }

        private void Clientes_Load(object sender, EventArgs e)
        {

        }
    }
}
