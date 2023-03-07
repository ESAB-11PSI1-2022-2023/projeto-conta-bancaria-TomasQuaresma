// Copyright(c) Tomás Quaresma. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabalhoContaBancaria
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Botão Fechar
        /// </summary>
        private void Fechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Botão Levantamento, retira a quantidade de saldo escolhida pelo utilizador
        /// </summary>
        private void Levantamento_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(Quantia.Text) < 0)
                {
                    MessageBox.Show("Introduza um valor numérico valido.","Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    Quantia.Text = null;
                }
            }
            catch
            {
                MessageBox.Show("Introduza um valor numérico valido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Quantia.Text = null;
            }
        }

        /// <summary>
        /// Botão Deposito, adiciona a quantidade de saldo escolhida pelo utilizador
        /// </summary>
        private void Deposito_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(Quantia.Text) < 0)
                {
                    MessageBox.Show("Introduza um valor numérico valido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Quantia.Text = null;
                }
            }
            catch
            {
                MessageBox.Show("Introduza um valor numérico valido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Quantia.Text = null;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Numero.Text = comboBox1.SelectedItem.ToString();
        }
    }
}
