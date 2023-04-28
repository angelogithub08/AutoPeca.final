using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoPeca.Formularios
{
    public partial class Pedido : Form
    {
        private VO.Pedidos ped;
        private List<VO.Pedidos> lista;
        private BE.pedidoBE be;
        public Pedido()
        {
            InitializeComponent();
            ped = new VO.Pedidos();
            lista = new List<VO.Pedidos>();
            liberarEdicao(false);
            carregar();
            carregarClientes();
            carregarPecas();
        }
        private void carregarClientes()
        {
            BE.clientesBE fab = new BE.clientesBE(new VO.Clientes());
            comboBox1.DataSource = null;
            comboBox1.DataSource = fab.listar();
            comboBox1.ValueMember = "codigo";
            comboBox1.DisplayMember = "codigo";
            comboBox1.Refresh();
        }
        private void carregarPecas()
        {
            BE.pecaBE pec = new BE.pecaBE(new VO.Pecas());
            comboBox2.DataSource = null;
            comboBox2.DataSource = pec.listar();
            comboBox2.ValueMember = "codigo";
            comboBox2.DisplayMember = "codigo";
            comboBox2.Refresh();
        }

        private void carregar()
        {
            be = new BE.pedidoBE(this.ped);
            listpedido.DataSource = null;
            listpedido.DataSource = be.listar();
            listpedido.SelectedIndex = -1;
            listpedido.ValueMember = "cod";
            listpedido.DisplayMember = "cod";
            listpedido.Refresh();
        }

        private void limpar()
        {
            txtcod.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            txtdatetime.Text = "";
        }

        private void InteractToObject()
        {

            ped.cod = int.Parse(txtcod.Text);
            ped.datetime = txtdatetime.Text;
            ped.codigodocliente = new VO.Clientes();
            ped.codigodocliente = (VO.Clientes)comboBox1.SelectedItem;
            ped.codigodapeca = new VO.Pecas();
            ped.codigodapeca = (VO.Pecas)comboBox2.SelectedItem;
            //pec.codigocliente = comboBox1.SelectedItem.ToString();
            //pec.codigopeca = comboBox2.SelectedItem.ToString();
        }
        private void objecttoInterface()
        {
            txtcod.Text = ped.cod.ToString();
            txtdatetime.Text = ped.datetime.ToString();
            //comboBox1.SelectedItem = pec.codigocliente.ToString();
            //comboBox2.SelectedItem = pec.codigopeca.ToString();
            int index = 0;
            foreach (VO.Clientes item in comboBox1.Items)
            {
                if (item.codigo.Equals(ped.codigodocliente.codigo))
                {
                    comboBox1.SelectedIndex = index;
                    break;
                }
                index++;
            }
            int index1 = 0;
            foreach (VO.Pecas item in comboBox2.Items)
            {
                if (item.Codigo.Equals(ped.codigodapeca.Codigo))
                {
                    comboBox2.SelectedIndex = index1;
                    break;
                }
                index++;
            }
        }
        private void liberarEdicao(bool habilita)
        {
            btnsalvar.Enabled = !habilita;
            btneditar.Enabled = habilita;
        }

        private void Pedido_Load(object sender, EventArgs e)
        {

        }

        private void btnsalvar_Click(object sender, EventArgs e)
        {
            try
            {
                ped = new VO.Pedidos();
                InteractToObject();
                be = new BE.pedidoBE(this.ped);
                be.incluir();
                lista.Add(ped);
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
            be = new BE.pedidoBE(this.ped);
            be.alterar();
            carregar();
        }

        private void btnselect_Click(object sender, EventArgs e)
        {
            be = new BE.pedidoBE(this.ped);
            ped = ((VO.Pedidos)listpedido.Items[listpedido.SelectedIndex]);
            objecttoInterface();
            liberarEdicao(true);
        }

        private void btnexcluir_Click(object sender, EventArgs e)
        {
            be = new BE.pedidoBE(this.ped);
            be.remover(int.Parse(listpedido.SelectedIndex.ToString()));
            carregar();
        }

        private void btnlimpar_Click(object sender, EventArgs e)
        {
            limpar();
            liberarEdicao(false);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtcodcli_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
